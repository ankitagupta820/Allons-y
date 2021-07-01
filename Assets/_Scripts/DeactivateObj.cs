using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateObj : MonoBehaviour
{
    public GameObject player;
    public float deActivateDis = 10f;

    void Update()
    {
        float yDis = transform.position.y - player.transform.position.y;
        if (yDis > deActivateDis)
        {
            gameObject.SetActive(false);
        } 
    }
}
