using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TunnelMovement : MonoBehaviour
{
    private void Update ()
    {
        transform.Translate(Vector3.up * Time.deltaTime * 2, Space.World);
    }
}
