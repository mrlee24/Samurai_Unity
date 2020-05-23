using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class CharacterCombat : MonoBehaviour
{
    CharacterStats myStats;

    

    void Start()
    {
        myStats = GetComponent<CharacterStats>();
    }

    public void Attack(CharacterStats targetStats)
    {
        if(targetStats.alive)
            DoDamage(targetStats);
    }

    public void Defend()
    {
        int value = Random.Range(0, 11);
        animator.PerformDefend();
        DoDamage(myStats);
    }

    public void DoDamage(CharacterStats stats)
        => stats.TakeDamage(myStats.damage.GetValue());
}
