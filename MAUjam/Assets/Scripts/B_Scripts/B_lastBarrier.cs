using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B_lastBarrier : MonoBehaviour
{
    public GameObject barrier;
    void Start()
    {
        barrier.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            barrier.SetActive(true);
        }
    }
}
