using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InteractionSystem5 : MonoBehaviour
{
    public GameObject interactNotification;

    public void DenotifyPlayer()
    {
        interactNotification.SetActive(false);
    }
    public void NotifyPlayer()
    {
        interactNotification.SetActive(true);
    }

}