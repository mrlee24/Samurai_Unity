using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance = null;
    [SerializeField]
    private GameObject player;
    private void Awake()
    {
        //We are making sure, only one instance will exist
        if (instance == null) //If no instance is defined
            instance = this; //This is the instance!!
        else if (instance != this) //IF there is another one
            Destroy(gameObject); //Destroy yourself
    }
}
