using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static System.TimeZoneInfo;

public class DoorInteraction2 : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;

    [SerializeField] private bool triggerActive = false;
    private RoomManager1 roomManager1;
    public int pointsRequirement = 10;

    public PopupWindow popupWindow;

    private void Start()
    {
        roomManager1 = GetComponent<RoomManager1>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name.Equals("Player"))
        {
            triggerActive = true;
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.name.Equals("Player"))
        {
            triggerActive = false;
        }
    }

    private void Update()
    {
        if (triggerActive && Input.GetKeyDown(KeyCode.E))
        {
            if (roomManager1.repPoints > pointsRequirement)
            {
                Debug.LogWarning("Cleared na");
                popupWindow.AddToQueue("Room Cleared!");
            }
            else if (roomManager1.repPoints < pointsRequirement)
            {
                Debug.LogWarning("opsxsz bawal pa");
                popupWindow.AddToQueue("Insufficient REP!");
            }
            else
            {
                SomeCoolAction();
            }
        }
    }

    public void SomeCoolAction()
    {
        StartCoroutine(Loadlevel(SceneManager.GetActiveScene().buildIndex + 2));
    }

    IEnumerator Loadlevel(int levelindex)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelindex);
    }
}
