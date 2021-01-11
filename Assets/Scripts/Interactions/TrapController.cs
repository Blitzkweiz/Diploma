using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapController : MonoBehaviour
{
    public int damage;
    public float damageDelay;

    private CharacterController characterController;
    private float lastDamage;

    void OnTriggerStay2D(Collider2D col)
    {
        if(col.CompareTag("Player") && lastDamage + damageDelay < Time.time && col.gameObject.TryGetComponent<CharacterController>(out characterController))
        {
            characterController.TakeDamage(damage);
            lastDamage = Time.time;
        }
    }
}
