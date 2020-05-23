using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance = null;

    [SerializeField]
    private List<GameObject> enemies = new List<GameObject>();

    [SerializeField]
    private GameObject boss;

    private bool bossIsAlive = true;

    private void Awake()
    {
        //We are making sure, only one instance will exist
        if (instance == null) //If no instance is defined
            instance = this; //This is the instance!!
        else if (instance != this) //IF there is another one
            Destroy(gameObject); //Destroy yourself
    }

    private void Start()
    {
        enemies = GameObject.FindGameObjectsWithTag("enemy").ToList();
        enemies.Add(boss);
    }

    public bool BossIsAlive()
    {
        if (boss.GetComponent<CharacterStats>().alive)
            return bossIsAlive = true;
        else
            return bossIsAlive = false;
    }


    public List<GameObject> GetEnemies()
        => enemies;

}
