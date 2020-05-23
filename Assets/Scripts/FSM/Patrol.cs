using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : NPCBaseFSM
{
    [SerializeField]GameObject[] patrolPoints;
    int currentPoint;

    private void Awake()
    {
        patrolPoints = GameObject.FindGameObjectsWithTag("PatrolPoint");
    }

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        currentPoint = 0;
    }

    IEnumerator WaitAWhile()
    {
        yield return new WaitForSecondsRealtime(2);
        character.Move(agent.desiredVelocity, false, false);
        agent.SetDestination(patrolPoints[currentPoint].transform.position);
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.speed = patrolSpeed;
        if (patrolPoints.Length == 0) return;

        Debug.Log(Vector3.Distance(patrolPoints[currentPoint].transform.position, NPC.transform.position));

        if(Vector3.Distance(NPC.transform.position, patrolPoints[currentPoint].transform.position) >= accuracy)
            {
                agent.SetDestination(patrolPoints[currentPoint].transform.position);
                character.Move(agent.desiredVelocity, false, false);
            }
       else if (Vector3.Distance(NPC.transform.position, patrolPoints[currentPoint].transform.position) <= accuracy)
        {
            currentPoint++;
            //character.Move(character.transform.forward, false, false);
            Debug.Log("Next point: " + currentPoint);
            if (currentPoint >= patrolPoints.Length)
                currentPoint = 0;
        }
        else
        {
            character.Move(Vector3.zero, false, false);
        }

        //Rotate towars target
        //var direction = patrolPoints[currentPoint].transform.position - NPC.transform.position;
        //Lock target
        //NPC.transform.rotation = Quaternion.Slerp(NPC.transform.rotation,
        //    Quaternion.LookRotation(direction), rotSpeed * Time.deltaTime);
        //character.Move(agent.desiredVelocity, false, false);
        //agent.stoppingDistance = 5f;
        //character.StartCoroutine(WaitAWhile());
        //agent.SetDestination(patrolPoints[currentPoint].transform.position);
        //agent.SetDestination(patrolPoints[currentPoint].transform.position);
        //NPC.transform.Translate(0, 0, Time.deltaTime * speed);
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    }

}
