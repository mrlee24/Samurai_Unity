using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    [SerializeField] EnemyManager enemyManager;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject win;

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
        Cursor.lockState = CursorLockMode.Locked;
        win.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetButtonUp("Cancel")) //If we press "ESC"
        {
            //isPaused = true;
            Time.timeScale = 0f;
            pauseMenu.SetActive(true);
        }
        if(!enemyManager.BossIsAlive())
        {
            win.SetActive(true);
            Time.timeScale = 0f;
        }
    }
    public void Resume()
    {
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
    }
}
