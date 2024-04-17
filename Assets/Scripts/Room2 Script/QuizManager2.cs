using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class QuizManager2 : MonoBehaviour
{
    private PointsManager pointsManager;

    public List<QuestionAndAnswers2> QnA;
    public GameObject[] options;
    public int currentQuestion;

    public GameObject Quizpanel, GoPanel, ClearedPanel, confirmationPanel, timerPanel, startPanel, hintPanel;

    //Buttons (para sa pagdisable)
    public Button buttonOne, buttonTwo, buttonThree, buttonFour, hintButton;

    public TextMeshProUGUI QuestionTxt, ScoreTxt, roomInfoText, hintsLeftText;

    int totalQuestions = 0;
    public int pointsToGive = 5;
    int score;

    // Timer
    float currentTime = 0f;
    public bool timeRunning;
    public float startingTime = 10f;
    public TextMeshProUGUI timerText;

    // Hint System
    public Color startColor;
    public int maxHintUsage = 3;
    private int hintCount;

    // Attempts
    public int maxAttempts = 3;
    private int attemptsRemaining;


    private void Start()
    {
        pointsManager = PointsManager.Instance;
        options = new GameObject[4];
        if (options == null)
        {
            Debug.LogError("Options array is not initialized properly!");
            
        }

        totalQuestions = QnA.Count;

        startPanel.SetActive(true);
        Quizpanel.SetActive(false);
        hintPanel.SetActive(false);

        currentTime = startingTime;
        InitializeAttemptsRemaining();

        hintCount = maxHintUsage;
        hintsLeftText.text = "Hints left: " + maxHintUsage.ToString();

        roomInfoText.text = "Hint: " + maxHintUsage.ToString() + "\nTime: " + startingTime.ToString() + "\nREP Points: " + pointsToGive + "\nAttempt left: " + attemptsRemaining;

    }


    private void InitializeAttemptsRemaining()
    {
        if (PlayerPrefs.HasKey("AttemptsRemaining2"))
        {
            Debug.Log("Has Key");
            attemptsRemaining = PlayerPrefs.GetInt("AttemptsRemaining2");
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
        PlayerPrefs.SetInt("AttemptsRemaining2", attemptsRemaining);
        PlayerPrefs.Save();
    }

    public void StartQuiz()
    {
        startPanel.SetActive(false);
        Quizpanel.SetActive(true);
        hintPanel.SetActive(true);

        timeRunning = true;

        if (attemptsRemaining > 0)
        {
            attemptsRemaining--;
            PlayerPrefs.SetInt("AttemptsRemaining2", attemptsRemaining);
            Debug.Log("Attempts decremented. Remaining:" + attemptsRemaining);
        }
        else
        {
            Debug.LogWarning("No attempts left!");
        }

        generateQuestion();
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

        //pointsManager.AddRepPoints();

        return true;

    }


    void GameOver()
    {
        timerText.color = Color.red;
        timeRunning = false;

        Quizpanel.SetActive(false);
        GoPanel.SetActive(true);
        hintPanel.SetActive(false);
        ScoreTxt.text = score + "/" + totalQuestions;

        /*PlayerPrefs.SetInt("RepPoints", repPoints);
        PlayerPrefs.Save();*/

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

    }

    public void wrong()
    {
        QnA.RemoveAt(currentQuestion);
        StartCoroutine(WaitForNext());
        DisableButtons();
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
            if (options[i].GetComponent<AnswerScript2>().isCorrect)
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
        if (options != null)
        {
            for (int i = 0; i < options.Length; i++)
            {
                //options[i].GetComponent<Image>().color = options[i].GetComponent<AnswerScript2>().startColor;
                options[i].GetComponent<AnswerScript2>().isCorrect = false;
                options[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = QnA[currentQuestion].Answers[i];


                if (QnA[currentQuestion].CorrectAnswer == i + 1)
                {
                    options[i].GetComponent<AnswerScript2>().isCorrect = true;

                }
            }
        }
        else
        {
            Debug.LogWarning("Options array is not initialized!");
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

            if (currentTime <= 0)
            {
                currentTime = 0;
                //timeAudio.Play();
                GameOver();

            }

        }

    }


}