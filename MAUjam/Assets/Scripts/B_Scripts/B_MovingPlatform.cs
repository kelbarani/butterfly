using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B_MovingPlatform : MonoBehaviour
{
    private float speed;
    private bool arrived;
    void Start()
    {
        
    }
    void Update()
    {
        if (!arrived)
        {
            transform.position = Vector2.right * speed * Time.deltaTime;
        }
        else
        {
            transform.position = Vector2.left * speed * Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "PatrolPoint")
        {
            arrived = !arrived;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            
        }
    }
}
