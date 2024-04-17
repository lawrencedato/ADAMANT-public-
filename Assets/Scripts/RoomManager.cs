using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class RoomManager1 : MonoBehaviour
{
    public int repPoints;

    private void Start()
    {


        if (PlayerPrefs.HasKey("REPpoints"))
        {
            repPoints = PlayerPrefs.GetInt("REPpoints");
        }
        //Debug.Log("rep points mo ngayon " + repPoints);

    }
    

}
