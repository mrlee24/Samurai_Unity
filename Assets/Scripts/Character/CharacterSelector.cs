using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelector : MonoBehaviour
{

    [SerializeField] private GameObject[] characters;
    [SerializeField] private GameObject swordCover;
    [SerializeField] private int currentCharacter;
    [SerializeField] private int previousCharacter;
    [SerializeField] private Animator animator;

    private void Start()
    {
        previousCharacter = currentCharacter;
        characters[currentCharacter].SetActive(true);
        swordCover.SetActive(true);
        animator.Play("Locomation");
    }

    private void Update()
    {
        if (currentCharacter > characters.Length - 1)
        {
            previousCharacter = characters.Length - 1;
            currentCharacter = 0;

            characters[previousCharacter].SetActive(false);
            characters[currentCharacter].SetActive(true);
        }
        else if (currentCharacter < 0)
        {
            previousCharacter = 0;
            currentCharacter = characters.Length - 1;

            characters[previousCharacter].SetActive(false);
            characters[currentCharacter].SetActive(true);
        }
    }

    public void NextCharacter()
    {
        currentCharacter += 1;
        previousCharacter = currentCharacter - 1;
        characters[previousCharacter].SetActive(false);
        characters[currentCharacter].SetActive(true);
        animator.Play("Locomation");
    }

    public void PreviousCharacter()
    {
        currentCharacter -= 1;
        previousCharacter = currentCharacter + 1;
        characters[previousCharacter].SetActive(false);
        characters[currentCharacter].SetActive(true);
        animator.Play("Locomation");
    }

    public int SelectedCharacter()
        => currentCharacter;
}
