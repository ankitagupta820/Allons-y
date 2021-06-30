using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coin : MonoBehaviour
{
    public float turnspeed = 90f;
    public int value = 10;

    ParticleSystem fracture;

    private void Start()
    {
        fracture = GetComponent<ParticleSystem>();
        // Debug.Log(fracture);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            ScoreManager.Instance.AddScore(value);
            fracture.Play();
        }
        
        gameObject.GetComponentInChildren<MeshRenderer>().enabled = false;
        Invoke("Destroy", 3f);
    }

    private void Update()
    {
        transform.Rotate(0, 0, turnspeed * Time.deltaTime);
    }

    private void DestroyItem()
    {
        Destroy(gameObject);
    }
}
