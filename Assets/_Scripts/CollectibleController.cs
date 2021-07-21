using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectibleController : MonoBehaviour
{
    public float turnspeed = 90f;
    private bool isEnablerCollected;
    public GameObject tutorialManager;
    private TutorialManager tm;

    private ScoreManager scoreManager;
    // Start is called before the first frame update

    public bool getEnablerCollecter() {
        return isEnablerCollected;
    }

    private void Start()
    {
        scoreManager = ScoreManager.Instance;
        isEnablerCollected = false;
        tutorialManager = GameObject.FindGameObjectWithTag("TutorialManager");
        if (tutorialManager != null) {
            tm = tutorialManager.GetComponent<TutorialManager>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject playerGO = other.gameObject;
        if (playerGO.tag == "Player")
        {
            if (scoreManager.collect(gameObject.tag))
            {
                scoreManager.displayMessage(gameObject.tag + " is collected!");
                enableSpecialEffect();
                Invoke("DisableObject", 3f);
                //isEnablerCollected = true;
                //if (tm != null)
                //{
                //    tm.setEnablerCollected(true);
                //}
            }
            else
            {
                scoreManager.displayMessage("Bag does not have enough capacity!!");
            }
        }
    }

    private void Update()
    {
        transform.Rotate(0, 0, turnspeed * Time.deltaTime);
    }

    private void DestroyObject()
    {
        Destroy(gameObject);
    }

    private void DisableObject()
    {
        gameObject.SetActive(false);
    }

    private void enableSpecialEffect()
    {
        makeInvisible();
        playSound();
        shatter();
    }

    private void enableSpecialEffect(GameObject playerGO)
    {
        makeInvisible();
        playSound();
        displayCircle(playerGO);
    }

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

    private void makeInvisible()
    {
        //gameObject.SetActive(false);
        gameObject.GetComponentInChildren<MeshRenderer>().enabled = false;
    }

    private void playSound()
    {
        AudioSource sound = gameObject.GetComponent<AudioSource>();
        if (sound != null)
        {
            sound.Play();
        }
    }
}
