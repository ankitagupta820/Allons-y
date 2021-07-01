using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    public string[] instructions;
    public int instructionIndex;
    public GameObject blankG;
    public GameObject enablersOnlyG;
    public GameObject easyLevelG;
    public GameObject coinAlertForTut;
    public GameObject coinObject;
    public GameObject objective;

    private float nextActionTime = 0.0f;
    public float period = 0.1f;
    private bool inst1shown;
    private bool inst2shown;
    private bool inst3shown;
    private bool inst4shown;
    private bool enablerCollected;

    // Start is called before the first frame update
    void Start()
    {
        blankG.SetActive(true);
        enablersOnlyG.SetActive(false);
        easyLevelG.SetActive(false);
        objective.SetActive(false);
        instructionIndex = 0;
        inst1shown = false;
        inst2shown = false;
        inst3shown = false;
        inst4shown = false;
        enablerCollected = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (instructionIndex == 0)
        {
            //Game Just Started
            if (!inst1shown) {
                inst1shown = true;
                ShowInst("Use Arrow or WASD to move. Dodge Red Eyed Monsters!!!", 5f);
            }
            
        }
        if (instructionIndex == 1) {
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
            {
                blankG.SetActive(false);
                enablersOnlyG.SetActive(true);
                instructionIndex++;
            }
        }
        if (instructionIndex == 2) {
            if (!inst2shown) {
                inst2shown = true;
                ShowInst("Colored Cubes are Enablers which allow you to change your color.", 10f);
            }
        }
        if (instructionIndex == 3) {

            //Keep checking to see if enabler is collected
            coin coinScript = coinObject.GetComponent<coin>();
            Debug.Log("Checking");
            //if (Time.time > nextActionTime)
            //{
            //    Debug.Log("Checking");
            //    nextActionTime += period;
            // execute block of code here
            if (enablerCollected) {
                    instructionIndex++;
                }
            //}

        }
        if (instructionIndex == 4) {
            if (!inst3shown)
            {
                inst3shown = true;
                ShowInst("Colors stacked are shown on the left corner of screen. Press Space to change color.", 12f);
                
                enablersOnlyG.SetActive(false);
                easyLevelG.SetActive(true);
            }

        }

        if (instructionIndex == 5) {
            if (!inst4shown)
            {
                inst4shown = true;
                ShowInst("Coloured Cylinders are Objects lost in Space. To collect them you have to be of the same color.", 7f);
               
            }
        }

        if (instructionIndex == 6) {
            objective.SetActive(true);
            gameObject.SetActive(false);
        }

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

    public void ShowInst(string instruction, float time)
    {
        StartCoroutine(ShowInstructions(instruction, time));
        
    }

    private IEnumerator ShowInstructions(string instruction, float time)
    {
        coinAlertForTut.GetComponent<Text>().text = instruction;
        yield return new WaitForSeconds(time);
        coinAlertForTut.GetComponent<Text>().text = "";
        instructionIndex++;
    }

    private void showSingleInst(string inst) {
        coinAlertForTut.GetComponent<Text>().text = inst;
    }

    private void clearSingleInstParent(float time) {
        StartCoroutine(clearSingleInst(time));
    }

    private IEnumerator clearSingleInst(float time) {
        yield return new WaitForSeconds(time);
        coinAlertForTut.GetComponent<Text>().text = "";
       
    }
}
