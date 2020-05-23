using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCBaseFSM : StateMachineBehaviour
{
    [SerializeField] protected GameObject NPC;
    [SerializeField] protected float speed = 3.0f;
    [SerializeField] protected float patrolSpeed = 0.5f;
    [SerializeField] protected float rotSpeed = 1.0f;
    [SerializeField] protected float accuracy = 15.0f;
    [SerializeField] protected GameObject opponent;

    protected NavMeshAgent agent;
    protected ThirdPersonCharacter character;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        NPC = animator.gameObject;
        opponent = NPC.GetComponent<AIController>().GetTarget();
        agent = NPC.GetComponent<NavMeshAgent>();
        character = NPC.GetComponent<ThirdPersonCharacter>();
    }
}
