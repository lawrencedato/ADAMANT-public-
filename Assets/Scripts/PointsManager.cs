using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PointsManager : MonoBehaviour
{
    QuizManager quizManager;
    QuizManager2 quizManager2;

    public int repPoints { get; private set; }
    public int pointsToGive = 5;
    [SerializeField] int roomTwoPoints = 15;
    [SerializeField] int roomThreePoints = 20;
    [SerializeField] int roomFourPoints = 25;
    [SerializeField] int roomFivePoints = 30;

    public TextMeshProUGUI repPointsText;

    private static PointsManager instance;

    public static PointsManager Instance
    {
        get { return instance; }
    }

    private void Awake()
    {

        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }


        instance = this;

        DontDestroyOnLoad(this.gameObject);
    }


    private void Start()
    {

        DontDestroyOnLoad(gameObject);
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
        UpdateRepPoints();
        repPointsText.text = "" + repPoints.ToString();
    }

    private void UpdateRepPoints()
    {
        if (PlayerPrefs.HasKey("REPpoints"))
        {
            repPoints = PlayerPrefs.GetInt("REPpoints");
        }
        else
        {
            SaveREPpoints();
        }
    }

    public void SaveREPpoints()
    {
        PlayerPrefs.SetInt("REPpoints", repPoints);
        PlayerPrefs.Save();
    }

    public void AddRepPoints()
    {
        repPoints += pointsToGive;
        SaveREPpoints();
        Debug.Log("REP points incremented: " + repPoints);
        repPointsText.text = "" + repPoints.ToString();
    }

    public void AddRepPoints2()
    {
        repPoints += roomTwoPoints;
        SaveREPpoints();
        Debug.Log("REP points incremented: " + repPoints);
        repPointsText.text = "" + repPoints.ToString();
    }

    public void AddRepPoints3()
    {
        repPoints += roomThreePoints;
        SaveREPpoints();
        Debug.Log("REP points incremented: " + repPoints);
        repPointsText.text = "" + repPoints.ToString();
    }

    public void AddRepPoints4()
    {
        repPoints += roomFourPoints;
        SaveREPpoints();
        Debug.Log("REP points incremented: " + repPoints);
        repPointsText.text = "" + repPoints.ToString();
    }

    public void AddRepPoints5()
    {
        repPoints += roomFivePoints;
        SaveREPpoints();
        Debug.Log("REP points incremented: " + repPoints);
        repPointsText.text = "" + repPoints.ToString();
    }

}
