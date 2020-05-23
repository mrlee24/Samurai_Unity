using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCharacter : MonoBehaviour
{
    [SerializeField] private GameObject[] characters;

    private int selectedCharacter;

    void Start()
    {
        selectedCharacter = PlayerPrefs.GetInt("CharacterSelected");
        for(int i = 0; i < characters.Length; i++)
        {
            if (characters[i] == characters[selectedCharacter])
                characters[i].gameObject.SetActive(true);
            else
                characters[i].gameObject.SetActive(false);
        }
    }
}
