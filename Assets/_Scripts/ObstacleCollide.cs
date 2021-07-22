using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleCollide : MonoBehaviour
{
   
    public string collectableLabel; // Label for letter type to be popped out by the asteroid.
    private ScoreManager scoreManager; 

    void Start()
    {
        scoreManager = ScoreManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject playerGO = other.gameObject;
        if (playerGO.tag == "Player")
        {
            int collectedObjects = ScoreManager.getCollectibleCollectedNumber(collectableLabel);
            scoreManager.popOutCollectibles(collectableLabel, collectedObjects);
        }
    }
}
