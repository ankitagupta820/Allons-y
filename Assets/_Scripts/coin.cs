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

            // shatter();
            gameObject.GetComponentInChildren<MeshRenderer>().enabled = false;

            switch (gameObject.tag) {

                case "YellowEnabler":
                    //poojaPlayerScript.setCurrentEnabler(PoojaPlayerController.Enablers.Yellow.ToString());
                    //poojaPlayerScript.rend.sharedMaterial = poojaPlayerScript.material[2];
                    ScoreManager.Instance.AddScore(value, gameObject.tag);

                    //Search for Image in Queue
                    bool alreadyPresent = false;
                    alreadyPresent = findImage("YellowEnablerImageUI", poojaPlayerScript.collectedEnablers);
                    
                    //Add Image in Queue if not already present
                    if (!alreadyPresent)
                    {
                        addImage("YellowEnablerImageUI", poojaPlayerScript.collectedEnablers);
                    }

                    shatter();
                    // Destroy(gameObject);
                    Invoke("DisableObject", 3f);
                    break;
                case "RedEnabler":
                    //poojaPlayerScript.setCurrentEnabler(PoojaPlayerController.Enablers.Red.ToString());
                    //poojaPlayerScript.rend.sharedMaterial = poojaPlayerScript.material[1];
                    ScoreManager.Instance.AddScore(value, gameObject.tag);
                    //Search for Image in Queue
                    alreadyPresent = false;
                    alreadyPresent = findImage("RedEnablerImageUI", poojaPlayerScript.collectedEnablers);

                    //Add Image in Queue if not already present
                    if (!alreadyPresent)
                    {
                        addImage("RedEnablerImageUI", poojaPlayerScript.collectedEnablers);
                    }

                    shatter();
                    // Destroy(gameObject);
                    Invoke("DisableObject", 3f);
                    break;
                case "BlueEnabler":
                    //poojaPlayerScript.setCurrentEnabler(PoojaPlayerController.Enablers.Blue.ToString());
                    //poojaPlayerScript.rend.sharedMaterial = poojaPlayerScript.material[1];
                    ScoreManager.Instance.AddScore(value, gameObject.tag);
                    //Search for Image in Queue
                    alreadyPresent = false;
                    alreadyPresent = findImage("BlueEnablerImageUI", poojaPlayerScript.collectedEnablers);

                    //Add Image in Queue if not already present
                    if (!alreadyPresent)
                    {
                        addImage("BlueEnablerImageUI", poojaPlayerScript.collectedEnablers);
                    }

                    shatter();
                    // Destroy(gameObject);
                    Invoke("DisableObject", 3f);
                    break;
                case "GreenEnabler":
                    //poojaPlayerScript.setCurrentEnabler(PoojaPlayerController.Enablers.Green.ToString());
                    //poojaPlayerScript.rend.sharedMaterial = poojaPlayerScript.material[1];
                    ScoreManager.Instance.AddScore(value, gameObject.tag);
                    //Search for Image in Queue
                    alreadyPresent = false;
                    alreadyPresent = findImage("GreenEnablerImageUI", poojaPlayerScript.collectedEnablers);

                    //Add Image in Queue if not already present
                    if (!alreadyPresent)
                    {
                        addImage("GreenEnablerImageUI", poojaPlayerScript.collectedEnablers);
                    }

                    shatter();
                    // Destroy(gameObject);
                    Invoke("DisableObject", 3f);
                    break;
                case "SkyEnabler":
                    //poojaPlayerScript.setCurrentEnabler(PoojaPlayerController.Enablers.Sky.ToString());
                    //poojaPlayerScript.rend.sharedMaterial = poojaPlayerScript.material[1];
                    ScoreManager.Instance.AddScore(value, gameObject.tag);
                    //Search for Image in Queue
                    alreadyPresent = false;
                    alreadyPresent = findImage("SkyEnablerImageUI", poojaPlayerScript.collectedEnablers);

                    //Add Image in Queue if not already present
                    if (!alreadyPresent)
                    {
                        addImage("SkyEnablerImageUI", poojaPlayerScript.collectedEnablers);
                    }

                    shatter();
                    // Destroy(gameObject);
                    Invoke("DisableObject", 3f);
                    break;
                case "SearchItemYellow":
                    if (poojaPlayerScript.getCurrentEnabler() == PoojaPlayerController.Enablers.Yellow.ToString() + "EnablerImageUI")
                    {
                        //poojaPlayerScript.setCurrentEnabler("");
                        //poojaPlayerScript.rend.sharedMaterial = poojaPlayerScript.material[0];
                        //ScoreManager.Instance.AddScore(value, "Search Item "+ gameObject.tag);
                        


                        //ScoreManager.Instance.AddScore(value, "Current Value " + poojaPlayerScript.currentEnabler + " To String value " + PoojaPlayerController.Enablers.Yellow.ToString());


                        displayCircle(playerGO);
                        // Destroy(gameObject);
                        
                        if (collectSearchObject("SearchObjective" + PoojaPlayerController.Enablers.Yellow.ToString())) {
                            ScoreManager.Instance.AddScore(value, "Search Item " + PoojaPlayerController.Enablers.Yellow.ToString());
                            Invoke("DisableObject", 3f);
                        }
                    }
                    else {
                        ScoreManager.Instance.AddScore(0, "Cannot be");
                    }
                    break;
                case "SearchItemRed":
                    if (poojaPlayerScript.getCurrentEnabler() == PoojaPlayerController.Enablers.Red.ToString() + "EnablerImageUI")
                    {
                        
                        if (collectSearchObject("SearchObjective" + PoojaPlayerController.Enablers.Red.ToString()))
                        {
                            ScoreManager.Instance.AddScore(value, "Search Item " + PoojaPlayerController.Enablers.Red.ToString());
                            displayCircle(playerGO);
                            Invoke("DisableObject", 3f);
                        }
                    }
                    else
                    {
                        ScoreManager.Instance.AddScore(0, "Cannot be");
                    }
                    break;
                case "SearchItemBlue":
                    if (poojaPlayerScript.getCurrentEnabler() == PoojaPlayerController.Enablers.Blue.ToString() + "EnablerImageUI")
                    {               
                        if (collectSearchObject("SearchObjective" + PoojaPlayerController.Enablers.Blue.ToString()))
                        {
                            ScoreManager.Instance.AddScore(value, "Search Item " + PoojaPlayerController.Enablers.Blue.ToString());
                            displayCircle(playerGO);
                            Invoke("DisableObject", 3f);
                        }
                    }
                    else
                    {
                        ScoreManager.Instance.AddScore(0, "Cannot be");
                    }
                    break;
                case "SearchItemGreen":
                    if (poojaPlayerScript.getCurrentEnabler() == PoojaPlayerController.Enablers.Green.ToString() + "EnablerImageUI")
                    {
                        
                        if (collectSearchObject("SearchObjective" + PoojaPlayerController.Enablers.Green.ToString()))
                        {
                            ScoreManager.Instance.AddScore(value, "Search Item " + PoojaPlayerController.Enablers.Green.ToString());
                            displayCircle(playerGO);
                            Invoke("DisableObject", 3f);
                        }
                    }
                    else
                    {
                        ScoreManager.Instance.AddScore(0, "Cannot be");
                    }
                    break;
                case "SearchItemSky":
                    if (poojaPlayerScript.getCurrentEnabler() == PoojaPlayerController.Enablers.Sky.ToString() + "EnablerImageUI")
                    {            
                        if (collectSearchObject("SearchObjective" + PoojaPlayerController.Enablers.Sky.ToString()))
                        {
                            ScoreManager.Instance.AddScore(value, "Search Item " + PoojaPlayerController.Enablers.Sky.ToString());
                            displayCircle(playerGO);
                            Invoke("DisableObject", 3f);
                        }
                    }
                    else
                    {
                        ScoreManager.Instance.AddScore(0, "Cannot be");
                    }
                    break;
                default:
                    //ScoreManager.Instance.AddScore(value, "Object Tag " + gameObject.tag);
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
                //extraImage.transform.position = new Vector3(854, 0, 0);
                extraImage.transform.position += Time.deltaTime * 6500 * -3 * Vector3.down;
            }
            foreach (Image value in collectedEnablers)
            {
                count++;
                value.transform.position += Time.deltaTime * 6500 * Vector3.down;
                
            }
            
        }

    }

    private bool collectSearchObject(string tagName)
    {
        //Image collectedImage;
        bool collected = false;
        GameObject imageObject = GameObject.FindGameObjectWithTag(tagName);

        if (imageObject != null)
        {
            //collectedImage = imageObject.GetComponent<Image>();

            Transform t = imageObject.transform;
            foreach (Transform tr in t)
            {

                //Text text = tr.transform.GetChild(0).gameObject.GetComponent<Text>();
                Text text = tr.GetComponent<Text>();
                int i = int.Parse(text.text);
                if (i > 0) {
                    collected = true;
                    i--;
                    Debug.Log(i);
                    text.text = i+"";
                }
                
            }
        }
        return collected;
    }

    private void DestroyObject()
    {
        Destroy(gameObject);
    }

    private void DisableObject()
    {
        gameObject.SetActive(false);
    }

    // shatter effect
    private void shatter()
    {
        if (GetComponent<ParticleSystem>() != null) {
            ParticleSystem fracture = GetComponent<ParticleSystem>();
            fracture.Play();
        }
       
    }

    private void displayCircle(GameObject playerGO)
    {
        ParticleSystem[] particles = playerGO.GetComponentsInChildren<ParticleSystem>();
        if (particles.Length > 1)
        {
            particles[1].Play();
        }
    }
}
