using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Analytics;

public class ScoreManager : MonoBehaviour
{
    private static ScoreManager _instance;

    public static ScoreManager Instance { get { return _instance; } }
    public TimeManager timeManager;  // TODO: change to singleton above

    public GameObject player;
    private Vector3 initialPos;
    private Rigidbody playerRB;

    public GameObject Score;
    public GameObject Dis;
    public GameObject Speed;
    public GameObject type1PlanetAlertGO;
    public GameObject type2PlanetAlertGO;
    public GameObject type3PlanetAlertGO;
    public GameObject type1CollectibleBalloonSprite;
    public GameObject type2CollectibleBalloonSprite;
    public GameObject type3CollectibleBalloonSprite;
    public GameObject type1Enabler;
    public GameObject type2Enabler;
    public GameObject type3Enabler;

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

    private string type1PlanetTagName;
    private string type2PlanetTagName;
    private string type3PlanetTagName;

    private string type1PlanetAlertTagName;
    private string type2PlanetAlertTagName;
    private string type3PlanetAlertTagName;

    private string type1CollectibleBalloonSpriteTag;
    private string type2CollectibleBalloonSpriteTag;
    private string type3CollectibleBalloonSpriteTag;

    private int alertVerticalDistanceThreshold;
    private int deliveryVerticalDistanceThreshold;

    public GameObject alertDisplay;
    public GameObject capacityDisplay;
    private Dictionary<string, int> collectibleVolumeMap = new Dictionary<string, int>();
    private Dictionary<string, int> collectibleScoreMap = new Dictionary<string, int>();
    private Dictionary<string, string> collectibleUITagMap = new Dictionary<string, string>();
    private static Dictionary<string, string> planetUITagMap = new Dictionary<string, string>();
    private static Dictionary<string, string> planetTagCollectibleTagMap = new Dictionary<string, string>();
    private static Dictionary<string, GameObject> planetAlertTagPlanetAlertGOMap = new Dictionary<string, GameObject>();
    private static Dictionary<string, List<string>> planetTagAlertMessageListMap = new Dictionary<string, List<string>>();
    private static Dictionary<string, List<string>> planetTagDeliveryMessageListMap = new Dictionary<string, List<string>>();
    private static Dictionary<string, List<string>> planetTagSuccessMessageListMap = new Dictionary<string, List<string>>();
    private Dictionary<string, string> balloonSpriteTagCollectibleTagMap = new Dictionary<string, string>();
    private static List<string> collectibleBalloonSpriteTagList = new List<string>();
    private static Dictionary<string, GameObject> cBllnSprtTgs2cBllnSprtsMap = new Dictionary<string, GameObject>();
    private Dictionary<string, string> collectibleTagCollectibleName = new Dictionary<string, string>();
    private Dictionary<string, string> planetTagPlanetName = new Dictionary<string, string>();

