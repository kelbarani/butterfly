using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.WSA;
public class B_Dialog : MonoBehaviour
{
    public TextMeshProUGUI textComp;

    public string[] lines;
    public float textSpeed;

    public float timer =3;
    public float counter;

    int index;
    public int questionIndex;
    public bool isSpeaking = false;

    public GameObject Buttons;
    
    void Start()
    {
        textComp.text = string.Empty;
        StartDialog();
    }

    void Update()
    {
        if (isSpeaking)
        {
            counter += Time.deltaTime;
        }
        
        if (counter >= timer)
        {
            counter = 0;
            StartCoroutine(NextLine());
        }

        if (index == questionIndex)
        {
            Buttons.SetActive(true);
            isSpeaking = false;
        }
    }

    void StartDialog()
    {
        index = 0;
        counter = timer - 0.1f;
    }

    IEnumerator TypeLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            textComp.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    IEnumerator NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            
            textComp.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            yield return new WaitForSeconds(1.5f);
            gameObject.SetActive(false);
        }
    }

    public void Answer(int answer)
    {
        
        index = answer;
        counter = timer;
        Buttons.SetActive(false);
         
    }

     public  void gettingAngry()
    {
        Debug.Log("KIZDI");
        transform.localScale = new Vector3(transform.localScale.x * 2, transform.localScale.y * 2, transform.localScale.z);

        
        GetComponent<EnemyAI>().enabled = true;                 
        GetComponent<EnemyHealth>().enabled = true;
    }
  
}
