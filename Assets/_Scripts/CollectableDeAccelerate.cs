using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableDeAccelerate : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            PoojaPlayerController pScript = other.gameObject.GetComponent<PoojaPlayerController>();
            pScript.setSpeedForDeAccelerate();

        }
    }
}
