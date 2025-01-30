using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public int health = 50;

    private AudioSource src;

    private Material og;
    public Material red;

    public GameObject orb;

    void Awake()
    {
        src = gameObject.GetComponent<AudioSource>();
    }

    public void TakeDamage(int dmg)
    {
        int rand = Convert.ToInt32(Mathf.Round(UnityEngine.Random.Range(1f, 4f)));

        audioManager.Instance.PlaySFX("EnemyDmg" + rand, src);

        health -= dmg;
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        int rand = UnityEngine.Random.Range(1,5);
        Vector3 add = new Vector3(0, 1, 0);
        if (rand == 1)
        {
            GameObject spawn = Instantiate(orb, gameObject.transform.position + add, Quaternion.identity);
        }

        StateManager.Instance.enemyCount--;
        Destroy(gameObject);
    }

}
