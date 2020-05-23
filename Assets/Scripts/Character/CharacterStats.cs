﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterStats : MonoBehaviour
{
    [SerializeField]
    private int maxHealth;

    [SerializeField]
    private Slider healthBar;

    protected AIController ai;
    protected ThirdPersonCharacter character;
    public int currentHealth { get; set; }

    public Stat damage;
    public Stat armor;

    public bool alive;

    private void Start()
    {
        ai = GetComponent<AIController>();
        character = GetComponent<ThirdPersonCharacter>();
        healthBar.value = maxHealth;
        alive = true;
    }

    public void SetHp()
        => currentHealth = maxHealth;

    private void Awake()
        => SetHp();

    public void TakeDamage(int damage)
    {
        currentHealth -= armor.GetValue();
        damage = Mathf.Clamp(damage, 0, int.MaxValue);

        currentHealth -= damage;
        healthBar.value = currentHealth;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        alive = false;
        character.IsKilled();
        ai.enabled = false;
        character.enabled = false;
        Debug.Log("Die");
    }
}
