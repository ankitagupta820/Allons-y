using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeTransparent : MonoBehaviour
{
    public float rayCastOffset = 5f;

    public GameObject player;

    void Start()
    {
    }

    void FixedUpdate()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, player.transform.position - transform.position,
            out hit, Vector3.Distance(transform.position, player.transform.position) + rayCastOffset))
        {

            GameObject hitTarget = hit.collider.gameObject;

            //Debug.Log(hitTarget.transform.root);
            if (hitTarget.transform.root != null)
            {
                // If the hit Target is a composite object
                Renderer[] childRenderers = hitTarget.transform.root.GetComponentsInChildren<Renderer>();
                foreach (Renderer rend in childRenderers)
                {
                    // Assuming material render mode is Fadeout!
                    StartCoroutine(AlphaFadeOut(rend, 0.2f));
                }
            }
        }
    }

    IEnumerator AlphaFadeOut(Renderer rend, float endAlpha, float duration = 1f)
    {
        for (float t = 0f; t < duration; t += Time.deltaTime)
        {
            Color color = rend.material.color;
            if (color.a > endAlpha)
            {
                color.a = color.a - 0.01f;
                rend.material.color = color;
                yield return null;
            }
        }
    }
}