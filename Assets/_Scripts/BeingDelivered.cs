using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeingDelivered : MonoBehaviour
{
    public GameObject targetPlanet;

    public float t;
    public float speed;

    public TimeManager timeManager;

    Vector3 targetPosition;

    
    void Start()
    {
        targetPosition = targetPlanet.transform.position;

        var roadShape = GetComponent<ParticleSystem>().shape;
        Quaternion q = Quaternion.FromToRotation(Vector3.forward, targetPosition - gameObject.transform.position);
        roadShape.rotation = q.eulerAngles;
    }

    // void FixedUpdate()
    void Update()  // Update() or fixedUpdate()
    {
        
        Vector3 currentPosition = gameObject.transform.position;
        Vector3 lerpTargetPosition = Vector3.Lerp(currentPosition, targetPosition, t);

        gameObject.transform.position = Vector3.MoveTowards(
                currentPosition,
                lerpTargetPosition,
                speed);

        // timeManager.DoSlowMotion();

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
