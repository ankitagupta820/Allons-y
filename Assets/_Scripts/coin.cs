using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coin : MonoBehaviour
{
    public float turnspeed = 90f;
    public int value = 10;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        GameObject playerGO = other.gameObject;
        if (playerGO.tag == "Player")
        {
            
            PoojaPlayerController poojaPlayerScript = playerGO.GetComponent<PoojaPlayerController>();
            switch (gameObject.tag) {
                case "YellowEnabler":
                    poojaPlayerScript.setCurrentEnabler(PoojaPlayerController.Enablers.Yellow.ToString());
                    poojaPlayerScript.rend.sharedMaterial = poojaPlayerScript.material[2];
                    ScoreManager.Instance.AddScore(value, gameObject.tag);
                    Destroy(gameObject);
                    break;
                case "RedEnabler":
                    poojaPlayerScript.setCurrentEnabler(PoojaPlayerController.Enablers.Red.ToString());
                    poojaPlayerScript.rend.sharedMaterial = poojaPlayerScript.material[1];
                    ScoreManager.Instance.AddScore(value, gameObject.tag);
                    Destroy(gameObject);
                    break;
                case "SearchItemRed":
                    if (poojaPlayerScript.getCurrentEnabler() == PoojaPlayerController.Enablers.Yellow.ToString())
                    {
                        poojaPlayerScript.setCurrentEnabler("");
                        poojaPlayerScript.rend.sharedMaterial = poojaPlayerScript.material[0];
                        //ScoreManager.Instance.AddScore(value, "Search Item "+ gameObject.tag);
                        ScoreManager.Instance.AddScore(value, "Search Item " + PoojaPlayerController.Enablers.Yellow.ToString());
                        

                    //ScoreManager.Instance.AddScore(value, "Current Value " + poojaPlayerScript.currentEnabler + " To String value " + PoojaPlayerController.Enablers.Yellow.ToString());


                        Destroy(gameObject);
                    }
                    else {
                        ScoreManager.Instance.AddScore(0, "Cannot be");
                    }
                    break;
                default:
                    ScoreManager.Instance.AddScore(value, "Object Tag " + gameObject.tag);
                    break;

            }

            
        }
        
    }

    private void Update()
    {
        transform.Rotate(0, 0, turnspeed * Time.deltaTime);
    }
}
