using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    [SerializeField]
    private CharacterCombat combat;
    [SerializeField]
    private ThirdPersonCharacter character;
    [SerializeField]
    private CharacterStats targetStats;

    private void OnTriggerEnter(Collider other)
    {
        if(character.m_attack)
        {
            if (other.tag != character.gameObject.tag)
            {
                targetStats = other.GetComponent<CharacterStats>();

                if (targetStats.alive)
                {
                    combat.Attack(targetStats);
                    targetStats.GetComponent<ThirdPersonCharacter>().IsHit();
                }
                else
                    targetStats = null;
            }
        }
    }
}
