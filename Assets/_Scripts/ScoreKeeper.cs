using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour
{
    public Text scoreValue;
    public Transform startPos;
    public GameObject player;
    private Transform playerPos;

    // Start is called before the first frame update
    void Start()
    {
        playerPos = player.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        scoreValue.text = (startPos.position.y - playerPos.position.y).ToString("F0");
    }
}
