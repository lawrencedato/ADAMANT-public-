using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public GameObject dialoguePanel;
    public Image characterImage;
    public TMP_Text dialogueText;
    public TMP_Text characterNameText;
    public Button continueButton;

    public Sprite npcImage;
    public Sprite playerImage;

    public string[] dialogue;
    private int index = 0;

    public float textSpeed = 0.05f;
    public float interactionDistance = 2.0f;

    public MovementController movementController;

    private bool isFirstText = true;
    private Coroutine typingCoroutine;

    private bool isTyping = false;

    void Start()
    {
        dialoguePanel.SetActive(false);
        continueButton.onClick.AddListener(OnContinueButtonClick);
        movementController = FindObjectOfType<MovementController>();

        if (movementController == null)
        {
            Debug.LogError("MovementController not found in the scene.");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !dialoguePanel.activeInHierarchy)
        {
            TryStartDialogue();
        }
    }

    void TryStartDialogue()
    {
        if (IsPlayerCloseToNPC())
        {
            StartDialogue();
        }
        // You can add an else statement here if you want to provide feedback when the player is not close to the NPC.
    }

    bool IsPlayerCloseToNPC()
    {
        if (movementController != null && movementController.transform != null)
        {
            float distance = Vector3.Distance(transform.position, movementController.transform.position);
            return distance <= interactionDistance;
        }

        return false;
    }

    void StartDialogue()
    {
        movementController.enabled = false;

        dialoguePanel.SetActive(true);
        StartTyping();
    }

    void StartTyping()
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }
        typingCoroutine = StartCoroutine(Typing());
    }

    IEnumerator Typing()
    {
        isTyping = true;

        characterImage.sprite = (index % 2 == 0) ? npcImage : playerImage;
        characterNameText.text = (index % 2 == 0) ? "NPC" : "Player";

        dialogueText.text = ""; // Clear previous text

        foreach (char letter in dialogue[index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(isFirstText ? textSpeed : 0f);
        }

        isFirstText = false; // Update flag after first text
        isTyping = false;
    }

    void OnContinueButtonClick()
    {
        if (dialoguePanel.activeInHierarchy)
        {
            if (isTyping)
            {
                // If still typing, complete the text instantly
                StopCoroutine(typingCoroutine);
                dialogueText.text = dialogue[index];
                isTyping = false;
            }
            else
            {
                index++;

                if (index < dialogue.Length)
                {
                    StartTyping(); // Start typing the next dialogue
                }
                else
                {
                    EndDialogue();
                }
            }
        }
    }

    void EndDialogue()
    {
        movementController.enabled = true;

        dialoguePanel.SetActive(false);
        index = 0;
        isFirstText = true; // Reset flag for next dialogue
    }
}