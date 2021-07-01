using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Analytics;

public class ScoreManager : MonoBehaviour
{
    private static ScoreManager _instance;

    public static ScoreManager Instance { get { return _instance; } }

    public GameObject player;
    private Vector3 initialPos;
    private Rigidbody playerRB;
    public GameObject Score;
/*    public GameObject Red_Collectible;
    public GameObject Yellow_collectible;*/
    public GameObject Coinalert;
    public GameObject Dis;
    public GameObject Speed;
    private float theScore = 0;
    private int countCollectibles;
    public float _scoreF = 0f;
    public int _dis = 0;
    public int _speed = 0;
    public float gameTimer = 0f;
    public string timerString;
    public void incrementCountCollectibles() {
        countCollectibles++;
    }

    public int getCountCollectibles()
    {
        return countCollectibles;
    }

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
        playerRB = player.GetComponent<Rigidbody>();
        initialPos = player.transform.position;
        countCollectibles = 0;
    }

    void Update()
    {
        CalcSpeed();
        CalcDis(); gameTimer += Time.deltaTime;

        int secs = (int)(gameTimer % 60);
        int mins = (int)(gameTimer / 60) % 60;
        int hrs = (int)(gameTimer / 3600) % 24;

        //create a variable "timerString" to return the time in string

        timerString = string.Format("{0:0}:{1:00}:{2:00}", hrs, mins, secs);
        _scoreF = float.Parse(Score.GetComponent<Text>().text);
        _dis = int.Parse(Dis.GetComponent<Text>().text);
        _speed = int.Parse(Speed.GetComponent<Text>().text);
    }

    void OnDestroy()
    {
        Analytics.CustomEvent("PlayerStats", new Dictionary<string, object>
          {
            { "Score", _scoreF},
            { "Distance", _dis},
            { "Speed", _speed},
            { "Time_Elapsed", timerString},
            {"Number_of_Collectables", countCollectibles}

          });
    }

    private void CalcSpeed()
    {
        Speed.GetComponent<Text>().text = playerRB.velocity.magnitude.ToString("F0");
    }

    private void CalcDis()
    {
        Dis.GetComponent<Text>().text = Vector3.Distance(initialPos, player.transform.position).ToString("F0");
    }

    public void AddScore(int score, string tagValue)
    {
        theScore += score;
        Score.GetComponent<Text>().text = theScore.ToString("F0");
        StartCoroutine(ShowAlert(tagValue));
        incrementCountCollectibles();

        //Debug.Log("***************Manish******************");
        //Debug.Log(getCountCollectibles());
        //Red_Collectible.GetComponent<Text>().text = theScore.ToString("F0");
        //Yellow_collectible.GetComponent<Text>().text = theScore.ToString("F0");
    }

    private IEnumerator ShowAlert(string tagValue)
    {
        Coinalert.GetComponent<Text>().text = tagValue + " COLLECTED!";
        yield return new WaitForSeconds(3f);
        Coinalert.GetComponent<Text>().text = "";
    }

    
}