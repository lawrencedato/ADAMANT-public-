using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnswerScript2 : MonoBehaviour
{
    public bool isCorrect = false;
    public QuizManager2 quizManager2;


    public Color startColor;

    private void Start()
    {
        Image imageComponent = GetComponent<Image>();
        if (imageComponent != null)
        {
            startColor = imageComponent.color;
        }
        else
        {
            Debug.Log("No Image component found on the object.");
        }
    }


    public void Answer()
    {
        if (isCorrect)
        {
            GetComponent<Image>().color = Color.green;
            //Debug.Log("Correct Answer");
            quizManager2.correct();

            
        }
        else
        {
            GetComponent<Image>().color = Color.red;
            //Debug.Log("Wrong Answer");
            quizManager2.wrong();
        }
    }

}