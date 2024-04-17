using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEditor;

public class QuizManager : MonoBehaviour
{
    private PointsManager pointsManager;

    public AudioManager AudioManager;
    public List<QuestionAndAnswers> QnA;
    public GameObject[] options;
    public int currentQuestion;

    [SerializeField] GameObject Quizpanel, GoPanel, ClearedPanel, confirmationPanel, timerPanel, startPanel, hintPanel, restartGamePanel;
 
    //Buttons (para sa pagdisable)
    [SerializeField] Button buttonOne, buttonTwo, buttonThree, buttonFour, hintButton;

    [SerializeField] TextMeshProUGUI QuestionTxt, ScoreTxt, roomInfoText, hintsLeftText, attemptsLeftText;

    int totalQuestions = 0;
    public int pointsToGive = 5;
    int score;

    // Timer
    [Header("Timer")]
    float currentTime = 0f;
    public bool timeRunning;
    public float startingTime = 10f;
    public TextMeshProUGUI timerText;
    private bool soundPlayed = false;

    // Hint System
    [Header("Hint System")]
    public Color startColor;
    public int maxHintUsage = 3;
    private int hintCount;

    // Attempts
    [Header("Attempt System")]
    public int maxAttempts = 3;
    public int attemptsRemaining;

    public int repPoints { get; private set; }

    int roomTwoPoints = 15;
    int roomThreePoints = 20;
    int roomFourPoints = 25;
    int roomFivePoints = 30;


    private void Start()
    {
        pointsManager = PointsManager.Instance;

        totalQuestions = QnA.Count;

        startPanel.SetActive(true);
        Quizpanel.SetActive(false);
        hintPanel.SetActive(false);

        currentTime = startingTime;
        InitializeAttemptsRemaining();

        hintCount = maxHintUsage;
        hintsLeftText.text = "Hints left: " + maxHintUsage.ToString();

        UpdatePointsToGive();
        roomInfoText.text = "Hint: " + maxHintUsage.ToString() + "\nTime: " + startingTime.ToString() + "\nREP Points: " + pointsToGive + "\nAttempt left: " + attemptsRemaining;

        //PlayerPrefs.SetInt("REPpoints", repPoints);
        //PlayerPrefs.Save();
    }

    public void UpdatePointsToGive()
    {
        int currentBuildIndex = SceneManager.GetActiveScene().buildIndex;

        switch (currentBuildIndex)
        {
            case 3:
                pointsToGive = 10;
                break;
            case 4:
                pointsToGive = roomTwoPoints;
                break;
            case 5:
                pointsToGive = roomThreePoints;
                break;
            case 6:
                pointsToGive = roomFourPoints;
                break;
            case 7:
                pointsToGive = roomFivePoints;
                break;
            default:
                pointsToGive = 10;
                break;
        }
    }

    private void InitializeAttemptsRemaining()
    {
        if (PlayerPrefs.HasKey("AttemptsRemaining"))
        {
            Debug.Log("Has Key");
            attemptsRemaining = PlayerPrefs.GetInt("AttemptsRemaining");
            Debug.Log("Attempts remaining: " + attemptsRemaining);
        }
        else
        {
            Debug.Log("Saved");
            attemptsRemaining = maxAttempts;
            SaveAttemptsRemaining();
        }
    }

    private void SaveAttemptsRemaining()
    {
        PlayerPrefs.SetInt("AttemptsRemaining", attemptsRemaining);
        PlayerPrefs.Save();
    }

    public void StartQuiz()
    {
        if (attemptsRemaining > 0)
        {
            startPanel.SetActive(false);
            Quizpanel.SetActive(true);
            hintPanel.SetActive(true);

            timeRunning = true;
            
            attemptsRemaining--;
            PlayerPrefs.SetInt("AttemptsRemaining", attemptsRemaining);
            Debug.Log("Attempts decremented. Remaining:" + attemptsRemaining);
            
            generateQuestion();
        }

        else if (attemptsRemaining <= 0) 
        {
            Quizpanel.SetActive(false);
            hintPanel.SetActive(false);
            restartGamePanel.SetActive(true);
            Debug.LogWarning("No attempts left!");

        }

    }

