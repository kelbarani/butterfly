using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirKick : MonoBehaviour
{
    [SerializeField] private float airKickDamage=30f;
    private CircleCollider2D airKickCollider;
    private AudioSource audioSource;
    public AudioClip punchBodySound;
    void Start()
    {
        airKickCollider = GetComponent<CircleCollider2D>();
        audioSource = GetComponent<AudioSource>();
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
        if (damageable != null)
        {
            audioSource.PlayOneShot(punchBodySound);
            damageable.TakeDamage(airKickDamage);
        }
    }
}
