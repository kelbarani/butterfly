using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class B_EnemyController : MonoBehaviour
{
    public GameObject level1, level2, level3,level4;
    public GameObject townSign;
    public GameObject platform;

    public GameObject king;
    public GameObject villager;
    public GameObject girl;

    public TextMeshProUGUI villagerAsk_Text;
    public TextMeshProUGUI girlsWarning_Text;
    public Sprite happy_King;
    public Sprite villager_Asking;
    public Sprite girl_happy;

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
       

        if (level1.transform.childCount == 0)
        {
            level2.SetActive(true);
            villager.GetComponent<SpriteRenderer>().sprite = villager_Asking;
            villagerAsk_Text.enabled = true;
            girl.SetActive(true);

            
            if (level2.transform.childCount == 0)
            {
                level3.SetActive(true);

                girl.GetComponent<SpriteRenderer>().sprite = girl_happy;
                girlsWarning_Text.enabled = true;

                villager.SetActive(false);
               
                if (level3.transform.childCount == 0)
                {
                    level4.SetActive(true);
                    if (level4.transform.childCount == 0)
                    {
                        Win();
                    }
                }
            }
        }
    }
    public void Win()
    {
        king.GetComponent<SpriteRenderer>().sprite = happy_King;
    }
}
