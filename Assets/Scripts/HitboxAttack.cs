using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxAttack : MonoBehaviour
{
    public int damage = 10;
    private bool canAttack;

    private void OnEnable()
    {
        canAttack = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        var hit = other.GetComponent<PlayerHealth>();
        
        if (hit != null && canAttack)
        {
            hit.TakeDamage(10);
            canAttack = false;
            Wait();
            canAttack = true;
        }
    }

    IEnumerable Wait()
    {
        yield return new WaitForSeconds(1f);
    }
}
