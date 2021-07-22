using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testGUI : MonoBehaviour {
    public GameObject[] cameraObjects;
    //public Camera[] sceneCameras;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
    }

    private void OnGUI()
    {
        float offsetY = 10;
        
        for (int i = 0; i < cameraObjects.Length; i++)
        {
            if (GUI.Button(new Rect(10,offsetY+ i*50,120,35), cameraObjects[i].name))
            {
                cameraObjects[i].SetActive(true);

                for (int x=0;x< cameraObjects.Length;x++)
                {
                    if (x != i)
                        cameraObjects[x].SetActive(false);
                }
            }
        }

    }
}
