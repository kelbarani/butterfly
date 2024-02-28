using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class B_EnemyController : MonoBehaviour
{
    //CheckPoint i�in
    GameObject player;
    public Transform[] spawnPoints;

    [Header("Leveller")]
    public GameObject level1;
    public GameObject level2;
    public GameObject level3;
    public GameObject level4;

    [Header("Objects")]
    public GameObject platform01;
    public GameObject platform02;

    [Header("Characters")]
    public GameObject king;
    public GameObject villager;
    public GameObject girl;

    public Sprite happy_King;
    public Sprite villager_Asking;
    public Sprite girl_happy;

    [Header("Texts")]
    public GameObject villagers_Dialog;
    public GameObject girls_Dialog;
    public GameObject kings_Dialog;

    public TextMeshProUGUI tobecon;
   

    private void Awake()
    {
        player = GameObject.Find("Player");
    }
    void Start()
    {
        level2.SetActive(false);
        level3.SetActive(false);
        level4.SetActive(false);

        girl.SetActive(false);

        villagers_Dialog.SetActive(false);
        girls_Dialog.SetActive(false);
    }

    void Update()
    {
        player.GetComponent<PlayerHealth>().respawnPoint = spawnPoints[0];

        if (level1.transform.childCount == 0)
        {
            level2.SetActive(true);
            villager.GetComponent<SpriteRenderer>().sprite = villager_Asking;
            villagers_Dialog.SetActive(true);
            if(girl!=null)girl.SetActive(true);
            player.GetComponent<PlayerHealth>().respawnPoint = spawnPoints[1];


            if (level2.transform.childCount == 0)
            {
                level3.SetActive(true);
                platform01.SetActive(true);
                if(girl!=null)girl.GetComponent<SpriteRenderer>().sprite = girl_happy;
                girls_Dialog.SetActive(true);

                villager.SetActive(false);
                player.GetComponent<PlayerHealth>().respawnPoint = spawnPoints[2];

                if (level3.transform.childCount == 0)
                {
                    platform02.SetActive(true);
                    level4.SetActive(true);
                    player.GetComponent<PlayerHealth>().respawnPoint = spawnPoints[3];
                    if (level4.transform.childCount == 0)
                    {
                        StartCoroutine(Win());
                    }
                }
            }
        }
    }
   

    IEnumerator Win()
    {
        king.GetComponent<SpriteRenderer>().sprite = happy_King;
        tobecon.enabled = true;
        yield return new WaitForSeconds(4f);
        Application.Quit();

    }
}
