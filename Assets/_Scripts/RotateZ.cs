using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateZ : MonoBehaviour
{
    public float turnspeed = 90f;
    private void Update()
    {
        transform.Rotate(0, turnspeed * Time.deltaTime, 0);
    }
}
