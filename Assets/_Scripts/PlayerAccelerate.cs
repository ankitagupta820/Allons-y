using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAccelerate : MonoBehaviour
{

     public GameObject character;
     private Rigidbody characterBody;
     
     public float _Velocity = 0.0f;      // Current Travelling Velocity
     public float _MaxVelocity = 50.0f;   // Maxima Velocity
     public float _Acc = 0.0f;           // Current Acceleration
     public float _AccSpeed = 1.0f;      // Amount to increase Acceleration with.
     public float _MaxAcc = 2.0f;        // Max Acceleration
     public float _MinAcc = -1.0f;       // Min Acceleration


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()


    {
          
        if (Input.GetKey(KeyCode.UpArrow))
             _Acc -= _AccSpeed;
 
         if (Input.GetKey(KeyCode.DownArrow))
             _Acc += _AccSpeed;
 
         if (_Acc > _MaxAcc)
             _Acc = _MaxAcc;
         else if (_Acc < _MinAcc)
             _Acc = _MinAcc;
 
         _Velocity += _Acc;
 
         if (_Velocity > _MaxVelocity)
             _Velocity = _MaxVelocity;
         else if (_Velocity < 0)
             _Velocity = 0;

        
         transform.Translate(Vector3.down * _Velocity * Time.deltaTime);

    }
}