    [SerializeField] private static int bagTotalCapacity;
    [SerializeField] private static int bagRemainingCapacity;
    [SerializeField] private static Dictionary<string, int> collectibleCollectedMap = new Dictionary<string, int>();
    [SerializeField] private static string currentCollectibleTag;
    [SerializeField] private static string currentPlanetTag;
    [SerializeField] private static bool isSuccess;
    [SerializeField] private static int currentCollectibleinBalloonIndex = -1;

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
        type1PlanetTagName = "SearchObjectiveRed";
        type2PlanetTagName = "SearchObjectiveYellow";
        type3PlanetTagName = "SearchObjectiveBlue";
        type1PlanetAlertTagName = "Type1PlanetDisplay";
        type2PlanetAlertTagName = "Type2PlanetDisplay";
        type3PlanetAlertTagName = "Type3PlanetDisplay";
        type1CollectibleBalloonSpriteTag = "RedEnablerImageUI";
        type2CollectibleBalloonSpriteTag = "YellowEnablerImageUI";
        type3CollectibleBalloonSpriteTag = "BlueEnablerImageUI";
        playerRB = player.GetComponent<Rigidbody>();
        initialPos = player.transform.position;
        countCollectibles = 0;
        defineCollectibleVolumeMap();
        defineCollectibleScoreMap();
        defineCollectibleCollectedMap();
        defineCollectibleUITagMap();
        definePlanetUITagMap();
        definePlanetTagCollectibleTagMap();
        definePlanetAlertTagPlanetAlertGOMap();
        definePlanetTagAlertMessageListMap();
        definePlanetTagDeliveryMessageListMap();
        definePlanetTagSuccessMessageListMap();
        defineBalloonSpriteTagCollectibleTagMap();
        defineCollectibleBalloonSpriteTagList();
        defineCBllnSprtTgs2cBllnSprtsMap();
        defineCollectibleTagCollectibleName();
        definePlanetTagPlanetName();
        bagTotalCapacity = int.Parse(capacityDisplay.GetComponent<Text>().text);
        bagRemainingCapacity = int.Parse(capacityDisplay.GetComponent<Text>().text);
        currentCollectibleTag = null;
        currentPlanetTag = null;
        alertVerticalDistanceThreshold = 200;
        deliveryVerticalDistanceThreshold = 50;
        isSuccess = false;
        currentCollectibleinBalloonIndex = -1;
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

    private void definePlanetUITagMap()
    {
        planetUITagMap[type1PlanetTagName] = type1PlanetAlertTagName;
        planetUITagMap[type2PlanetTagName] = type2PlanetAlertTagName;
        planetUITagMap[type3PlanetTagName] = type3PlanetAlertTagName;
    }

    private void definePlanetTagCollectibleTagMap()
    {
        planetTagCollectibleTagMap[type1PlanetTagName] = "RedEnabler";
        planetTagCollectibleTagMap[type2PlanetTagName] = "YellowEnabler";
        planetTagCollectibleTagMap[type3PlanetTagName] = "BlueEnabler";
    }

    private void definePlanetAlertTagPlanetAlertGOMap() {
        planetAlertTagPlanetAlertGOMap[type1PlanetAlertTagName] = type1PlanetAlertGO;
        planetAlertTagPlanetAlertGOMap[type2PlanetAlertTagName] = type2PlanetAlertGO;
        planetAlertTagPlanetAlertGOMap[type3PlanetAlertTagName] = type3PlanetAlertGO;
    }

    private void definePlanetTagAlertMessageListMap() {
        planetTagAlertMessageListMap[type1PlanetAlertTagName] = new List<string> { "Planet 'Letter' is Approaching!!!!", "Be ready to deliver 'Letter'!!"};
        planetTagAlertMessageListMap[type2PlanetAlertTagName] = new List<string> { "Planet 'Case' is Approaching!!!!", "Be ready to deliver 'Case'!!" };
        planetTagAlertMessageListMap[type3PlanetAlertTagName] = new List<string> { "Planet 'Crate' is Approaching!!!!", "Be ready to deliver 'Crate'!!" };
    }

    private void definePlanetTagDeliveryMessageListMap() {
        planetTagDeliveryMessageListMap[type1PlanetAlertTagName] = new List<string> { "Deliver Now to 'Letter' Planet", "Press Enter to deliver" };
        planetTagDeliveryMessageListMap[type2PlanetAlertTagName] = new List<string> { "Deliver Now to 'Case' Planet", "Press Enter to deliver" };
        planetTagDeliveryMessageListMap[type3PlanetAlertTagName] = new List<string> { "Deliver Now to 'Crate' Planet", "Press Enter to deliver" };
    }

    private void definePlanetTagSuccessMessageListMap() {
        planetTagSuccessMessageListMap[type1PlanetAlertTagName] = new List<string> { "Delivery is a Success!!", "Thank you for the delivery!" };
        planetTagSuccessMessageListMap[type2PlanetAlertTagName] = new List<string> { "Delivery is a Success!!", "Thank you for the delivery!" };
        planetTagSuccessMessageListMap[type3PlanetAlertTagName] = new List<string> { "Delivery is a Success!!", "Thank you for the delivery!" };
    }

