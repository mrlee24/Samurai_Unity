using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAI : MonoBehaviour
{
    [SerializeField]
    private float lookRadius = 15f;

    [SerializeField]
    private float attackZone = 5f;

    [SerializeField]
    private float safeZone = 2.5f;


    [SerializeField] protected float distanceToTarget;
    [SerializeField] protected Vector3 targetPosition;
    //[SerializeField] protected Interactable targetInteractable;

    [SerializeField] private EnemyManager enemyManager;

    private ThirdPersonCharacter character;
    private CharacterStats targetStats;
    private CharacterStats myStats;

    private void Start()
    {
        character = GetComponent<ThirdPersonCharacter>();
        myStats = GetComponent<CharacterStats>();
    }

    private void Update()
    {
        distanceToTarget = Vector3.Distance(GameObject.FindGameObjectWithTag("enemy").transform.position, transform.position);
    }

    public void FaceTarget()
    {
       
        if (distanceToTarget < 8)
        {
            targetStats = GameObject.FindGameObjectWithTag("enemy").GetComponent<CharacterStats>();
            Vector3 direction = (targetStats.transform.position - transform.position).normalized;

            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);

        }
    }

    public bool EnemyIsDetected()
    {
        distanceToTarget = Vector3.Distance(GameObject.FindGameObjectWithTag("enemy").transform.position, transform.position);

        if (distanceToTarget <= lookRadius)
        {
            targetStats = GameObject.FindGameObjectWithTag("enemy").GetComponent<CharacterStats>();

            targetPosition = GameObject.FindGameObjectWithTag("enemy").transform.position;
            return true;
        }
        return false;
    }
}
