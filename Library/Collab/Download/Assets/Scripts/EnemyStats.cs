using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharacterStats
{
    IEnumerator WaitBeforeDestroyObject()
    {
        yield return new WaitForSeconds(5);

        Destroy(gameObject);
    }


    public override void Die()
    {
        base.Die();
        StartCoroutine(WaitBeforeDestroyObject());
    }
}
