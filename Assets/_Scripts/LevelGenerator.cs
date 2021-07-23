using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class LevelGenerator : MonoBehaviour
{
    public int[] moleculeLengths;
    public GameObject[] molecules;
    public GameObject player;
    public GameObject spaceStation;
    public int maxNumberOfObjects;
    [SerializeField] private static int currentNumberOfObjects;
    [SerializeField] private static GameObject prevGeneratedObj;
    [SerializeField] private static int prevGeneratedObjSize;


    [Tooltip("Time interval  between each generation")]
    public float interval = 3f;
    [Tooltip("Amount of the random objects generated each time")]
    private int amount = 1;

    //object pooling to optimize runtime smoothness
    [Header("Object Pooling Setting")]
    [Tooltip("How many instance of each prefab to pre-initiate, choose between 0 to inf")]
    private int amountToPool = 1;
    private IEnumerator coroutine;
    private List<List<Tuple<GameObject,int>>> pooledObjectsHash;

    //for analytics
    private static int _numOfLevelGenerated = 0;

/*    #region SingletonPattern
    private static LevelGenerator _instance;
    public static LevelGenerator Instance { get { return _instance; } }
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    #endregion*/
    
    private void Start()
    {
        Time.timeScale = 1f;
        // pre-instantiate object pool
        pooledObjectsHash = new List<List<Tuple<GameObject, int>>>();
        currentNumberOfObjects = 0;
        GameObject atomicObject;
        int atomicObjectSize;
        Tuple<GameObject, int> tupleObj;
        List<Tuple<GameObject,int>> pooledObjects = new List<Tuple<GameObject,int>>();
        for (int objIndex = 0; objIndex < molecules.Length; objIndex++)
        {
            GameObject objectToPool = molecules[objIndex];
            int objectToPoolSize = moleculeLengths[objIndex];
            //Debug.Log("******************************************************");
            //Debug.Log(objIndex + " is " + objectToPool.tag);
            //Debug.Log("******************************************************");
            for (int i = 0; i < amountToPool; i++)
            {
                atomicObject = Instantiate(objectToPool);
                atomicObject.SetActive(false);
                atomicObjectSize = moleculeLengths[objIndex];
                tupleObj = new Tuple<GameObject, int>(atomicObject, atomicObjectSize);
                pooledObjects.Add(tupleObj);

            }
            pooledObjectsHash.Insert(objIndex, pooledObjects);
            pooledObjects = new List<Tuple<GameObject, int>>();
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
        while (currentNumberOfObjects < maxNumberOfObjects)
        {
            yield return new WaitForSeconds(waitTime);
            Debug.Log(maxNumberOfObjects);
            Debug.Log(currentNumberOfObjects);
            GenerateObj(amount);
        }
        Debug.Log("End of Level!!!");
        GenerateEndOfLevel();
    }

    private void GenerateEndOfLevel() {
        float temp = (prevGeneratedObj.transform.position.y * -1) + prevGeneratedObjSize + (spaceStation.transform.position.y * -1) + 130;
        // Randomly generate object position
        Vector3 newObjLoc = new Vector3(spaceStation.transform.position.x,
        (temp * -1),
        spaceStation.transform.position.z
        );
        spaceStation.transform.position = newObjLoc;
        spaceStation.SetActive(true);
        PoojaPlayerController.setEndPostion((temp - 20)*-1);

    }

    private void GenerateObj(int quantity)
    {
        Vector3 playerLoc = player.transform.position;
        for (int i = 0; i < quantity; i++)
        {

            int objType = UnityEngine.Random.Range(0, molecules.Length);
            // If object is in pooled object, simply reuse them
            Tuple<GameObject,int> newTupleObj = GetPooledObject(objType);
            if (newTupleObj != null && newTupleObj.Item1 != null)
            {
                GameObject newObj = newTupleObj.Item1;
                int newObjSize = newTupleObj.Item2;
                if (newObjSize == 0) {
                    newObjSize = 200;
                }

                if (prevGeneratedObj != null)
                {
                    float temp = (prevGeneratedObj.transform.position.y* -1) + prevGeneratedObjSize + (newObj.transform.position.y * -1) + 30;
                    // Randomly generate object position
                    Vector3 newObjLoc = new Vector3(newObj.transform.position.x,
                    (temp * -1),
                    newObj.transform.position.z
                    );
                    newObj.transform.position = newObjLoc;
                    prevGeneratedObj = newObj;
                    prevGeneratedObjSize = newObjSize;
                }
                else
                {
                    float temp = (playerLoc.y*-1) + (newObj.transform.position.y * -1) + 30;
                    // Randomly generate object position
                    Vector3 newObjLoc = new Vector3(newObj.transform.position.x,
                    (temp*-1),
                    newObj.transform.position.z
                    );
                    newObj.transform.position = newObjLoc;
                    prevGeneratedObj = newObj;
                    prevGeneratedObjSize = newObjSize;
                }

                newObj.SetActive(true); //need to be set inactive once not in use
                //newObj.GetComponentInChildren<MeshRenderer>().enabled = true; 

                // Set deactivate distance for the object, so object automatically deactivate after certain distance from player
                if (newObj.GetComponent<DeactivateLevel>() == null)
                {
                    newObj.AddComponent<DeactivateLevel>();
                    DeactivateLevel deactivateObj = newObj.GetComponent<DeactivateLevel>();
                    deactivateObj.player = player;
                    deactivateObj.setDeActivateDis(newObjSize+20);
                }
                prevGeneratedObj = newObj;
                //_numOfLevelGenerated += 1;
            }
        }
    }

    public Tuple<GameObject,int> GetPooledObject(int objType)
    {
        for (int i = 0; i < amountToPool; i++)
        {
            List<Tuple<GameObject,int>> pooledObjects = pooledObjectsHash[objType];
            //Debug.Log(pooledObjects.Count);
            Tuple<GameObject, int> tupleObject = pooledObjects[i];
            if (tupleObject == null && tupleObject.Item1 != null)
            {
                Debug.Log("Object "+i+" of type "+ tupleObject.Item1 + " is destroyed");
            }
            else {
                if (!tupleObject.Item1.activeInHierarchy)
                {
                    currentNumberOfObjects++;
                    return tupleObject;
                }
            }
            
        }
        return null;
    }

/*    public void SendEAnalytics()
    {
        // number of enabler generated
        Debug.Log(Analytics.CustomEvent("LevelStats", new Dictionary<string, object>
            {
                {"Level_Generated_Before_Death", _numOfLevelGenerated}
            }));
    }*/
}