    private void defineBalloonSpriteTagCollectibleTagMap() {
        balloonSpriteTagCollectibleTagMap[type1CollectibleBalloonSpriteTag] = type1CollectibleTagName;
        balloonSpriteTagCollectibleTagMap[type2CollectibleBalloonSpriteTag] = type2CollectibleTagName;
        balloonSpriteTagCollectibleTagMap[type3CollectibleBalloonSpriteTag] = type3CollectibleTagName;
    }

    private void defineCollectibleBalloonSpriteTagList() {
        collectibleBalloonSpriteTagList = new List<string> { type1CollectibleBalloonSpriteTag, type2CollectibleBalloonSpriteTag, type3CollectibleBalloonSpriteTag };
    }

    private void defineCBllnSprtTgs2cBllnSprtsMap() {
        cBllnSprtTgs2cBllnSprtsMap[type1CollectibleBalloonSpriteTag] = type1CollectibleBalloonSprite;
        cBllnSprtTgs2cBllnSprtsMap[type2CollectibleBalloonSpriteTag] = type2CollectibleBalloonSprite;
        cBllnSprtTgs2cBllnSprtsMap[type3CollectibleBalloonSpriteTag] = type3CollectibleBalloonSprite;
    }

    private void defineCollectibleTagCollectibleName() {
        collectibleTagCollectibleName[type1CollectibleTagName] = "Letter";
        collectibleTagCollectibleName[type2CollectibleTagName] = "Case";
        collectibleTagCollectibleName[type3CollectibleTagName] = "Crate";
    }

    private void definePlanetTagPlanetName() {
        planetTagPlanetName[type1PlanetTagName] = "Letter";
        planetTagPlanetName[type1PlanetTagName] = "Case";
        planetTagPlanetName[type1PlanetTagName] = "Crate";
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
        Debug.Log("Alert");
        Debug.Log(tagValue);
        Debug.Log(collectibleTagCollectibleName[tagValue]);
        alertDisplay.transform.GetChild(0).gameObject.GetComponent<Text>().text = collectibleTagCollectibleName[tagValue] + " COLLECTED!";
        //Coinalert.GetComponent<Text>().text = tagValue + " COLLECTED!";
        yield return new WaitForSeconds(3f);
        alertDisplay.transform.GetChild(0).gameObject.GetComponent<Text>().text = "";
        alertDisplay.SetActive(false);
    }

