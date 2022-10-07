using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
public class wiever : MonoBehaviour
{
    public QuizManager game;
    private int trueAnswer;
    public GameObject wieverPanel;
    public Sprite tureImage;
    public Sprite[] falseBars;
    public Image[] bars;
    public Button wieverJker;
    public Sprite deadWiever;
    private int luck=70;
    public Animator an;

    
    public void setTheAnswer()
    {
        
        int change = Random.Range(0, 100);
        if (change < luck)
        {
            for (int i = 0; i < game.options.Length; i++)
            {
                if (game.options[i].GetComponent<Answers>().isCorrect)
                {
                    trueAnswer = i;
                }
            }
        }
        else
        {
            trueAnswer = Random.Range(0,4);
        }
    }
    
    public void pressed()
    { 
        wieverJker.enabled=false;
       
        wieverPanel.SetActive(true);
        wieverJker.GetComponent<Image>().sprite = deadWiever;
        setTheAnswer();
        StartCoroutine(playAni());
        
    }

   
    public IEnumerator  closeEverything()
    {
        for (int i = 0; i < bars.Length; i++)
        {
            if (i == trueAnswer)
            {
                bars[i].sprite = tureImage;
                Debug.Log("true answer found "+i);
            }
            else
            {
                bars[i].sprite = falseBars[Random.Range(0, falseBars.Length)];
                
            }
        }
        
        yield return new WaitForSeconds(6f);
       
        wieverPanel.SetActive(false);
        
    }

    IEnumerator playAni()
    {
        yield return new WaitForSeconds(2f);
        an.SetBool("exit",false);
        yield return new WaitForSeconds(2f);
        wieverPanel.GetComponent<Animator>().enabled= false;
        StartCoroutine(closeEverything());
        //setTheAnswer();
    }
}
