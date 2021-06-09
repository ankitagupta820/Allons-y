using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coin : MonoBehaviour
{
    public float turnspeed = 90f;

    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name != "Player")
        {
            return;
        }

        


        if (gameObject.name == "Coin" || gameObject.name == "Coin(Clone)")
            Debug.Log("Coinnn");


        

        else
            Debug.Log("Coinnn3333");

        Destroy(gameObject);
    }


    private void Update()
    {
        transform.Rotate(0, 0, turnspeed * Time.deltaTime);
    }
}
