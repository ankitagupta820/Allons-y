using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableAccelerate : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //gameObject.SetActive(false);
            PoojaPlayerController pScript = other.gameObject.GetComponent<PoojaPlayerController>();
            pScript.setSpeedForAccelerate();

        }
    }
}
