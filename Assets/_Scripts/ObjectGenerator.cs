using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ObjectGenerator : MonoBehaviour
{
    public GameObject[] objects;
    public GameObject player;
    //public int _NumofBonds = 4;
   [Header("Bounding Coners")]
    public GameObject LeftUpper;
    public GameObject RightUpper;
    public GameObject LeftLower;
    public GameObject RightLower;
    [Header("Generation Specs")]
    [Tooltip("Time interval  between each generation")]
    public float interval = 3f;
    [Tooltip("Amount of the random objects generated each time")]
    public int amount = 1;
    [Tooltip("Random vertical distance between min vertical distance and max vertical distance," +
        " choose between -inf to inf")]
    public float minVerticalDistance = -120f;
    [Tooltip("Random vertical distance between min vertical distance and max vertical distance," +
        " choose between -inf to inf")]
    public float maxVerticalDistance = -80f;

    [Tooltip("Random scale down, choose between 0 to inf")]
    public float minScale = 0.3f;
    [Tooltip("Random scale up, choose between 0 to inf")]
    public float maxScale = 1f;
    [Header("Rotation Axis")]
    public bool rotateX;
    public bool rotateY = true;
    public bool rotateZ;
    [Tooltip("Minimum rotate degrees, choose between 0 to 360")]
    public float minRotate = 0f;
    [Tooltip("Maximum rotate degrees, choose between 0 to 360")]
    public float maxRotate = 180f;

    [Header("Object Deactivate Setting")]
    [Tooltip("Distance between object to player before destroying(above player), choose between 0 to inf")]
    public float deActivateDistance = 10f;

    //object pooling to optimize runtime smoothness
    [Header("Object Pooling Setting")]
    [Tooltip("How many instance of each prefab to pre-initiate, choose between 0 to inf")]
    public int amountToPool = 5;
    private IEnumerator coroutine;
    private List<List<GameObject>> pooledObjectsHash;

    private void Reset()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        LeftUpper = GameObject.Find("LeftUpper");
        RightUpper = GameObject.Find("RightUpper");
        LeftLower = GameObject.Find("LeftLower");
        RightLower = GameObject.Find("RightLower");
    }

    private void Awake()
    {
    }

    private void Start()
    {
        // pre-instantiate object pool
        pooledObjectsHash = new List<List<GameObject>>();

        GameObject temp;
        List<GameObject> pooledObjects = new List<GameObject>();
        for (int objIndex = 0; objIndex < objects.Length; objIndex++)
        {
            GameObject objectToPool = objects[objIndex];
            for (int i = 0; i < amountToPool; i++)
            {
                temp = Instantiate(objectToPool);
                temp.SetActive(false);
                pooledObjects.Add(temp);
                
            }
            pooledObjectsHash.Insert(objIndex, pooledObjects);
            pooledObjects = new List<GameObject>();
        }

        // start generating object once game starts
        coroutine = GeneratorRoutine(interval);
        StartCoroutine(coroutine);
    }

    void Update()
    {
    }

    private IEnumerator GeneratorRoutine(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            GenerateObj(amount);
        }
    }


    private void GenerateObj(int quantity)
    {
        Vector3 playerLoc = player.transform.position;
        for (int i = 0; i < quantity; i++)
        {
            
            int objType = Random.Range(0, objects.Length);
            //GameObject newObj = Instantiate(objType, newObjLoc, objType.transform.rotation);
            Debug.Log(objType);
            // If object is in pooled object, simply reuse them
            GameObject newObj = GetPooledObject(objType);
            if (newObj != null)
            {
                // Randomly generate object position
                Vector3 newObjLoc = new Vector3(Random.Range(LeftUpper.transform.position.x, RightUpper.transform.position.x),
                Random.Range(minVerticalDistance, maxVerticalDistance) + playerLoc.y,
                Random.Range(LeftLower.transform.position.z, RightLower.transform.position.z)
                );
                newObj.transform.position = newObjLoc;

                // Randomly generate object scale
                if (minScale < 0) minScale = 0;
                if (maxScale < 0) maxScale = 0;
                float randomScale = Random.Range(minScale, maxScale);
                newObj.transform.localScale = new Vector3(randomScale,randomScale,randomScale);

                // Randomly generate object rotation
                Vector3 rotateAxis = new Vector3(System.Convert.ToSingle(rotateX),
                    System.Convert.ToSingle(rotateY),
                    System.Convert.ToSingle(rotateZ));
                newObj.transform.Rotate(rotateAxis, Random.Range(minRotate, maxRotate), Space.World);
                newObj.SetActive(true); //need to be set inactive once not in use
                // Set deactivate distance for the object, so object automatically deactivate after certain distance from player
                newObj.AddComponent<DeactivateObj>();
                DeactivateObj deactivateObj = newObj.GetComponent<DeactivateObj>();
                deactivateObj.player = player;
                deactivateObj.deActivateDis = deActivateDistance;
            }
        }
    }

    public GameObject GetPooledObject(int objType)
    {
        for(int i = 0; i < amountToPool; i++)
        {
            Debug.Log(pooledObjectsHash[objType]);
            List <GameObject> pooledObjects = pooledObjectsHash[objType];
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        return null;
    }
}
