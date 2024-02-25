using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B_EnemyController : MonoBehaviour
{
    public GameObject level1, level2, level3;
    public GameObject king;

    public Sprite happy_King;

    void Start()
    {
        level2.SetActive(false);
        level3.SetActive(false);
    }

    void Update()
    {
        int enemyLvl1Count = CountObjectsWithName("Enemy lvl1");
        int enemyLvl2Count = CountObjectsWithName("Enemy lvl2");

        if (enemyLvl1Count == 0)
        {
            level2.SetActive(true);

            // Check if level 2 has no children
            if (level2.transform.childCount == 0)
            {
                level3.SetActive(true);

                // Check if level 3 has no children
                if (level3.transform.childCount == 0)
                {
                    Win();
                }
            }
        }
    }

    int CountObjectsWithName(string objectName)
    {
        int count = 0;

        // Count the objects with the specified name among the children of this GameObject
        foreach (Transform child in transform)
        {
            if (child.name == objectName)
            {
                count++;
            }
        }

        return count;
    }

    public void Win()
    {
        king.GetComponent<SpriteRenderer>().sprite = happy_King;
    }
}
