using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class B_JungleEntry : MonoBehaviour
{
    public TextMeshProUGUI E;
    public GameObject player;
    public Transform jungleSpawnTransform;
    public bool onTrigger;
    void Start()
    {
        player = GameObject.Find("Player");
    }

  
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T) && onTrigger)
        {
            LevelChanger(player);
        }
    }

    void LevelChanger(GameObject collision)
    {
        collision.transform.position = jungleSpawnTransform.position;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            E.enabled = true;
            onTrigger = true;
        }
    }

   

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            E.enabled = false;
            onTrigger = false;
        }
      
    }
}
