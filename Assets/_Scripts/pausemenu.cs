using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Analytics;

public class pausemenu : MonoBehaviour
{
    public static bool GamePaused = false;
    public GameObject pauseMenuUI;
    // Update is called once per frame

    public static int pauseCount = 0;
    public static int restartCount = 0;
    void Update()
    {
        Debug.Log("Game is resumed!!");
        Debug.Log(Time.timeScale);
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(GamePaused)
            {
                Resume();
            }else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        Debug.Log("Game is resumed!!");
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GamePaused = false;

    }

    void Pause()
    {
        Debug.Log("Game is paused!!");
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GamePaused = true;
        pauseCount += 1;
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("StartMenu");
    }

    public void RestartMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        restartCount += 1;
    }

    public void QuitGame()
    {
        Debug.Log("Quitting....");
        Application.Quit();
    }
    private void OnDestroy()
    {
        Analytics.CustomEvent("MenuStats", new Dictionary<string, object>
            {
                {"PauseCount", pauseCount},
                {"RestartCount", restartCount}
            });
    }







}
