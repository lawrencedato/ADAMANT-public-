using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class addreppoints : MonoBehaviour
{
    int Points = 100;
    public Button Add;
    public void AddPoints()
    {
        PlayerPrefs.SetInt("REPpoints", Points);
        PlayerPrefs.Save();
    }

}
