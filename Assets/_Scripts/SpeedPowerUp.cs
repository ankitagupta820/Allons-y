using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPowerUp : MonoBehaviour
{

    public float impulseMagnitude  = 10.0f;
    public float speedMultiplier = 1.5f;
    // public GameObject pickupEffect;

    public float duration = 5.0f;

    void OnTriggerEnter(Collider other){
        if (other.CompareTag("Player")){
            StartCoroutine(Pickup(other));
        }
        
    }

    IEnumerator Pickup(Collider player){

        //Instantiate(pickupEffect, transform.position, transform.rotation);
        Debug.Log("powerup picked up");
        


        PoojaPlayerController PlayerVariables = player.GetComponent<PoojaPlayerController>();
        //Debug.Log("current speed:", PlayerVariables._moveSpeed);
        // PlayerVariables._moveSpeed *= speedMultiplier;
        //Debug.Log("update speed:", PlayerVariables._moveSpeed);

        PlayerVariables.characterBody.AddForce(new Vector3(0,-(PlayerVariables._moveSpeed) * speedMultiplier * Time.deltaTime, 0), ForceMode.Impulse);

        

        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;
        yield return new WaitForSeconds(duration);


        //// how to revert back to original speed??

        Destroy(gameObject);
    }
    
}
