using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coin : MonoBehaviour
{
    public float turnspeed = 90f;
    public int value = 10;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            ScoreManager.Instance.AddScore(value);
        }
        Destroy(gameObject);
    }

    private void Update()
    {
        transform.Rotate(0, 0, turnspeed * Time.deltaTime);
    }
}
