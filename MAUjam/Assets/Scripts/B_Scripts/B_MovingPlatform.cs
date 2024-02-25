using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B_MovingPlatform : MonoBehaviour
{
    private float speed;
    private bool arrived;
   [SerializeField] private bool arrived =false;
    public float timer = 2f;
    private float counter;
   
    void Update()
    {
        counter += Time.deltaTime;

        if (counter >= timer)
        {
            arrived = !arrived;
            counter = 0;
        }
        if (!arrived)
        {
           
            transform.Translate(Vector2.up * speed * Time.deltaTime);
        }
        else if(arrived)
        {
            transform.Translate(Vector2.up * -speed * Time.deltaTime);
        }
    }

   
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.parent = null;
        }
    }
}
