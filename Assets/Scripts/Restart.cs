using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Restart : MonoBehaviour
{
    public Button restartButton;
    public Button startButton;
    private Vector3 defaultStartPosition;
    private Vector3 restartButtonPosition;

    void Start()
    {
        defaultStartPosition = startButton.transform.position;
        restartButtonPosition = restartButton.transform.position;

        // Check if the value exists in PlayerPrefs
        if (PlayerPrefs.HasKey("REPpoints") || PlayerPrefs.HasKey("AttemptsRemaining"))
        {
            // Value exists, enable the restart button
            SetRestartButtonVisible(true);
        }
        else
        {
            // Value doesn't exist, disable the restart button
            SetRestartButtonVisible(false);
        }
    }

    public void restartGame()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void SetRestartButtonVisible(bool isVisible)
    {
        restartButton.gameObject.SetActive(isVisible);

        if (!isVisible)
        {
            // Move start button to restart button position
            startButton.transform.position = restartButtonPosition;
        }
        else
        {
            // Move start button to default position
            startButton.transform.position = defaultStartPosition;
        }
    }
}