using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

            shatter();

            switch (gameObject.tag) {

                case "YellowEnabler":
                    poojaPlayerScript.setCurrentEnabler(PoojaPlayerController.Enablers.Yellow.ToString());
                    poojaPlayerScript.rend.sharedMaterial = poojaPlayerScript.material[2];
                    ScoreManager.Instance.AddScore(value, gameObject.tag);

                    //Search for Image in Queue
                    bool alreadyPresent = false;
                    alreadyPresent = findImage("YellowEnablerImageUI", poojaPlayerScript.collectedEnablers);
                    
                    //Add Image in Queue if not already present
                    if (!alreadyPresent)
                    {
                        addImage("YellowEnablerImageUI", poojaPlayerScript.collectedEnablers);
                    }

                    // Destroy(gameObject);
                    break;
                case "RedEnabler":
                    poojaPlayerScript.setCurrentEnabler(PoojaPlayerController.Enablers.Red.ToString());
                    poojaPlayerScript.rend.sharedMaterial = poojaPlayerScript.material[1];
                    ScoreManager.Instance.AddScore(value, gameObject.tag);
                    //Search for Image in Queue
                    alreadyPresent = false;
                    alreadyPresent = findImage("RedEnablerImageUI", poojaPlayerScript.collectedEnablers);

                    //Add Image in Queue if not already present
                    if (!alreadyPresent)
                    {
                        addImage("RedEnablerImageUI", poojaPlayerScript.collectedEnablers);
                    }
                    // Destroy(gameObject);
                    break;
                case "BlueEnabler":
                    poojaPlayerScript.setCurrentEnabler(PoojaPlayerController.Enablers.Blue.ToString());
                    poojaPlayerScript.rend.sharedMaterial = poojaPlayerScript.material[1];
                    ScoreManager.Instance.AddScore(value, gameObject.tag);
                    //Search for Image in Queue
                    alreadyPresent = false;
                    alreadyPresent = findImage("BlueEnablerImageUI", poojaPlayerScript.collectedEnablers);

                    //Add Image in Queue if not already present
                    if (!alreadyPresent)
                    {
                        addImage("BlueEnablerImageUI", poojaPlayerScript.collectedEnablers);
                    }
                    Destroy(gameObject);
                    break;
                case "GreenEnabler":
                    poojaPlayerScript.setCurrentEnabler(PoojaPlayerController.Enablers.Green.ToString());
                    poojaPlayerScript.rend.sharedMaterial = poojaPlayerScript.material[1];
                    ScoreManager.Instance.AddScore(value, gameObject.tag);
                    //Search for Image in Queue
                    alreadyPresent = false;
                    alreadyPresent = findImage("GreenEnablerImageUI", poojaPlayerScript.collectedEnablers);

                    //Add Image in Queue if not already present
                    if (!alreadyPresent)
                    {
                        addImage("GreenEnablerImageUI", poojaPlayerScript.collectedEnablers);
                    }
                    Destroy(gameObject);
                    break;
                case "SkyEnabler":
                    poojaPlayerScript.setCurrentEnabler(PoojaPlayerController.Enablers.Sky.ToString());
                    poojaPlayerScript.rend.sharedMaterial = poojaPlayerScript.material[1];
                    ScoreManager.Instance.AddScore(value, gameObject.tag);
                    //Search for Image in Queue
                    alreadyPresent = false;
                    alreadyPresent = findImage("SkyEnablerImageUI", poojaPlayerScript.collectedEnablers);

                    //Add Image in Queue if not already present
                    if (!alreadyPresent)
                    {
                        addImage("SkyEnablerImageUI", poojaPlayerScript.collectedEnablers);
                    }
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
                    //Destroy(gameObject);
                    break;

            }

            
        }
        
    }

    private void Update()
    {
        transform.Rotate(0, 0, turnspeed * Time.deltaTime);
    }

    private bool findImage(string tagName, Queue<Image> collectedEnablers) {
        bool alreadyPresent = false;
        foreach (Image value in collectedEnablers)
        {
            if (value.tag == tagName)
            {
                alreadyPresent = true;
            }
        }
        return alreadyPresent;
    }

    private void addImage(string tagName, Queue<Image> collectedEnablers) {
        Image collectedImage;
        GameObject imageObject = GameObject.FindGameObjectWithTag(tagName);
        
        if (imageObject != null)
        {
            collectedImage = imageObject.GetComponent<Image>();
            collectedEnablers.Enqueue(collectedImage);
            collectedImage.enabled = true;

            int count = 0;
            if (collectedEnablers.Count == 4) {
                Image extraImage = collectedEnablers.Dequeue();
                extraImage.enabled = false;
                extraImage.transform.position = new Vector3(854, 0, 0);
            }
            foreach (Image value in collectedEnablers)
            {
                count++;
                value.transform.position += Time.deltaTime * 6500 * Vector3.down;
                
            }
            
        }

    }

    private void shatter()
    {
        ParticleSystem fracture = GetComponent<ParticleSystem>();
        fracture.Play();
    }
}
