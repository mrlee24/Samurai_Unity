using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : CharacterStats
{
    IEnumerator OnWait()
    {
        yield return new WaitForSecondsRealtime(5f);

        SceneManager.LoadScene("Scene0");
    }
    public override void Die()
    {
        base.Die();

        StartCoroutine(OnWait());
    }
}
