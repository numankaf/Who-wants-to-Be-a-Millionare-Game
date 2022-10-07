using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using UnityEngine.EventSystems;
public class Answers : MonoBehaviour
{
    public AudioSource adSource;
    public DBOpAudioData data;

    public  Button myButton;
    public  Image myImage;
    public bool isCorrect = false;
    public QuizManager game;
    public Sprite trueSprite;
    public Sprite falseSprite;
    public Sprite idle;
    public Sprite picked;
    public Jokers jokerManager;

    public void answerButton()
    {
        EventSystem.current.SetSelectedGameObject(null);
        if (isCorrect)
        {
            changeSprite(trueSprite);
        }
        else
        {
            changeSprite(falseSprite);
        }
    }

    void changeSprite(Sprite s)
    {
        myImage.sprite = picked;
        
        StartCoroutine(wait(2.2f,s));
    }
    private IEnumerator wait(float t,Sprite s)
    {
        if (jokerManager.is2xAactive==false)
        {
            game.setAllButtonsInactive();
        }
        game.setAllButtonsInactive();
        
        adSource.clip = data.pickedClips[Random.Range(0,data.pickedClips.Length)];
        adSource.Play();
        yield return new WaitForSeconds(1.8f);
        adSource.Stop();
        myImage.sprite = s;

        if (isCorrect)
        {
            adSource.clip = data.SuccessClips[Random.Range(0,data.SuccessClips.Length)];
            adSource.Play();    
        }
        else 
        {
            adSource.clip = data.wrongClips[Random.Range(0,data.wrongClips.Length)];
            adSource.Play();    
        }
        
        
        yield return new WaitForSeconds(t);
        adSource.Stop();
        if (isCorrect)
        {
            set();
        }
        else if (jokerManager.is2xAactive)
        { 
            //wait
          
            game.setAllButtonsReactive();
            jokerManager.is2xAactive = false;
        }
        else
        {
            set();
        }
       
        
    }


    void set()
    {
        jokerManager.is2xAactive = false;
        myImage.sprite = idle;
        if (isCorrect)
        {
            game.setAllButtonsInactive();
            game.setAllButtonsReactive();
            game.correctAnswerReplied();
            
        }
        else
        {
            
            game.gameOver();
        }
    }
}
