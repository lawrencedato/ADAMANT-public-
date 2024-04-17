using UnityEngine;
using TMPro;

public class PopUpText : MonoBehaviour
{
    public TextMeshProUGUI text1;
    public TextMeshProUGUI text2;
    public float fadeDuration = 1f; // Adjust this to change the overall duration of fading
    public float secondPopUpDuration = 1f; // Adjust this to change how long the second pop-up text stays visible
    public Vector3 initialOffsetText1, finalOffsetText1;
    public Vector3 initialOffsetText2, finalOffsetText2;
    public GameObject panel1; // Add a reference to your first panel here
    public GameObject panel2; // Add a reference to your second panel here

    private float fadeStartTimeText1;
    private float fadeStartTimeText2;
    private bool text1Finished = false;

    private void Start()
    {
        fadeStartTimeText1 = Time.time;
        fadeStartTimeText2 = float.MaxValue; // Initialize to a very large value to prevent showing text2 prematurely
        text1.gameObject.SetActive(true);
        panel1.SetActive(true);
    }

    private void Update()
    {
        if (!text1Finished)
        {
            float progressText1 = (Time.time - fadeStartTimeText1) / fadeDuration;
            if (progressText1 <= 1)
            {
                transform.localPosition = Vector3.Lerp(initialOffsetText1, finalOffsetText1, progressText1);
                text1.color = new Color(text1.color.r, text1.color.g, text1.color.b, 1 - progressText1);
                if (progressText1 >= 0.5f && panel1.activeSelf) // Check if panel1 is still active
                {
                    HideText1AndPanel(); // Hide the first text and its panel
                    text1Finished = true; // Mark text1 as finished
                    // Call the method to show the second text and panel
                    ShowText2AndPanel();
                }
            }
        }
        else
        {
            // Handle text2 display
            float progressText2 = (Time.time - fadeStartTimeText2) / (fadeDuration * 0.5f); // Adjusted fadeDuration here to make it faster
            if (progressText2 <= 1)
            {
                transform.localPosition = Vector3.Lerp(initialOffsetText2, finalOffsetText2, progressText2);
                text2.color = new Color(text2.color.r, text2.color.g, text2.color.b, progressText2);
            }
            else if (Time.time >= fadeStartTimeText2 + secondPopUpDuration)
            {
                // If secondPopUpDuration has passed, start fading out the second text
                HideText2AndPanel();
            }
        }
    }

    private void HideText1AndPanel()
    {
        text1.gameObject.SetActive(false); // Hide the first text
        panel1.SetActive(false); // Hide the first panel
    }

    private void ShowText2AndPanel()
    {
        fadeStartTimeText2 = Time.time; // Start fading in text2
        text2.gameObject.SetActive(true);
        panel2.SetActive(true); // Show the second panel
    }

    private void HideText2AndPanel()
    {
        text2.gameObject.SetActive(false); // Hide the second text
        panel2.SetActive(false); // Hide the second panel
        Destroy(gameObject); // Destroy the gameObject when both text and panel are hidden
    }
}



