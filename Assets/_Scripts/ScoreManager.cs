using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Analytics;

public class ScoreManager : MonoBehaviour
{
    private static ScoreManager _instance;

    public static ScoreManager Instance { get { return _instance; } }

    public GameObject player;
    private Vector3 initialPos;
    private Rigidbody playerRB;

    public GameObject Score;
    public GameObject Dis;
    public GameObject Speed;

    public float _scoreF = 0f;
    public int _dis = 0;
    public int _speed = 0;
    public float gameTimer = 0f;
    public string timerString;
    private float theScore = 0;
    private int countCollectibles;

    private string type1CollectibleTagName;
    private string type2CollectibleTagName;
    private string type3CollectibleTagName;
    private string type4CollectibleTagName;
    private string type5CollectibleTagName;

    public GameObject alertDisplay;
    public GameObject capacityDisplay;
    private Dictionary<string, int> collectibleVolumeMap = new Dictionary<string, int>();
    private Dictionary<string, int> collectibleScoreMap = new Dictionary<string, int>();
    private Dictionary<string, string> collectibleUITagMap = new Dictionary<string, string>();

    [SerializeField] private static int bagTotalCapacity;
    [SerializeField] private static int bagRemainingCapacity;
    [SerializeField] private static Dictionary<string, int> collectibleCollectedMap = new Dictionary<string, int>();


    // Singleton Pattern
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

    void Start()
    {
        type1CollectibleTagName = "RedEnabler";
        type2CollectibleTagName = "YellowEnabler";
        type3CollectibleTagName = "BlueEnabler";
        type4CollectibleTagName = "GreenEnabler";
        type5CollectibleTagName = "SkyEnabler";
        playerRB = player.GetComponent<Rigidbody>();
        initialPos = player.transform.position;
        countCollectibles = 0;
        defineCollectibleVolumeMap();
        defineCollectibleScoreMap();
        defineCollectibleCollectedMap();
        defineCollectibleUITagMap();
        bagTotalCapacity = int.Parse(capacityDisplay.GetComponent<Text>().text);
        bagRemainingCapacity = int.Parse(capacityDisplay.GetComponent<Text>().text);
    }

    void Update()
    {
        CalcSpeed();
        CalcDis(); gameTimer += Time.deltaTime;

        int secs = (int)(gameTimer % 60);
        int mins = (int)(gameTimer / 60) % 60;
        int hrs = (int)(gameTimer / 3600) % 24;

        //create a variable "timerString" to return the time in string

        timerString = string.Format("{0:0}:{1:00}:{2:00}", hrs, mins, secs);
        _scoreF = float.Parse(Score.GetComponent<Text>().text);
        _dis = int.Parse(Dis.GetComponent<Text>().text);
        _speed = int.Parse(Speed.GetComponent<Text>().text);
    }

    void OnDestroy()
    {
        SendAnalytics();
    }

    private void defineCollectibleVolumeMap() {
        collectibleVolumeMap[type1CollectibleTagName] = 1;
        collectibleVolumeMap[type2CollectibleTagName] = 2;
        collectibleVolumeMap[type3CollectibleTagName] = 5;
        collectibleVolumeMap[type4CollectibleTagName] = 10;
        collectibleVolumeMap[type5CollectibleTagName] = 15;
    }

    private void defineCollectibleScoreMap()
    {
        collectibleScoreMap[type1CollectibleTagName] = 10;
        collectibleScoreMap[type2CollectibleTagName] = 20;
        collectibleScoreMap[type3CollectibleTagName] = 50;
        collectibleScoreMap[type4CollectibleTagName] = 100;
        collectibleScoreMap[type5CollectibleTagName] = 150;
    }

    private void defineCollectibleCollectedMap()
    {
        collectibleCollectedMap[type1CollectibleTagName] = 0;
        collectibleCollectedMap[type2CollectibleTagName] = 0;
        collectibleCollectedMap[type3CollectibleTagName] = 0;
        collectibleCollectedMap[type4CollectibleTagName] = 0;
        collectibleCollectedMap[type5CollectibleTagName] = 0;
    }

