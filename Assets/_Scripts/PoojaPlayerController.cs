using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PoojaPlayerController : MonoBehaviour
{

    [SerializeField] private float _moveSpeed = 10f; //current speed

    [SerializeField] public float _MaxVelocity = 18.0f;   // Maxima Velocity
    [SerializeField] public float _MinVelocity = 5.0f;   // Maxima Velocity
    [SerializeField] public float _NormVelocity = 10.0f;   // Normal Velocity
    [SerializeField] public float _AccSpeed = 0.0f;      // Speed increase delta
  
    [SerializeField] private float startTime; //To Keep track of time for which acceleration keeps

    [SerializeField] public Queue<Image> collectedEnablers = new Queue<Image>();
    [SerializeField] public int currentEnablerIndex = -1;

    public float glideTolerance = .5f; // amount of time during which player can press glide

    private Rigidbody characterBody;
    public Material[] material;
    public int x;
    public Renderer rend;

    public string currentEnabler = "Pooja";
    public enum Enablers { Red,  Yellow, Blue, Green, Sky};

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
        // Material changes based on enabler collections
        x = 0;
        Transform t = this.transform;
        foreach (Transform tr in t)
        {
            if (tr.tag == "Balloon")
            {

                //rend = tr.GetComponent<Renderer>();
                rend = tr.transform.GetChild(0).gameObject.GetComponent<Renderer>();
                rend.enabled = true;
                rend.sharedMaterial = material[x];
            }
        }
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
            characterBody.AddForce(new Vector3(_moveSpeed * Time.deltaTime, 0, 0));

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A) && transform.position.x > -3)
            characterBody.AddForce(new Vector3(-_moveSpeed * Time.deltaTime, 0, 0));

        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W) && transform.position.z < 2)
            characterBody.AddForce(new Vector3(0, 0 ,_moveSpeed * Time.deltaTime));

        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S) && transform.position.z > -10)
            characterBody.AddForce(new Vector3(0, 0 ,-_moveSpeed * Time.deltaTime));

        if (Input.GetKey(KeyCode.Space))
        {

            currentEnablerIndex++;
            if (currentEnablerIndex > collectedEnablers.Count)
            {
                currentEnablerIndex = 0;
            }
            int count = -1;
            foreach (Image value in collectedEnablers)
            {
                count++;
                if (count == currentEnablerIndex)
                {
                    currentEnabler = value.tag;
                    break;
                }
            }
            switch (currentEnabler)
            {
                case "RedEnablerImageUI":
                    rend.sharedMaterial = material[1];
                    break;
                case "YellowEnablerImageUI":
                    rend.sharedMaterial = material[2];
                    break;
                case "BlueEnablerImageUI":
                    rend.sharedMaterial = material[3];
                    break;
                case "GreenEnablerImageUI":
                    rend.sharedMaterial = material[4];
                    break;
                case "SkyEnablerImageUI":
                    rend.sharedMaterial = material[5];
                    break;
                default:
                    break;

            }

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
        if (collision.gameObject.CompareTag("Target") || collision.gameObject.CompareTag("Boundary"))
        {
            ParticleSystem dust = gameObject.GetComponentInChildren<ParticleSystem>();
            // INVALID when there is more than one particle system in children of "Player"
            dust.Play();
        }
    }

}
