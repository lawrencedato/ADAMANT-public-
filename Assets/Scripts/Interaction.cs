using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.CompilerServices;

public class Interaction : MonoBehaviour
{
    [SerializeField]
    public bool interactAllowed;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("room1"))
        {
            interactAllowed = true;
            collision.gameObject.GetComponent<InteractionSystem>().NotifyPlayer();
        }

        if (collision.gameObject.name.Equals("room2"))
        {
            interactAllowed = true;
            collision.gameObject.GetComponent<InteractionSystem2>().NotifyPlayer();
        }

        if (collision.gameObject.name.Equals("room3"))
        {
            interactAllowed = true;
            collision.gameObject.GetComponent<InteractionSystem3>().NotifyPlayer();
        }
        if (collision.gameObject.name.Equals("room4"))
        {
            interactAllowed = true;
            collision.gameObject.GetComponent<InteractionSystem4>().NotifyPlayer();
        }
        if (collision.gameObject.name.Equals("room5"))
        {
            interactAllowed = true;
            collision.gameObject.GetComponent<InteractionSystem5>().NotifyPlayer();
        }
            if (collision.gameObject.name.Equals("NPC"))
        {
            interactAllowed = true;
            collision.gameObject.GetComponent<InteractionNPC>().NotifyPlayer();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("room1"))
        {
            interactAllowed = true;
            collision.gameObject.GetComponent<InteractionSystem>().DenotifyPlayer();
        }

        if (collision.gameObject.name.Equals("room2"))
        {
            interactAllowed = true;
            collision.gameObject.GetComponent<InteractionSystem2>().DenotifyPlayer();
        }

        if (collision.gameObject.name.Equals("room3"))
        {
            interactAllowed = true;
            collision.gameObject.GetComponent<InteractionSystem3>().DenotifyPlayer();
        }

        if (collision.gameObject.name.Equals("room4"))
        {
            interactAllowed = true;
            collision.gameObject.GetComponent<InteractionSystem4>().DenotifyPlayer();
        }
        if (collision.gameObject.name.Equals("room5"))
        {
            interactAllowed = true;
            collision.gameObject.GetComponent<InteractionSystem5>().DenotifyPlayer();
        }
        if (collision.gameObject.name.Equals("NPC"))
        {
            interactAllowed = true;
            collision.gameObject.GetComponent<InteractionNPC>().DenotifyPlayer();
        }
    }
}
