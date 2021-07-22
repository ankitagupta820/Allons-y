using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    private static TutorialManager _instance;
    public static TutorialManager Instance { get { return _instance; } }
    public string[] instructions;
    public GameObject blankG;
    public GameObject enablersOnlyG;
    public GameObject easyLevelG;
    public GameObject instructionDisplayBox;
    public GameObject coinObject;
    public GameObject learnAsYouPlayCanvas;
    //public GameObject objective;

    private float nextActionTime = 0.0f;
    private float period = 0.1f;
    private bool isGamePaused;
    private bool isWaiting;
    private List<bool> instructionsShownList;
    private List<string> instructionsList;
    private List<float> instructionsShowTimeList;
    //private bool isNextButtonInstance;

    [SerializeField] static private int instructionIndex;
    [SerializeField] static private bool enablerCollected;

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

    // Start is called before the first frame update
    void Start()
    {
        blankG.SetActive(true);
        enablersOnlyG.SetActive(false);
        easyLevelG.SetActive(false);
        //objective.SetActive(false);
        instructionIndex = 0;
        instructionsShownList = new List<bool> { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false };
        instructionsShowTimeList = new List<float> { 5f, 0f, 15f, 10f, 5f, 5f, 15f, 5f, 8f, 5f, 12f, 5f, 16f, 5f, 21f, 5f, 5f };
        instructionsList = new List<string> { "Welcome to the Outer Space!!!", "", "Use Arrow or WASD to move. Dodge Red Eyed Monsters!!!", "", "", "You are a Space Postman!! COLLECT the GREEN letters, BLUE cases and RED crates in your bag as you run through them in Space", "", "Press 'Space' Key to bring items to the top of the bag on your back.", "", "Your bag can hold limited items!! Capacity of bag is shown on the inventory box at the top", "", "When a planet is approaching, alert will appear on top right corner. Press 'Space' key to keep the item of the Planet on the top of your bag!!", "", "On 'Deliver Now!' alert, press 'Enter' key to deliver the items", "", "Your Objective is to make as many Deliveries as possible by the end of your Space Run!!! Enjoy!!!", "" };
        enablerCollected = false;
        isWaiting = true;
        //isNextButtonInstance = false;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(instructionIndex);
        switch (instructionIndex) {
            case 0:
                Debug.Log("Case 0");
                if (!instructionsShownList[instructionIndex]) {
                    instructionsShownList[instructionIndex] = true;
                    pauseTheGame();
                    learnAsYouPlayCanvas.SetActive(true);
                    showInstNoWait(instructionsList[instructionIndex]);
                }
                break;
            case 1:
                Debug.Log("Case 1");
                Debug.Log(isWaiting);
                
                if (!instructionsShownList[instructionIndex])
                {
                    instructionsShownList[instructionIndex] = true;
                    isWaiting = true;
                    waitOnInstructions(instructionsShowTimeList[instructionIndex]);
                }
                Debug.Log(isWaiting);
                if (!isWaiting)
                {
                    Debug.Log("Waiting done");
                    isWaiting = true;
                    nextInstructionsWithOutResume();
                }
                break;
            case 2:
                Debug.Log("Case 2");
                if (!instructionsShownList[instructionIndex])
                {
                    instructionsShownList[instructionIndex] = true;
                    pauseTheGame();
                    learnAsYouPlayCanvas.SetActive(true);
                    showInstNoWait(instructionsList[instructionIndex]);
                    //waitOnInstructions(instructionsShowTimeList[instructionIndex]);
                }
                break;
            case 3:
                Debug.Log("Case 3");
                if (!instructionsShownList[instructionIndex])
                {
                    instructionsShownList[instructionIndex] = true;
                    blankG.SetActive(false);
                    enablersOnlyG.SetActive(true);
                    isWaiting = true;
                    waitOnInstructions(instructionsShowTimeList[instructionIndex]);
                }
                if (!isWaiting)
                {
                    isWaiting = true;
                    nextInstructionsWithOutResume();
                }
                break;
            case 4:
                Debug.Log("Case 4");
                Debug.Log(isWaiting);

                if (!instructionsShownList[instructionIndex])
                {
                    instructionsShownList[instructionIndex] = true;
                    isWaiting = true;
                    waitOnInstructions(instructionsShowTimeList[instructionIndex]);
                }
                Debug.Log(isWaiting);
                if (!isWaiting)
                {
                    Debug.Log("Waiting done");
                    isWaiting = true;
                    nextInstructionsWithOutResume();
                }
                break;
            case 5:
                Debug.Log("Case 5");
                if (!instructionsShownList[instructionIndex])
                {
                    instructionsShownList[instructionIndex] = true;
                    pauseTheGame();
                    learnAsYouPlayCanvas.SetActive(true);
                    showInstNoWait(instructionsList[instructionIndex]);
                    //waitOnInstructions(instructionsShowTimeList[instructionIndex]);
                }
                break;
            case 6:
                Debug.Log("Case 6");
                Debug.Log(isWaiting);

                if (!instructionsShownList[instructionIndex])
                {
                    instructionsShownList[instructionIndex] = true;
                    isWaiting = true;
                    waitOnInstructions(instructionsShowTimeList[instructionIndex]);
                }
                Debug.Log(isWaiting);
                if (!isWaiting)
                {
                    Debug.Log("Waiting done");
                    isWaiting = true;
                    nextInstructionsWithOutResume();
                }
                break;
            case 7:
                Debug.Log("Case 7");
                if (!instructionsShownList[instructionIndex])
                {
                    instructionsShownList[instructionIndex] = true;
                    pauseTheGame();
                    learnAsYouPlayCanvas.SetActive(true);
                    showInstNoWait(instructionsList[instructionIndex]);
                    //waitOnInstructions(instructionsShowTimeList[instructionIndex]);
                }
                break;
            case 8:
                Debug.Log("Case 8");
                Debug.Log(isWaiting);

                if (!instructionsShownList[instructionIndex])
                {
                    instructionsShownList[instructionIndex] = true;
                    isWaiting = true;
                    //blankG.SetActive(false);
                    //enablersOnlyG.SetActive(false);
                    //easyLevelG.SetActive(true);
                    waitOnInstructions(instructionsShowTimeList[instructionIndex]);
                }
                Debug.Log(isWaiting);
                if (!isWaiting)
                {
                    Debug.Log("Waiting done");
                    isWaiting = true;
                    nextInstructionsWithOutResume();
                }
                break;
            case 9:
                Debug.Log("Case 9");
                if (!instructionsShownList[instructionIndex])
                {
                    instructionsShownList[instructionIndex] = true;
                    pauseTheGame();
                    learnAsYouPlayCanvas.SetActive(true);
                    showInstNoWait(instructionsList[instructionIndex]);
                    //waitOnInstructions(instructionsShowTimeList[instructionIndex]);
                }
                break;
            case 10:
                Debug.Log("Case 10");
                Debug.Log(isWaiting);

                if (!instructionsShownList[instructionIndex])
                {
                    instructionsShownList[instructionIndex] = true;
                    isWaiting = true;
                    waitOnInstructions(instructionsShowTimeList[instructionIndex]);
                }
                Debug.Log(isWaiting);
                if (!isWaiting)
                {
                    Debug.Log("Waiting done");
                    isWaiting = true;
                    nextInstructionsWithOutResume();
                }
                break;
            case 11:
                Debug.Log("Case 11");
                if (!instructionsShownList[instructionIndex])
                {
                    instructionsShownList[instructionIndex] = true;
                    pauseTheGame();
                    learnAsYouPlayCanvas.SetActive(true);
                    showInstNoWait(instructionsList[instructionIndex]);
                    //waitOnInstructions(instructionsShowTimeList[instructionIndex]);
                }
                break;
            case 12:
                Debug.Log("Case 12");
                Debug.Log(isWaiting);

                if (!instructionsShownList[instructionIndex])
                {
                    instructionsShownList[instructionIndex] = true;
                    isWaiting = true;
                    waitOnInstructions(instructionsShowTimeList[instructionIndex]);
                }
                Debug.Log(isWaiting);
                if (!isWaiting)
                {
                    Debug.Log("Waiting done");
                    isWaiting = true;
                    nextInstructionsWithOutResume();
                }
                break;
            case 13:
                Debug.Log("Case 13");
                if (!instructionsShownList[instructionIndex])
                {
                    instructionsShownList[instructionIndex] = true;
                    pauseTheGame();
                    learnAsYouPlayCanvas.SetActive(true);
                    showInstNoWait(instructionsList[instructionIndex]);
                    //waitOnInstructions(instructionsShowTimeList[instructionIndex]);
                }
                break;
            case 14:
                Debug.Log("Case 14");
                Debug.Log(isWaiting);

                if (!instructionsShownList[instructionIndex])
                {
                    instructionsShownList[instructionIndex] = true;
                    isWaiting = true;
                    waitOnInstructions(instructionsShowTimeList[instructionIndex]);
                }
                Debug.Log(isWaiting);
                if (!isWaiting)
                {
                    Debug.Log("Waiting done");
                    isWaiting = true;
                    nextInstructionsWithOutResume();
                }
                break;
            case 15:
                Debug.Log("Case 15");
                if (!instructionsShownList[instructionIndex])
                {
                    instructionsShownList[instructionIndex] = true;
                    pauseTheGame();
                    learnAsYouPlayCanvas.SetActive(true);
                    showInstNoWait(instructionsList[instructionIndex]);
                    //waitOnInstructions(instructionsShowTimeList[instructionIndex]);
                }
                break;
            case 16:
                Debug.Log("Case 16");
                Debug.Log(isWaiting);

                if (!instructionsShownList[instructionIndex])
                {
                    instructionsShownList[instructionIndex] = true;
                    isWaiting = true;
                    waitOnInstructions(instructionsShowTimeList[instructionIndex]);
                }
                Debug.Log(isWaiting);
                if (!isWaiting)
                {
                    isWaiting = true;
                    enablersOnlyG.SetActive(false);
                    blankG.SetActive(false);
                    easyLevelG.SetActive(true);
                    gameObject.SetActive(false);
                }
                break;
            default:
                Debug.Log("Case 16");
                Debug.Log(isWaiting);

                if (!instructionsShownList[instructionIndex])
                {
                    instructionsShownList[instructionIndex] = true;
                    isWaiting = true;
                    waitOnInstructions(instructionsShowTimeList[instructionIndex]);
                }
                Debug.Log(isWaiting);
                if (!isWaiting)
                {
                    isWaiting = true;
                    enablersOnlyG.SetActive(false);
                    blankG.SetActive(false);
                    easyLevelG.SetActive(true);
                    gameObject.SetActive(false);
                }
                break;

        }


        //if (instructionIndex == 0)
        //{
        //    //Game Just Started
        //    if (!inst1shown) {
        //        inst1shown = true;
        //        ShowInst("Use Arrow or WASD to move. Dodge Red Eyed Monsters!!!", 5f);
        //    }

        //}
        //if (instructionIndex == 1) {
        //    if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
        //    {
        //        blankG.SetActive(false);
        //        enablersOnlyG.SetActive(true);
        //        instructionIndex++;
        //    }
        //}
        //if (instructionIndex == 2) {
        //    if (!inst2shown) {
        //        inst2shown = true;
        //        ShowInst("Colored Cubes are Enablers which allow you to change your color.", 10f);
        //    }
        //}
        //if (instructionIndex == 3) {

        //    //Keep checking to see if enabler is collected
        //    coin coinScript = coinObject.GetComponent<coin>();

        //    //Debug.Log("Checking");
        //    //if (Time.time > nextActionTime)
        //    //{
        //    //    Debug.Log("Checking");
        //    //    nextActionTime += period;
        //    // execute block of code here
        //    if (enablerCollected) {
        //            instructionIndex++;
        //        }
        //    //}

        //}
        //if (instructionIndex == 4) {
        //    if (!inst3shown)
        //    {
        //        inst3shown = true;
        //        ShowInst("Colors stacked are shown on the right corner of screen. Press Space to change color.", 12f);

        //        enablersOnlyG.SetActive(false);
        //        easyLevelG.SetActive(true);
        //    }

        //}

        //if (instructionIndex == 5) {
        //    if (!inst4shown)
        //    {
        //        inst4shown = true;
        //        ShowInst("Coloured Cylinders are Objects lost in Space. To collect them you have to be of the same color.", 7f);

        //    }
        //}

        //if (instructionIndex == 6)
        //{
        //    //objective.SetActive(true);
        //    learnAsYouPlayCanvas.SetActive(false);
        //    easyLevelG.SetActive(true);
        //    instructionIndex++;
        //    gameObject.SetActive(false);
        //    gameObject.SetActive(false);
        //    easyLevelG.SetActive(true);
        //}
        

    }

    public void setEnablerCollected(bool val) {
        enablerCollected = val;
    }

    public void coinCheck(float time,coin coinScript)
    {
        StartCoroutine(checkCoin(time, coinScript));
    }

    private IEnumerator checkCoin(float time, coin coinScript) {
        yield return new WaitForSeconds(time);
        coinScript.getEnablerCollecter();
    }

    public void showInstNoWait(string instruction)
    {
        instructionDisplayBox.GetComponent<Text>().text = instruction;
    }

    public void ShowInst(string instruction, float time)
    {
        StartCoroutine(ShowInstructions(instruction, time));
        
    }

    private IEnumerator ShowInstructions(string instruction, float time)
    {
        instructionDisplayBox.GetComponent<Text>().text = instruction;
        float pauseEndTime = Time.realtimeSinceStartup + time;
        while(Time.realtimeSinceStartup < pauseEndTime)
        {
            yield return 0;
        }
        //wont work when game is paused
        //yield return new WaitForSeconds(time);
        nextInstructions();
    }

    public void waitOnInstructions(float time) {
        StartCoroutine(waitOnInstructionsInner(time));
    }

    private IEnumerator waitOnInstructionsInner(float time) {
        isWaiting = true;
        float pauseEndTime = Time.realtimeSinceStartup + time;
        while (Time.realtimeSinceStartup < pauseEndTime)
        {
            yield return 0;
        }
        isWaiting = false;
    }

    private void showSingleInst(string inst) {
        instructionDisplayBox.GetComponent<Text>().text = inst;
    }

    private void clearSingleInstParent(float time) {
        StartCoroutine(clearSingleInst(time));
    }

    private IEnumerator clearSingleInst(float time) {
        yield return new WaitForSeconds(time);
        instructionDisplayBox.GetComponent<Text>().text = "";
       
    }

    private void pauseTheGame() {
        AudioListener.pause = true;
        Time.timeScale = 0f;
        isGamePaused = true;
    }

    private void resumeTheGame()
    {
        Time.timeScale = 1f;
        isGamePaused = false;
    }

    public void skipInstructions() {
        learnAsYouPlayCanvas.SetActive(false);
        resumeTheGame();
        instructionIndex = 16;
        
    }

    public void nextInstructions()
    {
        learnAsYouPlayCanvas.SetActive(false);
        resumeTheGame();
        instructionIndex++;
    }


    public void nextInstructionsWithOutResume()
    {
        //learnAsYouPlayCanvas.SetActive(false);
        instructionIndex++;
    }
}
