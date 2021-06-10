using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGraphic : MonoBehaviour
{
    public Material[] material;
    public int x;
    Renderer rend;

    // Start is called before the first frame update
    void Start()
    {
        x=0;
        rend=GetComponent<Renderer>();
        rend.enabled=true;
        rend.sharedMaterial = material[x];
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
            x=2;
        else if (Input.GetKey(KeyCode.DownArrow))
            x=1;
        else
            x=0;

        rend.sharedMaterial = material[x];
        
    }
}
