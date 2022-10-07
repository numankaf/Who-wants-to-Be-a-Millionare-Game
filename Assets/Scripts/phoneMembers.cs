using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class phoneMembers : MonoBehaviour
{
    public QuizManager game;
    
    public Button phone;
    public Sprite deadPhone;
    public GameObject member;
    public List<GameObject> options;
    public Text trueAnswer;
    public GameObject call;
    public String privateAnswerTrue;
    public String privateAnswerfalse;
    public float luck;
    public float lastLuck;
    
   // private bool[] changes = { false,false,false,false,true,true,true,true,false,false};
    //private Text[] falseAnswers;
    // private int m = 0;
   
    public Text resultingText;
    
    
    public int getPercentage()
    {
        int perCent = Random.Range(0, 100);
        return perCent;
    }

    public void presButton()
    {
        StartCoroutine(callPressed());
    }

    private IEnumerator callPressed()
    {
        yield return new WaitForSeconds(2.5f);
        pressed();
    }
    
    public  void pressed()
    {
        
        
        phone.enabled = false;
        phone.GetComponent<Image>().sprite = deadPhone;
       
        member.SetActive(true);
     

        foreach (var k in options)
        {
            if (k.GetComponent<Answers>().isCorrect == true)
            {
                trueAnswer.text = k.transform.GetChild(0).GetComponent<Text>().text;
            }
            else
            {
                // falseAnswers[m].text = k.transform.GetChild(0).GetComponent<Text>().text;
                // m++;
            }
        }

        int  change = getPercentage();
         lastLuck = luck - (luck*0.025f * game.currentIndex);
        if (change<lastLuck)
        {
            resultingText.text =privateAnswerTrue+ trueAnswer.text;
            StartCoroutine(closeEverything());
        }
        else
        {
            resultingText.text = privateAnswerfalse;
            StartCoroutine(closeEverything());

        }
    }

    IEnumerator  closeEverything()
    {
        yield return new WaitForSeconds(3f);
        member.SetActive(false);
        call.SetActive(false);
        
    }
    
}
