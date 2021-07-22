using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deliver : MonoBehaviour
{

    public List<GameObject> planetsList;
    public float verticalDistanceThreshold;
    public TimeManager timeManager;

    public GameObject itemPrefab;
    

    Queue<GameObject> planetsQueue;
    // public LineRenderer roadPrefab;



    void Start()
    {
        planetsQueue = new Queue<GameObject>(planetsList);
    }

    void Update()
    {
        
        // to do:
        // clone a new item at current player position
        // and start to move to the target
        // condition for time to deliver: there is a planet nearby

        if (toDeliver())
        {
            displayFlare();

            GameObject item = Instantiate(itemPrefab) as GameObject;

            item.transform.position = gameObject.transform.position;
            item.GetComponent<BeingDelivered>().targetPlanet = planetsQueue.Dequeue();

            generateRoad();

            timeManager.DoSlowMotion();
        }
    }

    private bool toDeliver()
    {
        if (planetsQueue.Count == 0)
        {
            return false;
        }

        GameObject closestPlanet = planetsQueue.Peek();
        return gameObject.transform.position.y - closestPlanet.transform.position.y < verticalDistanceThreshold;
        
    }

    private void displayFlare()
    {
        ParticleSystem[] particles = GetComponentsInChildren<ParticleSystem>();
        if (particles.Length > 1)
        {
            particles[2].Play();
        }
    }

    private void generateRoad()
    {
        // LineRenderer road = Instantiate(roadPrefab) as LineRenderer;
    }

    
}
