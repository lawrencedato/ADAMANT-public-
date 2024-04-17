
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header ("---------- Audio Source ----------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("---------- Audio Clip ----------")]
    public AudioClip background;
    public AudioClip BGRoom1;
    public AudioClip BGRoom2;
    public AudioClip BGRoom3;
    public AudioClip BGRoom4;
    public AudioClip BGRoom5;
    public AudioClip buttonClick;
    public AudioClip insufficientPoints;
    public AudioClip WrongAns;
    public AudioClip CorrectAns;
    public AudioClip HintButton;
    public AudioClip GameOver;
    public AudioClip RoomCleared;
    public AudioClip GameCompleted;
    public AudioClip TimerSound;
    public GameObject QuizPanel;
    public GameObject RestartGamePanel;
    public GameObject GOPanel;
    public AudioClip RestartGame;
    AudioManager audioManager;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }   


    private void Start()
    {
        musicSource.clip = background;
        musicSource.Play();
    }
    public void room1()
    {
        if (QuizPanel.activeInHierarchy)
        {
            musicSource.Stop();
            musicSource.clip = BGRoom1;
            musicSource.Play();
        }
        else if (GOPanel.activeInHierarchy)
        {
            musicSource.Stop();
            audioManager.PlaySFX(RestartGame);
        }
        else if (RestartGamePanel.activeInHierarchy)
        {
            musicSource.Stop();
            audioManager.PlaySFX(RestartGame);
        }
    }
    public void room2()
    {
        if (QuizPanel.activeInHierarchy)
        {
            musicSource.Stop();
            musicSource.clip = BGRoom2;
            musicSource.Play();
        }
        else if (GOPanel.activeInHierarchy)
        {
            musicSource.Stop();
            audioManager.PlaySFX(RestartGame);
        }
        else if (RestartGamePanel.activeInHierarchy)
        {
            musicSource.Stop();
            audioManager.PlaySFX(RestartGame);
        }
    }
    public void room3()
    {
        if (QuizPanel.activeInHierarchy)
        {
            musicSource.Stop();
            musicSource.clip = BGRoom3;
            musicSource.Play();
        }
        else if (GOPanel.activeInHierarchy)
        {
            musicSource.Stop();
            audioManager.PlaySFX(RestartGame);
        }
        else if (RestartGamePanel.activeInHierarchy)
        {
            musicSource.Stop();
            audioManager.PlaySFX(RestartGame);
        }
    }
    public void room4()
    {
        if (QuizPanel.activeInHierarchy)
        {
            musicSource.Stop();
            musicSource.clip = BGRoom4;
            musicSource.Play();
        }
        else if (GOPanel.activeInHierarchy)
        {
            musicSource.Stop();
            audioManager.PlaySFX(RestartGame);
        }
        else if (RestartGamePanel.activeInHierarchy)
        {
            musicSource.Stop();
            audioManager.PlaySFX(RestartGame);
        }
    }
    public void room5()
    {
        if (QuizPanel.activeInHierarchy)
        {
            musicSource.Stop();
            musicSource.clip = BGRoom5;
            musicSource.Play();
        }
        else if (GOPanel.activeInHierarchy)
        {
            musicSource.Stop();
            audioManager.PlaySFX(RestartGame);
        }
        else if (RestartGamePanel.activeInHierarchy)
        {
            musicSource.Stop();
            audioManager.PlaySFX(RestartGame);
        }
    }
    public void Pause(AudioClip clip)
    {
        musicSource.Stop();
    }

    public void PlaySFX(AudioClip clip) 
    {
        SFXSource.PlayOneShot(clip);
    }

    public void ButtonClick()
    {
        audioManager.PlaySFX(audioManager.buttonClick);
    }
}
