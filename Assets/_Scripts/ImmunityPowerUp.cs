using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImmunityPowerUp : MonoBehaviour
{

    public float duration = 5.0f;

    void OnTriggerEnter(Collider other, float duration){
        if (other.CompareTag("Player")){
            //StartCoroutine(Pickup(other));
            GameManager.Instance.isImmune = true;


            //Start a count down for duration, and after the duration, GameManager.Instance.isUmmune = false;
            duration -= Time.deltaTime;
            if(duration == 0){
                GameManager.Instance.isImmune = false;
            }

            
        }
        
    }

    IEnumerator Pickup(Collider player){

        //Instantiate(pickupEffect, transform.position, transform.rotation);
        Debug.Log("powerup picked up");


        void OnTriggerEnter(Collider player){
            if(player.CompareTag("Enemy")){
                PoojaPlayerController PlayerVariables = player.GetComponent<PoojaPlayerController>();


                //transform.Translate(Vector3(0, -(PlayerVariables._moveSpeed).time.deltaTime, 0), Space.World);

                GetComponent<MeshRenderer>().enabled = false;
                GetComponent<Collider>().enabled = false;
                yield return new WaitForSeconds(duration);


                //// how to revert back to original state??

                Destroy(gameObject);

            }
            
        }
        
    }
    
}
