using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 3f;

    AudioManager audioManager;

    [Header("Menu Screens")]
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private GameObject mainMenu;

    [Header("Slider")]
    [SerializeField] private Slider loadingSlider;

    private void Awake()
    {
      
    }

    public void PlayGame(int index)
    {
        StartCoroutine(Loadlevel(index));
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    IEnumerator Loadlevel(int index)
    {
        loadingSlider.value = 0;
        mainMenu.SetActive(false);
        loadingScreen.SetActive(true);

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(index);
        asyncOperation.allowSceneActivation = false;
        float progress = 0;
        
        while (!asyncOperation.isDone)
        {

             progress = Mathf.MoveTowards(progress, asyncOperation.progress, Time.deltaTime);
            loadingSlider.value = progress;
            if (progress >= 0.9f)
            {
                loadingSlider.value = 1;
                asyncOperation.allowSceneActivation = true;
            }
            transition.SetTrigger("Start");
            yield return null;

        }
    }

}
