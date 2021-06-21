using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    private static ScoreManager _instance;

    public static ScoreManager Instance { get { return _instance; } }

    public GameObject player;
    private Vector3 initialPos;
    private Rigidbody playerRB;
    public GameObject Score;
    public GameObject Coinalert;
    public GameObject Dis;
    public GameObject Speed;
    private float theScore = 0;

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
    }

    void Update()
    {
        CalcSpeed();
        CalcDis();
    }

    private void CalcSpeed()
    {
        Speed.GetComponent<Text>().text = playerRB.velocity.magnitude.ToString("F0");
    }

    private void CalcDis()
    {
        Dis.GetComponent<Text>().text = Vector3.Distance(initialPos,player.transform.position).ToString("F0");
    }

    public void AddScore(int score)
    {
        theScore += score;
        Score.GetComponent<Text>().text = theScore.ToString("F0");
        StartCoroutine(ShowAlert());
    }

    private IEnumerator ShowAlert()
    {
        Coinalert.GetComponent<Text>().text = "COIN COLLECTED!";
        yield return new WaitForSeconds(.5f);
        Coinalert.GetComponent<Text>().text = "";
    }
}
