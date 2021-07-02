using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Analytics;
public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }
    public GameObject deathMenuUI;
    public GameObject winMenuUI;
    public static bool GamePaused = false;
    public static int _deathCount = 0;
    public static int _winCount = 0;

    // Singleton Pattern
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    void Start()
    {
        deathMenuUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayerDeath()
    {
        deathMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GamePaused = true;
        _deathCount += 1;
        ScoreManager.Instance.SendAnalytics();
        /*LevelGenerator.Instance.SendEAnalytics();*/
    }

    public void PlayerWin() {
        winMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GamePaused = true;
        _winCount += 1;
        ScoreManager.Instance.SendAnalytics();
        /*LevelGenerator.Instance.SendEAnalytics();*/
    }

    public void Resume()
    {
        deathMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GamePaused = false;
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
    }

    public void QuitGame()
    {
        Debug.Log("Quitting....");
        Application.Quit();
    }

    void OnDestroy()
    {
        Debug.Log(Analytics.CustomEvent("Death_Win_Stats", new Dictionary<string, object>
            {
                {"Death_By_Monster", _deathCount},
                {"Number_of_Wins", _winCount}
            }));
    }
}
