using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeingDelivered : MonoBehaviour
{
    public GameObject targetPlanet;

    public float t;
    public float speed;

    Vector3 targetPosition;

    
    void Start()
    {
        targetPosition = targetPlanet.transform.position;
    }


    void FixedUpdate()
    {
        Vector3 currentPosition = gameObject.transform.position;
        Vector3 lerpTargetPosition = Vector3.Lerp(currentPosition, targetPosition, t);

        gameObject.transform.position = Vector3.MoveTowards(
                currentPosition,
                lerpTargetPosition,
                speed);

        Invoke("DestroyItem", 3f);
        
    }

    //public void SetTargetPlanet(GameObject targetPlanet)
    //{
    //    this.targetPlanet = targetPlanet;
    //}

    void DestroyItem()
    {
        Destroy(gameObject);
    }
}
