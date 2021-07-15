using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateLevel : MonoBehaviour
{
    public GameObject player;
    private float deActivateDis = 20f;

    public void setDeActivateDis(float dis) {
        deActivateDis = dis;
    }

    void Update()
    {
        float yDis = transform.position.y - player.transform.position.y;
        if (yDis > deActivateDis)
        {
            gameObject.SetActive(false);
        }
    }
}
