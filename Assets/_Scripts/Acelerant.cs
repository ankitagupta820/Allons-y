using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Acelerant : MonoBehaviour
{
    public float turnspeed = 90f;
    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
    private void Update()
    {
        transform.Rotate(0, 0, turnspeed * Time.deltaTime);
    }
}
