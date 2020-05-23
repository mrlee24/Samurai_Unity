using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelectorUI : MonoBehaviour
{
    [SerializeField] private CharacterSelector selector;
    [SerializeField] private GameObject WarningPanel;
    [SerializeField] private LevelLoader levelLoader;
    [SerializeField] private int sceneIndex;

    public void Forward()
        => selector.NextCharacter();

    public void Previous()
        => selector.PreviousCharacter();

    public void PlayGame()
    {
        PlayerPrefs.SetInt("CharacterSelected", selector.SelectedCharacter());
        levelLoader.LoadLevel(sceneIndex);
    }

    public void DisplayWarning()
        => WarningPanel.SetActive(true);

    public void ReturnMainMenu()
        => SceneManager.LoadScene(0);
}
