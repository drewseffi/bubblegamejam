using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public TMP_Text healthUI;

    private AudioSource src;

    void Start()
    {
        currentHealth = maxHealth;
        src = gameObject.GetComponent<AudioSource>();
    }

    void Update()
    {
        if (currentHealth <= 0)
        {
            audioManager.Instance.PlaySFX("PlayerDeath", src);
            StateManager.Instance.Die();
        }

        healthUI.text = currentHealth.ToString();
    }

    public void TakeDamage(int dmg)
    {
        currentHealth = currentHealth - dmg;
        if (currentHealth > 0)
        {
            audioManager.Instance.PlaySFX("PlayerHit1", src);
        }
    }
}
