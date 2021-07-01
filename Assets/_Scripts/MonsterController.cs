using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    public Vector3 initialPos;
    public float travelDistance = 100f;
    public float lerpDuration = 10f;
    
    void Start()
    {
        initialPos = transform.position;
        initialPos.y += travelDistance;
        StartCoroutine(LerpPosition(initialPos, lerpDuration));
        
    }

    IEnumerator LerpPosition(Vector3 targetPosition, float duration)
    {
        float time = 0;
        Vector3 startPosition = transform.position;

        while (time < duration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPosition;
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.transform.gameObject.name);
        if (other.transform.gameObject.tag == "Player")
        {
            GameManager.Instance.PlayerDeath();
        }
    }
}
