using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
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
    [Tooltip("True if you want to make the next generated object locate relative to the previously generated object, instead of the player position")]
    public bool locateBasedOnPrevObj = false;
    [SerializeField] private GameObject prevGeneratedObj;
    [Tooltip("Time interval  between each generation")]
    public float interval = 3f;
    [Tooltip("Amount of the random objects generated each time")]
    private int amount = 1;
    [Tooltip("Random vertical distance between min vertical distance and max vertical distance," +
        " choose between -inf to inf")]
    private float minVerticalDistance = -100f;
    [Tooltip("Random vertical distance between min vertical distance and max vertical distance," +
        " choose between -inf to inf")]
    private float maxVerticalDistance = -200f;

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
    public float deActivateDistance = 150f;

    //object pooling to optimize runtime smoothness
    [Header("Object Pooling Setting")]
    [Tooltip("How many instance of each prefab to pre-initiate, choose between 0 to inf")]
    private int amountToPool = 10;
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
            Debug.Log("******************************************************");
            Debug.Log(objIndex + " is " + objectToPool.tag);
            Debug.Log("******************************************************");
            for (int i = 0; i < amountToPool; i++)
            {
                temp = Instantiate(objectToPool);
                temp.SetActive(false);
                Transform t = temp.transform;
                foreach (Transform tr in t)
                {
                    MeshRenderer mr = tr.GetComponent<MeshRenderer>();
                    if (mr == null)
                    {
                        Debug.Log("MeshRenderer is absent");
                    }
                    else {
                        Debug.Log("MeshRenderer is present "+mr.materials[0]);
                    }
                    //if (tr.tag == "Balloon")
                    //{

                    //    //rend = tr.GetComponent<Renderer>();
                    //    rend = tr.transform.GetChild(0).gameObject.GetComponent<Renderer>();
                    //    rend.enabled = false;
                    //    rend.sharedMaterial = material[x];
                    //}
                }

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
            // If object is in pooled object, simply reuse them
            GameObject newObj = GetPooledObject(objType);
            if (newObj != null)
            {
                //if (locateBasedOnPrevObj && prevGeneratedObj != null)
                //{
                //    // Randomly generate object position
                //    Vector3 newObjLoc = new Vector3(newObj.transform.position.x,
                //    Random.Range(minVerticalDistance, maxVerticalDistance) + prevGeneratedObj.transform.position.y,
                //    newObj.transform.position.z
                //    );
                //    newObj.transform.position = newObjLoc;
                //}
                //else
                //{

                float rangeVal = Random.Range(minVerticalDistance, maxVerticalDistance);
                    float temp = rangeVal + playerLoc.y;
                    // Randomly generate object position
                    Vector3 newObjLoc = new Vector3(newObj.transform.position.x,
                    temp,
                    newObj.transform.position.z
                    );
                    newObj.transform.position = newObjLoc;
                //}

                // Randomly generate object scale
                //if (minScale < 0) minScale = 0;
                //if (maxScale < 0) maxScale = 0;
                //float randomScale = Random.Range(minScale, maxScale);
                //newObj.transform.localScale = new Vector3(randomScale, randomScale, randomScale);

                // Randomly generate object rotation
                //Vector3 rotateAxis = new Vector3(System.Convert.ToSingle(rotateX),
                //    System.Convert.ToSingle(rotateY),
                //    System.Convert.ToSingle(rotateZ));
                //newObj.transform.Rotate(rotateAxis, Random.Range(minRotate, maxRotate), Space.World);
                newObj.SetActive(true); //need to be set inactive once not in use
                Debug.Log("Player Y " + playerLoc.y);
                Debug.Log("Object Y " + temp);
                Debug.Log("Range Value " + rangeVal);
                Debug.Log("minVerticalDistance " + minVerticalDistance);
                Debug.Log("maxVerticalDistance " + maxVerticalDistance);

                // Set deactivate distance for the object, so object automatically deactivate after certain distance from player
                if (newObj.GetComponent<DeactivateLevel>() == null)
                {
                    newObj.AddComponent<DeactivateLevel>();
                    DeactivateLevel deactivateObj = newObj.GetComponent<DeactivateLevel>();
                    deactivateObj.player = player;
                    //deactivateObj.setDeActivateDis(deActivateDistance);
                }
                prevGeneratedObj = newObj;

            }
        }
    }

    public GameObject GetPooledObject(int objType)
    {
        //Debug.Log("Called");
        for (int i = 0; i < amountToPool; i++)
        {
            List<GameObject> pooledObjects = pooledObjectsHash[objType];
            Debug.Log(pooledObjects.Count);
            if (pooledObjects[i] == null)
            {
                Debug.Log("Object "+i+" of type "+objType+" is destroyed");
            }
            else {
                if (!pooledObjects[i].activeInHierarchy)
                {
                    //Debug.Log(i);
                    return pooledObjects[i];
                }
            }
            
        }
        return null;
    }
}
