using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Analytics;

public class Instruction_pause : MonoBehaviour
{
    public static bool GamePaused = false;
    public GameObject pauseMenuUI;
    // Update is called once per frame
    private AudioSource[] audioSources;

    private void Start()
    {
        audioSources = GameObject.FindObjectsOfType<AudioSource>();
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GamePaused = false;
        for (int i = 0; i < audioSources.Length; i++)
        {
            audioSources[i].Play();
        }
    }

    public void Pause()
    {
        for (int i = 0; i < audioSources.Length; i++)
        {
            audioSources[i].Pause();
        }
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GamePaused = true;
        
    }
}