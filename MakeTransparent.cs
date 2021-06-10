using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeTransparent : MonoBehaviour
{

    [SerializeField] private Material transparentMaterial;

    public float closedDistance= 1f;

    Vector3 playerLocation;

    void Start() {
        playerLocation = transform.position;
    }

    void Update()
    {
        Vector3 movingDirection = transform.position - playerLocation;
        playerLocation = transform.position;

        Ray ray = new Ray(transform.position, movingDirection);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo)) {

            Transform hit = hitInfo.transform;
            // Debug.Log(hit.gameObject);
            // Debug.Log(hitInfo.distance);

            if (hitInfo.distance > closedDistance && hit.gameObject.tag == "Cloud") {

                Debug.DrawLine(ray.origin, hitInfo.point, Color.red);

                Transform parent = hit.parent;
                // Debug.Log(hit.parent);

                for (int i = 0; i < parent.childCount; i++) {
                    parent.GetChild(i).GetComponent<Renderer>().material = transparentMaterial;
                }

                // hit.GetComponent<Renderer>().material = transparentMaterial;
            }
            
        } else {
            Debug.DrawLine(ray.origin, ray.origin + ray.direction * 100, Color.green);
        }
    }

}
