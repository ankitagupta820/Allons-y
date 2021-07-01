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
    private float countCollectibles;

    public void incrementCountCollectibles() {
        countCollectibles++;
    }

    public void getCountCollectibles(int val)
    {
        countCollectibles = val;
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
        CalcDis();
    }

    private void OnDestroy()
    {
        Debug.Log(Analytics.CustomEvent("PlayerStatsDan", new Dictionary<string, object>
        {
            {
                "Score", theScore
            }
        }));
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
        //Debug.Log(countCollectibles);
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