using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InteractionNPC : MonoBehaviour
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