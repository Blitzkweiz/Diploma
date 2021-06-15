using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterController : MonoBehaviour
{
    public int currentHealth = 6;
    public int maxHealth = 6;
    public int damage;
    public float speed;

    protected virtual void Death()
    {
        Destroy(gameObject);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if(currentHealth <= 0)
        {
            Death();
        }
        Debug.Log($"Take {damage} damage");
    }
}
