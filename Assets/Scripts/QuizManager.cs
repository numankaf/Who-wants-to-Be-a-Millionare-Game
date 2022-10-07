using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class QuizManager : MonoBehaviour
{
    public QuestionDB tier1;
    public QuestionDB tier2;
    public QuestionDB tier3;
    public QuestionDB tier4;
    public QuestionDB tier5;
    public QuestionDB GodTier;


    public ParticleSystem ps;

    public AudioSource adSource;
    public DBOpAudioData audioData;

    public CharacterDB CharacterDB;
    public GameObject player;
    private Sprite playerSprite;

    public GameObject x2joker;

    public Sprite idleButton;
    public List<Question> Questions = new List<Question>();
    public GameObject[] options;
    public Text QuestionText;
    public int currentQuestion;
    public GameObject QuizPanel;
    public GameObject GoPanel;
    public GameObject PricePanel;
    public GameObject JokerPanel;
    public GameObject failPanel;
    public GameObject game;
    public GameObject successPanel;
    public GameObject wieverPanel;
    public TextMeshProUGUI ScoreText;
    public int score = 0;
    public Image[] pickeds;

    public Sprite[] successPanelsSprites;
    public Text successText;

    private String[] successTextes =
        {"HELAL LA SANA", "POLİS MİSİN LA", "NE DİYÜĞN LA SEN", "ALFA AMQ", "200IQ?", "İYİSİN İYİ"};

    public AudioSource bgMuisc;
    public GameObject wonPanel;
    public TextMeshProUGUI wontext;


    private int[] points =
        {500, 1000, 2000, 3000, 5000, 7500, 15000, 25000, 50000, 75000, 150000, 250000, 500000, 1000000};

    public int currentIndex = 0;

    List<Question> setQuestions(int tierGrade)
    {
        QuestionDB tier;
        if (tierGrade >= 12)
        {
            tier = GodTier;
        }
        else if (tierGrade >= 10)
        {
            tier = tier5;
        }
        else if (tierGrade >= 8)
        {
            tier = tier4;
        }
        else if (tierGrade >= 5)
        {
            tier = tier3;
        }
        else if (tierGrade >= 2)
        {
            tier = tier2;
        }
        else
        {
            tier = tier1;
        }

        List<Question> qs = new List<Question>();
        for (int i = 0; i < tier.questions.Length; i++)
        {
            qs.Add(tier.questions[i]);
        }

        return qs;
    }


    public void Start()
    {
        ps.Stop();
        player.SetActive(true);
        int selectedCharacterInteger;
        if (!PlayerPrefs.HasKey("selected"))
        {
            selectedCharacterInteger = 0;
        }
        else
        {
            selectedCharacterInteger = PlayerPrefs.GetInt("selected");
            playerSprite = CharacterDB.arr[selectedCharacterInteger].chacarterSprite;
            player.GetComponent<Image>().sprite = playerSprite;
        }
        Questions = setQuestions(currentIndex);
        GoPanel.SetActive(false);
        generateQuestion();
        foreach (var ele in pickeds)
        {
            ele.enabled = false;
        }

        pickeds[0].enabled = true;
    }

    public void setAllButtonsInactive()
    {
        for (int i = 0; i < options.Length; i++)
        {
            options[i].transform.GetComponent<Button>().enabled = false;
        }
    }

    public void setAllButtonsReactive()
    {
        for (int i = 0; i < options.Length; i++)
        {
            options[i].transform.GetComponent<Button>().enabled = true;
        }
    }

    public void retry()
    {
        StartCoroutine(callRetry());
    }

    private IEnumerator callRetry()
    {
        adSource.clip = audioData.retryButton;
        adSource.Play();
        yield return new WaitForSeconds(2f);
        adSource.Stop();
        SceneManager.LoadScene("Game");
    }

    public void returnToMainMenu()
    {
        StartCoroutine(callReturnToMainMenu());
    }


    private IEnumerator callReturnToMainMenu()
    {
        adSource.clip = audioData.mainMenuButton;
        adSource.Play();
        yield return new WaitForSeconds(0.5f);
        adSource.Stop();
        SceneManager.LoadScene("Menu");
    }

    public void gameOver()
    {
        if (currentIndex == points.Length)
        {
            failPanel.SetActive(false);
            QuizPanel.SetActive(false);
            JokerPanel.SetActive(false);
            PricePanel.SetActive(false);
            player.SetActive(false);
            GoPanel.SetActive(true);
            ScoreText.text = "Kazanılan Para : " + score.ToString();
        }
        else
        {
            StartCoroutine(failedAraSahne());
        }
    }

    public IEnumerator failedAraSahne()
    {
        game.SetActive(false);
        failPanel.SetActive(true);

        yield return new WaitForSeconds(2f);
        game.SetActive(true);
        failPanel.SetActive(false);
        QuizPanel.SetActive(false);
        JokerPanel.SetActive(false);
        PricePanel.SetActive(false);
        player.SetActive(false);
        GoPanel.SetActive(true);
        ScoreText.text = "Kazanılan Para : " + score.ToString();
    }

    public IEnumerator wonTheGame()
    {
        game.SetActive(false);
        bgMuisc.Stop();
        wonPanel.SetActive(true);
        wontext.enabled = false;
        yield return new WaitForSeconds(10f);
        wontext.enabled = true;
        yield return new WaitForSeconds(18f);
        wonPanel.SetActive(false);
        game.SetActive(true);
        failPanel.SetActive(false);
        QuizPanel.SetActive(false);
        JokerPanel.SetActive(false);
        PricePanel.SetActive(false);
        player.SetActive(false);
        GoPanel.SetActive(true);
        ScoreText.text = "Kazanılan Para : " + score.ToString();
    }


    public void correctAnswerReplied()
    {
        wieverPanel.SetActive(false);

        if (score == 15000)
        {
            x2joker.SetActive(true);
        }

        foreach (var el in options)
        {
            el.SetActive(true);
        }


        if (currentIndex < 13)
        {
            pickeds[currentIndex + 1].enabled = true;
            pickeds[currentIndex].enabled = false;
        }
        else
        {
            //do nothing
        }

        if (currentIndex == 2 || currentIndex == 5 || currentIndex == 8 || currentIndex == 10 || currentIndex == 12)
        {
            Questions = setQuestions(currentIndex);
        }
        else
        {
            //do nothing
        }

        score = points[currentIndex];
        currentIndex++;

        if (currentIndex == points.Length)
        {
            StartCoroutine(wonTheGame());
        }
        else
        {
            StartCoroutine(successedAraSahne());
        }
    }

    public IEnumerator successedAraSahne()
    {
        successPanel.GetComponentInChildren<Image>().sprite =
            successPanelsSprites[Random.Range(0, successPanelsSprites.Length)];
        successText.text = successTextes[Random.Range(0, successTextes.Length)];
        ps.Play();
        game.SetActive(false);
        successPanel.SetActive(true);
        yield return new WaitForSeconds(2.7f);
        game.SetActive(true);
        successPanel.SetActive(false);
        generateNextQ();
        ps.Stop();
    }


    public void generateAnswers()
    {
        String[] op = {"A . ", "B . ", "C . ", "D . "};
        for (int i = 0; i < options.Length; i++)
        {
            options[i].GetComponent<Answers>().isCorrect = false;
            options[i].transform.GetChild(0).GetComponent<Text>().text = op[i] + Questions[currentQuestion].answers[i];
            if (Questions[currentQuestion].correctAnswer == i)
            {
                options[i].GetComponent<Answers>().isCorrect = true;
            }
        }
    }

    public void generateQuestion()
    {
        currentQuestion = Random.Range(0, Questions.Count);
        QuestionText.text = Questions[currentQuestion].question;
        generateAnswers();
    }

    public void generateNextQ()
    {
        Questions.RemoveAt(currentQuestion);
        setIdleToButtons();
        generateQuestion();
    }

    void setIdleToButtons()
    {
        foreach (var ele in options)
        {
            ele.transform.GetComponent<Image>().sprite = idleButton;
        }
    }
}