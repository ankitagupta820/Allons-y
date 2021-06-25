using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoojaPlayerController : MonoBehaviour
{

    [SerializeField] private float _moveSpeed = 10f; //current speed

    [SerializeField] public float _MaxVelocity = 18.0f;   // Maxima Velocity
    [SerializeField] public float _MinVelocity = 5.0f;   // Maxima Velocity
    [SerializeField] public float _NormVelocity = 10.0f;   // Normal Velocity
    [SerializeField] public float _AccSpeed = 0.0f;      // Speed increase delta
  
    [SerializeField] private float startTime; //To Keep track of time for which acceleration keeps

    public float glideTolerance = .5f; // amount of time during which player can press glide

    private Rigidbody characterBody;
    public Material[] material;
    public int x;
    public Renderer rend;

    public string currentEnabler = "Pooja";
    public enum Enablers { Red,  Yellow};

    public void setCurrentEnabler(string value) {
        this.currentEnabler = value;
    }

    public string getCurrentEnabler()
    {
        return this.currentEnabler;
    }

    void Start()
    {
        characterBody = gameObject.GetComponent<Rigidbody>();
        // Material changes based on accerlation/deceleration
        x = 0;
        rend = GetComponent<Renderer>();
        rend.enabled = true;
        rend.sharedMaterial = material[x];
    }

    void Update()
    {
      
        if (_AccSpeed != 0.0 && _moveSpeed <= _MaxVelocity && _moveSpeed >= _MinVelocity)
            _moveSpeed = _moveSpeed + _AccSpeed * Time.deltaTime;
        else
        {
            _AccSpeed = 0.0f;
            if (_moveSpeed != _NormVelocity && Time.time - startTime > 20)
                _moveSpeed = _NormVelocity;
        }


        //transform.position += Time.deltaTime * _moveSpeed * Vector3.down;
        //transform.position += Time.deltaTime * _moveSpeed * Vector3.forward;
        //characterBody.AddForce(new Vector3(0, -_moveSpeed * Time.deltaTime , 0)); // Using Gravity

        // This would cast rays only against colliders in layer 8 .
        var layerMask8 = 1 << 8;

        RaycastHit hit;

        // cast a ray to the right of the player object
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.right), out hit, 30, layerMask8))
        {

            // orient the Moving Object's Left direction to Match the Normals on his Right
            var RunnerRotation = Quaternion.FromToRotation(Vector3.left, hit.normal);

            //Smooth rotation
            transform.rotation = Quaternion.Slerp(transform.rotation, RunnerRotation, Time.deltaTime * 10);
        }


        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D) && transform.position.x < 13)
            /*transform.position += Time.deltaTime * _moveSpeed * Vector3.right;*/
            /*characterBody.MovePosition(transform.position + Time.deltaTime * _moveSpeed * Vector3.right);*/
            characterBody.AddForce(new Vector3(_moveSpeed * Time.deltaTime, 0, 0));

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A) && transform.position.x > -3)
            /*transform.position += Time.deltaTime * _moveSpeed * Vector3.left;*/
            /*characterBody.MovePosition(transform.position + Time.deltaTime * _moveSpeed * Vector3.left);*/
            characterBody.AddForce(new Vector3(-_moveSpeed * Time.deltaTime, 0, 0));

        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W) && transform.position.z < 2)
            /*transform.position += Time.deltaTime * _moveSpeed * Vector3.forward;*/
            /*characterBody.MovePosition(transform.position + Time.deltaTime * _moveSpeed * Vector3.forward);*/
            characterBody.AddForce(new Vector3(0, 0 ,_moveSpeed * Time.deltaTime));

        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S) && transform.position.z > -10)
            /*transform.position += Time.deltaTime * _moveSpeed * Vector3.back;*/
            /*characterBody.MovePosition(transform.position + Time.deltaTime * _moveSpeed * Vector3.back);*/
            characterBody.AddForce(new Vector3(0, 0 ,-_moveSpeed * Time.deltaTime));

        //if (_moveSpeed > _NormVelocity)
        //    x = 2;
        //else if (_moveSpeed < _NormVelocity)
        //    x = 1;
        //else
        //    x = 0;

        //rend.sharedMaterial = material[x];


        //if (Input.GetKey(KeyCode.Space))
        //    transform.position = new Vector3(0, 0.5f, 0);

        switch (currentEnabler)
        {
            case "Red":
                rend.sharedMaterial = material[1];
                break;
            case "Yellow":
                rend.sharedMaterial = material[2];
                break;
            default:
                break;

        }

    }

    public void setSpeedForAccelerate() {
        startTime = Time.time;
        _AccSpeed = 2.0f;
    }

    public void setSpeedForDeAccelerate()
    {
        startTime = Time.time;
        _AccSpeed = -2.0f;
    }

    private void OnCollisionEnter(Collision collision)
    {
        
    }

}
