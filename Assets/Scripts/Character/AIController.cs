using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    private Animator animator;
    private NavMeshAgent agent;
    private ThirdPersonCharacter character;
    private CharacterStats targetStats;
    private CharacterStats myStats;
    public bool approachedTarget;
    private float attackCooldown = 0f;
    private float attackSpeed = 1f;

    public enum State
    {
        PATROL,
        CHASE,
        FIGHT,
        DEATH
    }

    public State state;
    private bool isEnemy;
    // Variables for Patrolling
    [SerializeField]
    private GameObject[] waypoints;
    [SerializeField]
    private int currentWaypoint = 0;
    [SerializeField]
    private float patrolSpeed = 0.5f;

    // Variables for Chasing
    [SerializeField]
    private float chaseSpeed = 1f;
    [SerializeField]
    private GameObject target;

    [SerializeField]
    private float lookRadius = 15f;

    [SerializeField]
    private float attackZone = 5f;

    [SerializeField]
    private float distanceToTarget;

    // Variables for Attacking
    [SerializeField]
    private float safeZone = 2f;

    public NavMeshAgent GetNavMeshAgent()
        => agent;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        character = GetComponent<ThirdPersonCharacter>();
        myStats = GetComponent<CharacterStats>();
        agent.updatePosition = true;
        agent.updateRotation = false;

        state = State.PATROL;

        myStats.alive = true;

        StartCoroutine("FSM");
    }

    void Patrol()
    {
        agent.stoppingDistance = 0f;
        agent.speed = patrolSpeed;

        if (!character.m_SheathSword)
            character.SheathSword();

        if (waypoints.Length == 0) return;

        if (Vector3.Distance(transform.position, waypoints[currentWaypoint].transform.position) > 2)
        {
            //Debug.Log("Distance to the next waypoint: " + Vector3.Distance(transform.position, waypoints[currentWaypoint].transform.position));
            agent.SetDestination(waypoints[currentWaypoint].transform.position);
            character.Move(agent.desiredVelocity, false, false);
        }
        else if (Vector3.Distance(transform.position, waypoints[currentWaypoint].transform.position) <= 2)
        {
            currentWaypoint++;
            //character.Move(character.transform.forward, false, false);
            if (currentWaypoint >= waypoints.Length)
                currentWaypoint = 0;
        }
        else
            character.Move(Vector3.zero, false, false);
    }

    void Chase()
    {
        agent.speed = chaseSpeed;
        character.Move(agent.desiredVelocity, false, false);
        agent.stoppingDistance = safeZone;
        FaceTarget();
        agent.SetDestination(target.transform.position);
        //StopPosition();
    }

    IEnumerator ExcuteAttack()
    {
        yield return new WaitForSeconds(Random.Range(3, 6));
        int decideValue = Random.Range(0, 2);
        switch (decideValue)
        {
            case 0:
                character.SingleAttack();
                break;
            case 1:
                character.MultipleAttack();
                break;
        }
    }

    private void Attack(bool canAttack)
    {
        if (canAttack)
        {
            attackCooldown -= Time.deltaTime;
            if (attackCooldown <= 0)
            {
                StartCoroutine(ExcuteAttack());
                attackCooldown = 4f / attackSpeed;
            }
        }
    }

    void ApproachEnemy()
    {
        agent.speed = 0.5f;
        character.Move(agent.desiredVelocity, false, false);
        agent.stoppingDistance = safeZone;
        agent.SetDestination(target.transform.position);
    }

    void StrafeLeft()
    {
        character.Move(Vector3.left, false, false);
        //agent.SetDestination(-transform.right);
    }

    void StrafeRight()
    {
        character.Move(Vector3.right, false, false);
        //agent.SetDestination(transform.right);
    }

    void Backward()
    {
        character.Move(Vector3.back, false, false);
        //agent.SetDestination(-transform.forward);
    }

    void Forward()
    {
        character.Move(Vector3.forward, false, false);
        //agent.SetDestination(transform.forward);
    }

    void MoveAround()
    {
        if (distanceToTarget < safeZone)
        {
            character.Move(Vector3.back, false, false);
            //agent.SetDestination(-transform.forward);
        }

        if (distanceToTarget >= attackZone)
        {
            Forward();
        }
    }

    void Fight()
    {
        StopPosition();
        agent.speed = patrolSpeed;
        if (targetStats.alive)
        {
            FaceTarget();
            if (distanceToTarget <= attackZone)
            {
                //ApproachEnemy();
                //character.Move(-transform.forward, false, false);
                MoveAround();
                Attack(true);
            }
        }
        else
            state = State.PATROL;
    }

    void StopPosition()
    {
        if (agent.remainingDistance >= agent.stoppingDistance + 1f)
        {
            character.Move(agent.desiredVelocity, false, false);
            agent.SetDestination(target.transform.position);
        }
        else
            character.Move(Vector3.zero, false, false);
    }

    IEnumerator FSM()
    {
        while (myStats.alive)
        {
            switch (state)
            {
                case State.PATROL:
                    Patrol();
                    break;
                case State.CHASE:
                    Chase();
                    break;
                case State.FIGHT:
                    Fight();
                    break;
            }
            if (!myStats.alive)
            {
                if (state == State.DEATH)
                    Death();
            }
            yield return null;
        }
    }

    public NavMeshAgent GetMeshAgent()
        => agent;

    public GameObject GetTarget()
        => target;

    private void Death()
    {
        agent.stoppingDistance = 0f;
        agent.SetDestination(transform.position);
        StopAllCoroutines();
    }

    private void Update()
    {
        UpdateDistance();
        if (SpotOutTarget())
        {
            if (character.m_SheathSword)
                character.WithDrawSword();
            if (distanceToTarget <= safeZone)
                state = State.FIGHT;
            else
                state = State.CHASE;
        }
        else
            state = State.PATROL;
    }

    public void FaceTarget()
    {
        Vector3 direction;

        direction = (target.transform.position - transform.position).normalized;

        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    private bool SpotOutTarget()
    {
        if (distanceToTarget <= lookRadius)
        {
            target = GameObject.FindGameObjectWithTag("Player");
            targetStats = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterStats>();
            state = State.CHASE;
            return isEnemy = true;
        }
        return isEnemy = false;
    }

    private void UpdateDistance()
    {
        distanceToTarget = Vector3.Distance(GameObject.FindGameObjectWithTag("Player").transform.position, transform.position);
        //animator.SetFloat("distanceToTarget", distanceToTarget);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lookRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackZone);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, safeZone);
    }
}
