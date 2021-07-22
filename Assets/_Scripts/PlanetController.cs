using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlanetController : MonoBehaviour
{
    private bool isDeliveryMade;
    public GameObject tutorialManager;
    private TutorialManager tm;

    private ScoreManager scoreManager;
    // Start is called before the first frame update

    public bool getDeliveryMade() {
        return isDeliveryMade;
    }

    private void Start()
    {
        scoreManager = ScoreManager.Instance;
        isDeliveryMade = false;
        tutorialManager = GameObject.FindGameObjectWithTag("TutorialManager");
        if (tutorialManager != null) {
            tm = tutorialManager.GetComponent<TutorialManager>();
        }
    }

    private void Update()
    {
        if (scoreManager.isWithinRangeForAlert(gameObject))
        {
            if (scoreManager.isWithinRangeForDelivery(gameObject))
            {
                //Debug.Log("Is Within Range");
                if (ScoreManager.getIsSuccess())
                {
                    ScoreManager.setCurrentPlanetTag(null);
                    ScoreManager.setCurrentCollectibleTag(null);
                    ScoreManager.startDisplayPlanetDeliverySuccessAlert(gameObject.tag);
                }
                else {
                    ScoreManager.setCurrentPlanetTag(gameObject.tag);
                    ScoreManager.setCurrentCollectibleTag(scoreManager.getPlanetTagCollectibleTagMap()[gameObject.tag]);
                    ScoreManager.startDisplayPlanetDeliveryAlert(gameObject.tag);
                }
                
            }
            else
            {
                ScoreManager.setCurrentPlanetTag(null);
                ScoreManager.setCurrentCollectibleTag(null);
                ScoreManager.startDisplayPlanetAlert(gameObject.tag);
                ScoreManager.setIsSuccess(false);
            }
        }
        else {
            //Debug.Log("Planet Outta Range Called");
            if (scoreManager.isOutOfRangeForAlert(gameObject)) {
                //Debug.Log("Planet Outta Range");
                ScoreManager.setCurrentPlanetTag(null);
                ScoreManager.setCurrentCollectibleTag(null);
                ScoreManager.stopDisplayPlanetAlert(gameObject.tag);
            }
        }
    }

}