    private void defineCollectibleUITagMap() {
        collectibleUITagMap[type1CollectibleTagName] = "RedEnablerImageUI";
        collectibleUITagMap[type2CollectibleTagName] = "YellowEnablerImageUI";
        collectibleUITagMap[type3CollectibleTagName] = "BlueEnablerImageUI";
        collectibleUITagMap[type4CollectibleTagName] = "GreenEnablerImageUI";
        collectibleUITagMap[type5CollectibleTagName] = "SkyEnablerImageUI";
    }

    private void CalcSpeed()
    {
        Speed.GetComponent<Text>().text = playerRB.velocity.magnitude.ToString("F0");
    }

    private void CalcDis()
    {
        Dis.GetComponent<Text>().text = Vector3.Distance(initialPos, player.transform.position).ToString("F0");
    }

    private IEnumerator ShowAlert(string tagValue)
    {
        alertDisplay.SetActive(true);
        alertDisplay.transform.GetChild(0).gameObject.GetComponent<Text>().text = tagValue + " COLLECTED!";
        //Coinalert.GetComponent<Text>().text = tagValue + " COLLECTED!";
        yield return new WaitForSeconds(3f);
        alertDisplay.transform.GetChild(0).gameObject.GetComponent<Text>().text = "";
        alertDisplay.SetActive(false);
    }

    public void AddScore(int score, string tagValue)
    {
        theScore += score;
        Score.GetComponent<Text>().text = theScore.ToString("F0");
        StartCoroutine(ShowAlert(tagValue));
        incrementCountCollectibles();

        //Debug.Log("***************Manish******************");
        //Debug.Log(getCountCollectibles());
        //Red_Collectible.GetComponent<Text>().text = theScore.ToString("F0");
        //Yellow_collectible.GetComponent<Text>().text = theScore.ToString("F0");
    }

    public bool hasCapacity(int requiredCapacity) {
        bool hasCapacityVar = false;
        if (bagRemainingCapacity - requiredCapacity >= 0) {
            hasCapacityVar = true;
        }
        return hasCapacityVar;
    }

    public bool collect(string collectibleTagName)
    {
        bool collectedVar = false;
        Debug.Log(collectibleTagName);
        Debug.Log(collectibleVolumeMap[collectibleTagName]);
        int requiredCapacity = collectibleVolumeMap[collectibleTagName];
        if (hasCapacity(requiredCapacity))
        {
            //Add Score
            AddScore(collectibleScoreMap[collectibleTagName], collectibleTagName);

            //Update Capacity Display
            //Reduced the bag capacity
            bagRemainingCapacity = bagRemainingCapacity - requiredCapacity;
            capacityDisplay.GetComponent<Text>().text = bagRemainingCapacity.ToString("F0");

            //Update Collectible Number Display
            int collectiblesCollected = getCollectibleCollectedNumber(collectibleTagName);
            collectiblesCollected++;
            setCollectibleCollectedNumber(collectibleTagName, collectiblesCollected);
            string UIScoreComponentDisplayTag = collectibleUITagMap[collectibleTagName];
            GameObject UIScoreComponentDisplay = GameObject.FindGameObjectWithTag(UIScoreComponentDisplayTag);
            UIScoreComponentDisplay.GetComponent<Text>().text = collectiblesCollected.ToString("F0");


            collectedVar = true;
        }
        return collectedVar;
    }

