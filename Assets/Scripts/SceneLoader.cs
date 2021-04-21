using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    const float DEFAULT_LOAD_SCENE_DELAY = 0f;

    [SerializeField] private int splashScreenSceneIndex = 0;
    private float splashScreenDelay = 4f;
    private int currentSceneIndex;

    [SerializeField] private string startSceneName = "StartScene";
    [SerializeField] private string optionsSceneName = "OptionsScene";
    [SerializeField] private string gameOverSceneName = "GameOverScene";


    void Start()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentSceneIndex == splashScreenSceneIndex)
            LoadNextScene(splashScreenDelay);
    }

    public void LoadMainMenu()
    {
        StartCoroutine(LoadScene(startSceneName));
    }

    public void LoadOptionsScene() 
    { 
        StartCoroutine(LoadScene(optionsSceneName)); 
    }

    public void LoadNextScene(float delay = DEFAULT_LOAD_SCENE_DELAY) 
    { 
        StartCoroutine(LoadScene(currentSceneIndex + 1, delay)); 
    }

    public void RestartScene() 
    {
        StartCoroutine(LoadScene(currentSceneIndex));
    }

    public void LoadGameOverScene(float delay = DEFAULT_LOAD_SCENE_DELAY)
    {
        StartCoroutine(LoadScene(gameOverSceneName, delay));
    }


    private IEnumerator LoadScene(int sceneIndex, float delay = DEFAULT_LOAD_SCENE_DELAY)
    {
        Time.timeScale = 1;
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneIndex);
    }

    private IEnumerator LoadScene(string sceneName, float delay = DEFAULT_LOAD_SCENE_DELAY)
    {
        Time.timeScale = 1;
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
