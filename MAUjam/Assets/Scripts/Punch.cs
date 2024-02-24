using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Punch : MonoBehaviour
{
    private CircleCollider2D punchCollider;
    [SerializeField] private float punchDamage=20f;
    
    private void Awake()
    {
        punchCollider = GetComponent<CircleCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.TakeDamage(punchDamage);
        }
    }
    
    
}