    public bool popOutCollectibles(string collectibleTagName, int number)
    {
        bool emptiedVar = false;

        int collectiblesCollected = getCollectibleCollectedNumber(collectibleTagName);
        //Check if number of collectibles to be poped out is more than or equal to what we have collected
        if (collectiblesCollected - number >= 0)
        {
            
            //Update Capacity Display
            //Increase the bag capacity
            int singleCollectibleCapacity = collectibleVolumeMap[collectibleTagName];
            int consumedCapacity = singleCollectibleCapacity * number;
            bagRemainingCapacity = bagRemainingCapacity + consumedCapacity;
            capacityDisplay.GetComponent<Text>().text = bagRemainingCapacity.ToString("F0");

            //Update Collectible Number Display
            collectiblesCollected = collectiblesCollected - number;
            setCollectibleCollectedNumber(collectibleTagName, collectiblesCollected);
            string UIScoreComponentDisplayTag = collectibleUITagMap[collectibleTagName];
            GameObject UIScoreComponentDisplay = GameObject.FindGameObjectWithTag(UIScoreComponentDisplayTag);
            UIScoreComponentDisplay.GetComponent<Text>().text = collectiblesCollected.ToString("F0");

            emptiedVar = true;
        }

        return emptiedVar;
    }

    public bool popOutAllCollectiblesForDelivery(string collectibleTagName, int deliveryScore)
    {
        bool emptiedVar = false;

        int collectiblesCollected = getCollectibleCollectedNumber(collectibleTagName);
        //Check if there are any collectibles collected at all
        if (collectiblesCollected > 0)
        {
            //Add Score
            AddScore(collectibleScoreMap[collectibleTagName]* collectiblesCollected * deliveryScore, collectibleTagName);

            //Update Capacity Display
            //Increase the bag capacity
            int singleCollectibleCapacity = collectibleVolumeMap[collectibleTagName];
            int consumedCapacity = singleCollectibleCapacity * collectiblesCollected;
            bagRemainingCapacity = bagRemainingCapacity + consumedCapacity;
            capacityDisplay.GetComponent<Text>().text = bagRemainingCapacity.ToString("F0");

            //Update Collectible Number Display
            collectiblesCollected = 0;
            setCollectibleCollectedNumber(collectibleTagName, collectiblesCollected);
            string UIScoreComponentDisplayTag = collectibleUITagMap[collectibleTagName];
            GameObject UIScoreComponentDisplay = GameObject.FindGameObjectWithTag(UIScoreComponentDisplayTag);
            UIScoreComponentDisplay.GetComponent<Text>().text = collectiblesCollected.ToString("F0");

            emptiedVar = true;
        }

        return emptiedVar;
    }

    public void displayMessage(string message) {
        StartCoroutine(displayAlertFor3Seconds(message));
    }

    private IEnumerator displayAlertFor3Seconds(string message) {
        alertDisplay.SetActive(true);
        alertDisplay.transform.GetChild(0).gameObject.GetComponent<Text>().text = message;
        yield return new WaitForSeconds(3f);
        alertDisplay.transform.GetChild(0).gameObject.GetComponent<Text>().text = "";
        alertDisplay.SetActive(false);
    }

    public void SendAnalytics()
    {
        Debug.Log(Analytics.CustomEvent("PlayerStats", new Dictionary<string, object>
          {
            { "Score", _scoreF},
            { "Distance", _dis},
            { "Speed", _speed},
            { "Time_Elapsed", timerString},
            {"Number_of_Collectables", countCollectibles}

          }));
    }

    public void incrementCountCollectibles()
    {
        countCollectibles++;
    }

    public int getCountCollectibles()
    {
        return countCollectibles;
    }

    public int getBagTotalCapacity() {
        return bagTotalCapacity;
    }

    public void setBagTotalCapacity(int newBagCapacity) {
        bagTotalCapacity = newBagCapacity;
    }
    
    public int getBagRemainingCapacity()
    {
        return bagRemainingCapacity;
    }

    public void setBagRemainingCapacity(int newBagRemainingCapacity)
    {
        bagRemainingCapacity = newBagRemainingCapacity;
    }

    public static int getCollectibleCollectedNumber(string collectibleTag) {
        return collectibleCollectedMap[collectibleTag];
    }

    public static void setCollectibleCollectedNumber(string collectibleTag, int value)
    {
        collectibleCollectedMap[collectibleTag] = value;
    }

}