    private IEnumerator ShowAlertSuccess(string message)
    {
        alertDisplay.SetActive(true);
        alertDisplay.transform.GetChild(0).gameObject.GetComponent<Text>().text = message;
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

    public void AddScoreDelivery(int score, string tagValue)
    {
        theScore += score;
        Score.GetComponent<Text>().text = theScore.ToString("F0");
        StartCoroutine(ShowAlertSuccess("Delivery Successful!!"));
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
        Debug.Log(collectibleTagName);
        Debug.Log(number);

        int collectiblesCollected = getCollectibleCollectedNumber(collectibleTagName);
        Debug.Log(collectiblesCollected);
        //Check if number of collectibles to be poped out is more than or equal to what we have collected
        if (collectiblesCollected - number >= 0)
        {

            //Update Capacity Display
            //Increase the bag capacity]
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

            displayMessage("Oops!! The Asteroid has removed some items from your bag!!");

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
            AddScoreDelivery(collectibleScoreMap[collectibleTagName]* collectiblesCollected * deliveryScore, collectibleTagName);

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

    public bool isWithinRangeForAlert(GameObject planetObject) {
        bool isWithinRangeForAlertVar = false;
        if (player.transform.position.y - planetObject.transform.position.y < alertVerticalDistanceThreshold && player.transform.position.y - planetObject.transform.position.y >= 0) {
            isWithinRangeForAlertVar = true;
        }
        return isWithinRangeForAlertVar;
    }

    public bool isOutOfRangeForAlert(GameObject planetObject)
    {
        //Debug.Log("Outta Range Called "+ planetObject.tag);
        //Debug.Log(planetObject.tag + "  Player " + player.transform.position.y + " Planet " + planetObject.transform.position.y);
        bool isOutOfRangeForAlertVar = false;
        if (player.transform.position.y - planetObject.transform.position.y < 0)
        {
            //Debug.Log("Outta Range True "+ planetObject.tag);
            isOutOfRangeForAlertVar = true;
        }
        return isOutOfRangeForAlertVar;
    }

    public bool isWithinRangeForDelivery(GameObject planetObject)
    {
        bool isWithinRangeForDeliveryVar = false;
        if (player.transform.position.y - planetObject.transform.position.y < deliveryVerticalDistanceThreshold && player.transform.position.y - planetObject.transform.position.y >= 0)
        {
            isWithinRangeForDeliveryVar = true;
        }
        return isWithinRangeForDeliveryVar;
    }

    public bool deliver(string balloonSpriteTag) {
        bool isDelivered = false;
        if (currentCollectibleTag != null && currentPlanetTag != null) {
         
            bool canDeliveryBasedOnBP = false;
            //check if can deliver based on backpack
            if (currentCollectibleTag == balloonSpriteTagCollectibleTagMap[balloonSpriteTag]) {
                canDeliveryBasedOnBP = true;
            }
            if (canDeliveryBasedOnBP)
            {
                //Update Score
                //Empty Bag
                if (popOutAllCollectiblesForDelivery(currentCollectibleTag, 20))
                {
                    //Add Yuantao's Animation
                    //Instantiate a corresponding item (letter OR case OR crate)
                    GameObject itemPrefab = getItemPrefab(currentCollectibleTag);
                    GameObject item = Instantiate(itemPrefab) as GameObject;

                    //Send the item to the target planet
                    item.transform.position = player.transform.position;
                    GameObject currentPlanet = GameObject.FindGameObjectWithTag(currentPlanetTag);
                    item.GetComponent<BeingDelivered>().targetPlanet = currentPlanet;

                    // Explosion effect
                    ParticleSystem[] particleSystems = player.GetComponentsInChildren<ParticleSystem>();
                    particleSystems[2].Play();

                    // bullet time
                    timeManager.DoSlowMotion();

                    //Show Success Message
                    isSuccess = true;
                    //ScoreManager.startDisplayPlanetDeliverySuccessAlert(currentPlanetTag);
                }
                else {
                    displayMessage("You have not collected enough items in your bag to deliver");
                }
                displayMessage("Delivery completed!");
            }
            else {
                displayMessage("Delivery rejected! Change delivering item by pressing SPACE.");
            }
        }


        return isDelivered;
    }

    public void displayMessage(string message) {
        StartCoroutine(displayAlertFor3Seconds(message));
    }

    private GameObject getItemPrefab(string collectibleTagName)
    {
        
        if (collectibleTagName.Equals(type1CollectibleTagName))
        {
            return type1Enabler;
        }
        else if (collectibleTagName.Equals(type2CollectibleTagName))
        {
            return type2Enabler;
        }
        else if (collectibleTagName.Equals(type3CollectibleTagName))
        {
            return type3Enabler;
        }
        return null;
    }


    private IEnumerator displayAlertFor3Seconds(string message) {
        alertDisplay.SetActive(true);
        alertDisplay.transform.GetChild(0).gameObject.GetComponent<Text>().text = message;
        yield return new WaitForSeconds(3f);
        alertDisplay.transform.GetChild(0).gameObject.GetComponent<Text>().text = "";
        alertDisplay.SetActive(false);
    }

    public static void startDisplayPlanetAlert(string planetTag)
    {
        //Debug.Log("Alert " + planetTag);
        string planetAlertTag = planetUITagMap[planetTag];
        startDisplayPlanetAlertCommon(planetAlertTag, planetTagAlertMessageListMap[planetAlertTag][0], planetTagAlertMessageListMap[planetAlertTag][1], true);
    }

    public static void startDisplayPlanetDeliveryAlert(string planetTag)
    {
        //Debug.Log("Delivery " + planetTag);
        string planetAlertTag = planetUITagMap[planetTag];
        startDisplayPlanetAlertCommon(planetAlertTag, planetTagDeliveryMessageListMap[planetAlertTag][0], planetTagDeliveryMessageListMap[planetAlertTag][1], true);
    }

    public static void startDisplayPlanetDeliverySuccessAlert(string planetTag)
    {
        string planetAlertTag = planetUITagMap[planetTag];
        // Debug.Log("Delivery Success " + planetAlertTag + " "+ planetTagSuccessMessageListMap[planetAlertTag][0] + " " + planetTagSuccessMessageListMap[planetAlertTag][1]);
        startDisplayPlanetAlertCommon(planetAlertTag, planetTagSuccessMessageListMap[planetAlertTag][0], planetTagSuccessMessageListMap[planetAlertTag][1], true);
        //startDisplayPlanetAlertCommon(planetAlertTag, planetTagAlertMessageListMap[planetAlertTag][0], planetTagAlertMessageListMap[planetAlertTag][1], true);
    }

    public static void stopDisplayPlanetAlert(string planetTag)
    {
        string planetAlertTag = planetUITagMap[planetTag];
        startDisplayPlanetAlertCommon(planetAlertTag, "", "", false);
    }

    private static void startDisplayPlanetAlertCommon(string planetAlertTag, string message1, string message2, bool isActive) { 
            GameObject planetAlertDisplay = planetAlertTagPlanetAlertGOMap[planetAlertTag];
            planetAlertDisplay.SetActive(isActive);
            planetAlertDisplay.transform.GetChild(0).gameObject.GetComponent<Text>().text = message1;
            planetAlertDisplay.transform.GetChild(2).gameObject.GetComponent<Text>().text = message2;
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

    public Dictionary<string, string> getPlanetTagCollectibleTagMap() {
        return planetTagCollectibleTagMap;
    }

    public static int getCollectibleCollectedNumber(string collectibleTag) {
        return collectibleCollectedMap[collectibleTag];
    }

    public static void setCollectibleCollectedNumber(string collectibleTag, int value)
    {
        collectibleCollectedMap[collectibleTag] = value;
    }

    public static string getCurrentCollectibleTag() {
        return currentCollectibleTag;
    }

    public static void setCurrentCollectibleTag(string currentCollectibleTagVar)
    {
        currentCollectibleTag = currentCollectibleTagVar;
    }

    public static string getCurrentPlanetTag() {
        return currentPlanetTag;
    }

    public static void setCurrentPlanetTag(string currentPlanetTagVar) {
        currentPlanetTag = currentPlanetTagVar;
    }

    public static bool getIsSuccess() {
        return isSuccess;
    }

    public static void setIsSuccess(bool isSuccessVar) {
        isSuccess = isSuccessVar;
    }

    public static int getCurrentCollectibleinBalloonIndex() {
        return currentCollectibleinBalloonIndex;
    }

    public static void setCurrentCollectibleinBalloonIndex(int currentCollectibleinBalloonIndexVar) {
        currentCollectibleinBalloonIndex = currentCollectibleinBalloonIndexVar;
    }

    public static List<string> getCollectibleBalloonSpriteTagList() {
        return collectibleBalloonSpriteTagList;
    }

    public static Dictionary<string, GameObject> getCBllnSprtTgs2cBllnSprtsMap() {
        return cBllnSprtTgs2cBllnSprtsMap;
    }

    public Dictionary<string, string> getCollectibleTagCollectibleName() {
        return collectibleTagCollectibleName;
    }

    public Dictionary<string, string> getPlanetTagPlanetName() {
        return planetTagPlanetName;
    }
}