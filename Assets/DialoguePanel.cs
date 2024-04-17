using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialoguePanel : MonoBehaviour
{
    public GameObject dialoguePanel;
    public GameObject popUpPanel; // Reference to the pop-up panel
    public TMP_Text popUpText; // Reference to the TextMeshPro text component in the pop-up panel
    public Image characterImage;
    public TMP_Text dialogueText;
    public TMP_Text characterNameText;
    public Button continueButton;

    public Sprite npcImage;

    private int playerReputation;

    public float textSpeed = 0.05f;
    public float interactionDistance = 2.0f;

    public MovementController movementController;

    private bool isFirstText = true;
    private Coroutine typingCoroutine;

    private bool isTyping = false;
    public AudioManager AudioManager;

    string finalText;

    void Start()
    {
        dialoguePanel.SetActive(false);
        popUpPanel.SetActive(false); // Deactivate the pop-up panel initially
        continueButton.onClick.AddListener(OnContinueButtonClick);
        movementController = FindObjectOfType<MovementController>();

        if (movementController == null)
        {
            Debug.LogError("MovementController not found in the scene.");
        }

        // Retrieve player reputation from PlayerPrefs
        playerReputation = PlayerPrefs.GetInt("REPpoints");

        // Start the dialogue if player is close to NPC
        TryStartDialogue();
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

        characterImage.sprite = npcImage;
        characterNameText.text = "NPC";

        dialogueText.text = ""; // Clear previous text

        // Check if all rooms are completed and player has 100 reputation
        if (playerReputation == 100)
        {
            finalText = "Congratulations! You are now qualified for this scholarship!";
        }
        else
        {
            finalText = "You need to try harder, talk to me after you complete all the rooms.";
        }

        foreach (char letter in finalText.ToCharArray())
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
                StopAllCoroutines();
                dialogueText.text = finalText;
                isTyping = false;

                // Show the pop-up panel if player has 100 reputation
                if (playerReputation == 100)
                {
                    StopAllCoroutines();
                    dialogueText.text = finalText;
                    isTyping = false;
                    ShowPopUpPanel();
                }
            }
            else
            {
                EndDialogue();
            }
        }
    }

    void EndDialogue()
    {
        movementController.enabled = true;

        dialoguePanel.SetActive(false);
        isFirstText = true; // Reset flag for next dialogue
    }

    void ShowPopUpPanel()
    {
        popUpPanel.SetActive(true);
        AudioManager.PlaySFX(AudioManager.GameCompleted);
        // You can customize the text content here if needed
    }
}




