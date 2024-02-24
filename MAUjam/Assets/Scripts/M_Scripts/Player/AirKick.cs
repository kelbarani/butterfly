using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirKick : MonoBehaviour
{
    [SerializeField] private float airKickDamage=30f;
    private CircleCollider2D airKickCollider;
    
    void Start()
    {
        airKickCollider = GetComponent<CircleCollider2D>();
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.TakeDamage(airKickDamage);
        }
    }
}
