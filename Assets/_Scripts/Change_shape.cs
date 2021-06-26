using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Change_shape : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
            if(other.gameObject.name == "Coin3"){
                this.transform.GetChild(2).gameObject.active=false;
                this.transform.GetChild(4).gameObject.active=true;
                Destroy(other.gameObject);
            }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
