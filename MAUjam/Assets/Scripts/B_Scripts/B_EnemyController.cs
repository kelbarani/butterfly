using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class B_EnemyController : MonoBehaviour
{
    //CheckPoint için
    GameObject player;
    public Transform[] spawnPoints;
    

    public GameObject level1, level2, level3,level4;
    public GameObject townSign;
    public GameObject platform01;

    public GameObject king;
    public GameObject villager;
    public GameObject girl;

    public TextMeshProUGUI villagerAsk_Text;
    public TextMeshProUGUI girlsWarning_Text;
    public TextMeshProUGUI tobecon;
    public Sprite happy_King;
    public Sprite villager_Asking;
    public Sprite girl_happy;

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

        villagerAsk_Text.enabled = false;
        girlsWarning_Text.enabled = false;
    }

    void Update()
    {
        player.GetComponent<PlayerHealth>().respawnPoint = spawnPoints[0];

        if (level1.transform.childCount == 0)
        {
            level2.SetActive(true);
            villager.GetComponent<SpriteRenderer>().sprite = villager_Asking;
            villagerAsk_Text.enabled = true;
            girl.SetActive(true);
            player.GetComponent<PlayerHealth>().respawnPoint = spawnPoints[1];


            if (level2.transform.childCount == 0)
            {
                level3.SetActive(true);
                platform01.SetActive(true);
                girl.GetComponent<SpriteRenderer>().sprite = girl_happy;
                girlsWarning_Text.enabled = true;

                villager.SetActive(false);
                player.GetComponent<PlayerHealth>().respawnPoint = spawnPoints[2];

                if (level3.transform.childCount == 0)
                {
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