    public void retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }


    public bool Cleared()
    {

        timeRunning = false;

        Quizpanel.SetActive(false);
        GoPanel.SetActive(false);
        ClearedPanel.SetActive(true);
        hintPanel.SetActive(false);
        AudioManager.PlaySFX(AudioManager.RoomCleared);
        AudioManager.Pause(AudioManager.BGRoom1);

        int currentBuildIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentBuildIndex == 4)
        { 
            pointsManager.AddRepPoints2(); 
        }
        else if(currentBuildIndex == 5)
        {
            pointsManager.AddRepPoints3();
        }
        else if( currentBuildIndex == 6) 
        {
            pointsManager.AddRepPoints4();
        }
        else if( currentBuildIndex == 7) 
        {
            pointsManager.AddRepPoints5();
        }
        else
        { 
            pointsManager.AddRepPoints(); 
        }
        
        attemptsRemaining = maxAttempts;
        PlayerPrefs.SetInt("AttemptsRemaining", attemptsRemaining);
        

        return true;

    }


    void GameOver()
    {
        timerText.color = Color.red;
        timeRunning = false;
        AudioManager.PlaySFX(AudioManager.GameOver);
        Quizpanel.SetActive(false);
        GoPanel.SetActive(true);
        hintPanel.SetActive(false);
        ScoreTxt.text = score + "/" + totalQuestions;
        attemptsLeftText.text = "Attempts Left: " + attemptsRemaining.ToString();

    }


    public void No()
    {
        confirmationPanel.SetActive(false);
    }

    public void ExitRoom()
    {
        confirmationPanel.SetActive(true);
    }

    public void DisableButtons()
    {
        buttonOne.interactable = false;
        buttonTwo.interactable = false;
        buttonThree.interactable = false;
        buttonFour.interactable = false;
    }

    public void correct()
    {
        score += 1;


        QnA.RemoveAt(currentQuestion);
        StartCoroutine(WaitForNext());
        DisableButtons();
        AudioManager.PlaySFX(AudioManager.CorrectAns);

    }

    public void wrong()
    {
        QnA.RemoveAt(currentQuestion);
        StartCoroutine(WaitForNext());
        DisableButtons();
        AudioManager.PlaySFX(AudioManager.WrongAns);
    }

    public void hintRefresh()
    {
        hintButton.interactable = true;
    }

    public void HintFunction()
    {

        if (hintCount > 0)
        {
            hintCount--;
            hintsLeftText.text = "Hints left: " + hintCount.ToString();
            HighlightCorrectAnswer();
            AudioManager.PlaySFX(AudioManager.HintButton);
            hintButton.interactable = false;
        }
        else if (hintCount == 0)
        {
            hintsLeftText.text = "No hints left ";
            hintButton.interactable = false;
        }
    }

    void HighlightCorrectAnswer()
    {
        for (int i = 0; i < options.Length; i++)
        {
            if (options[i].GetComponent<AnswerScript>().isCorrect)
            {
                options[i].GetComponent<Image>().color = new Color32(255, 255, 225, 100);
                break;
            }
        }
    }

    public void buttonRefresher()
    {
        buttonOne.interactable = true;
        buttonTwo.interactable = true;
        buttonThree.interactable = true;
        buttonFour.interactable = true;
        hintButton.interactable = true;
    }

    IEnumerator WaitForNext()
    {
        yield return new WaitForSeconds(1);
        buttonRefresher();
        generateQuestion();
        currentTime = startingTime;
        if (hintCount <= 0)
        {
            hintPanel.SetActive(false);
        }

    }

    void SetAnswers()
    {
        for (int i = 0; i < options.Length; i++)
        {
            options[i].GetComponent<Image>().color = options[i].GetComponent<AnswerScript>().startColor;
            options[i].GetComponent<AnswerScript>().isCorrect = false;
            options[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = QnA[currentQuestion].Answers[i];


            if (QnA[currentQuestion].CorrectAnswer == i + 1)
            {
                options[i].GetComponent<AnswerScript>().isCorrect = true;

            }
        }
    }

    void generateQuestion()
    {
        if (QnA.Count > 0)
        {
            currentQuestion = Random.Range(0, QnA.Count);

            QuestionTxt.text = QnA[currentQuestion].Question;
            SetAnswers();
        }
        else
        {
            Debug.Log("Out of Question");
            if (score == totalQuestions)
            {
                Cleared();
            }
            else if (score < totalQuestions)
            {
                GameOver();
            }

        }
    }


    void Update()
    {
        if (timeRunning == true)
        {
            currentTime -= 1 * Time.deltaTime;
            timerText.text = currentTime.ToString("0:00");

            if (currentTime <= 5f && !soundPlayed)
            {
                // Play sound effect
                AudioManager.PlaySFX(AudioManager.TimerSound);

                // Set soundPlayed to true to prevent repeated playing
                soundPlayed = true;
            }

            if (currentTime <= 0)
            {
                currentTime = 0;
                //timeAudio.Play();
                GameOver();

            }

        }

    }


}