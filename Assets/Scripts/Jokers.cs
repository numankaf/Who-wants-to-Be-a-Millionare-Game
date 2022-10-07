using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Jokers : MonoBehaviour
{
    public Jokers jokerManager;
    public QuizManager game;
    public List<GameObject> falsess;
    public Button jok50;
    public Image jok50image;
    public Sprite Jok50deadSprite;
    public List<Answers> answerScript;
    public bool is2xAactive = false;

    public Button jok2x;
    public Sprite jok2xDead;

    public void joker50()
    {
        for (int i =0;i<falsess.Count;i++)
        {
            if (falsess[i].GetComponent<Answers>().isCorrect==true)
            {
                falsess.RemoveAt(i);
            }
        }

        int k = Random.Range(0, falsess.Count);
        GameObject f1=falsess[k]; falsess.RemoveAt(k);
        k=Random.Range(0, falsess.Count);
        GameObject f2=falsess[k];
        f1.SetActive(false);
        f2.SetActive(false);
        jok50.enabled = false;
        jok50image.sprite = Jok50deadSprite;

    }

    public void joker2X()
    {
        jokerManager.is2xAactive = true;
        jok2x.enabled = false;
        jok2x.GetComponent<Image>().sprite = jok2xDead;

    }
}
