using UnityEngine;
using System.Collections;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField]
    private SettingsMenu settings;

    private static MusicPlayer instance = null;

    public static MusicPlayer Instance
    {
        get { return instance; }
    }

    void Awake()
    {
        Time.timeScale = 1f;
        SceneManager.sceneLoaded += OnSceneLoad;
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        settings.SetVolume(PlayerPrefs.GetFloat("masterVol"));
        settings.SetVolumeSFX(PlayerPrefs.GetFloat("sfxVol"));
    }

    void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == 2)
        {
            Destroy(gameObject);
        }
    }
}