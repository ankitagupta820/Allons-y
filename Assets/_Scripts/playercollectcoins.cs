using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playercollectcoins : MonoBehaviour
{
    public  GameObject Score;
    public GameObject Coinalert;
    public static int theScore = 0;
    //public AudioSource collectSound;


    void OnTriggerEnter(Collider other)
    {
        //  collectSound.Play();
        if (gameObject.name == "Coin" || gameObject.name == "Coin(Clone)")
            theScore = theScore + 10;

        else if (gameObject.name == "Coin2" || gameObject.name == "Coin2(Clone)")
            theScore = theScore + 50;

        else
            theScore = theScore + 100;

        Score.GetComponent<Text>().text = "SCORE: " + theScore;
        Coinalert.GetComponent<Text>().text = "You Just Collected a Coin!";
       

        Destroy(gameObject);

    }


 
}
