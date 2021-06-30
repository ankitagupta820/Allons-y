using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    public Transform positionToMoveTo;
    public float lerpDuration = 5f;
    
    void Start()
    {
        StartCoroutine(LerpPosition(positionToMoveTo.position, lerpDuration));
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
