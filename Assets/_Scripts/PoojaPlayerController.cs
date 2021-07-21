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

    
    public float glideTolerance = .5f; // amount of time during which player can press glide

    private Rigidbody characterBody;
    public Material[] material;
    public int x;
    public Renderer rend;
    Plane[] planes;
    public Collider anchor;
    public Transform boundsCenter;
    private ScoreManager scoreManager;


    void Start()
    {
        characterBody = gameObject.GetComponent<Rigidbody>();
        // Material changes based on enabler collections
        x = 0;
        Transform t = this.transform;
        Transform tr = GameObject.FindGameObjectWithTag("Balloon").transform;

        //rend = tr.GetComponent<Renderer>();
        //rend = tr.transform.GetChild(0).gameObject.GetComponent<Renderer>();
        rend = tr.GetChild(0).gameObject.GetComponent<Renderer>();
        rend.enabled = false;
        rend.sharedMaterial = material[x];
        planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);
        scoreManager = ScoreManager.Instance;
        
    }

    void Update()
    {
        if (!PlayerOnScreen())
        {
            Debug.Log("disappears!");
            characterBody.AddForce(new Vector3(boundsCenter.position.x - transform.position.x,
                0,
                boundsCenter.position.z - transform.position.z)*Time.deltaTime, ForceMode.Impulse);

        }
        if (_AccSpeed != 0.0 && _moveSpeed <= _MaxVelocity && _moveSpeed >= _MinVelocity)
            _moveSpeed = _moveSpeed + _AccSpeed * Time.deltaTime;
        else
        {
            _AccSpeed = 0.0f;
            if (_moveSpeed != _NormVelocity && Time.time - startTime > 20)
                _moveSpeed = _NormVelocity;
        }

        //////////////Use ray casting to find object which are glidable//////////////////////
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
        //////////////////////////////////////////////////////////////////////////////////////
      
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            characterBody.AddForce(new Vector3(_moveSpeed * Time.deltaTime, 0, 0));

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            characterBody.AddForce(new Vector3(-_moveSpeed * Time.deltaTime, 0, 0));

        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
            characterBody.AddForce(new Vector3(0, 0 ,_moveSpeed * Time.deltaTime));

        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
            characterBody.AddForce(new Vector3(0, 0 ,-_moveSpeed * Time.deltaTime));


        if (Input.GetKeyDown(KeyCode.Return)) {
            //Debug.Log("Enter Pressed");
            if (ScoreManager.getCurrentPlanetTag() != null && ScoreManager.getCurrentCollectibleTag() != null) {
                scoreManager.deliver(ScoreManager.getCollectibleBalloonSpriteTagList()[ScoreManager.getCurrentCollectibleinBalloonIndex()]);
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            int currentCollectibleinBalloonIndex = ScoreManager.getCurrentCollectibleinBalloonIndex();
            if (currentCollectibleinBalloonIndex != -1)
            {
                ScoreManager.getCBllnSprtTgs2cBllnSprtsMap()[ScoreManager.getCollectibleBalloonSpriteTagList()[currentCollectibleinBalloonIndex]].SetActive(false);
            }
            currentCollectibleinBalloonIndex++;
            if (currentCollectibleinBalloonIndex >= ScoreManager.getCollectibleBalloonSpriteTagList().Count)
            {
                currentCollectibleinBalloonIndex = 0;
            }

            ScoreManager.setCurrentCollectibleinBalloonIndex(currentCollectibleinBalloonIndex);
            ScoreManager.getCBllnSprtTgs2cBllnSprtsMap()[ScoreManager.getCollectibleBalloonSpriteTagList()[currentCollectibleinBalloonIndex]].SetActive(true);
            //rend.enabled = true;
            //rend.sharedMaterial = material[1];

            //currentEnablerIndex++;
            //if (currentEnablerIndex >= collectedEnablers.Count)
            //{
            //    currentEnablerIndex = 0;
            //}
            //int count = -1;
            //foreach (Image value in collectedEnablers)
            //{
            //    count++;
            //    if (count == currentEnablerIndex)
            //    {
            //        currentEnabler = value.tag;
            //        break;
            //    }
            //}
            //switch (currentEnabler)
            //{
            //    case "RedEnablerImageUI":
            //        rend.enabled = true;
            //        rend.sharedMaterial = material[1];
            //        break;
            //    case "YellowEnablerImageUI":
            //        rend.enabled = true;
            //        rend.sharedMaterial = material[2];
            //        break;
            //    case "BlueEnablerImageUI":
            //        rend.enabled = true;
            //        rend.sharedMaterial = material[3];
            //        break;
            //    case "GreenEnablerImageUI":
            //        rend.enabled = true;
            //        rend.sharedMaterial = material[4];
            //        break;
            //    case "SkyEnablerImageUI":
            //        rend.enabled = true;
            //        rend.sharedMaterial = material[5];
            //        break;
            //    default:
            //        break;

            //}

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
            ParticleSystem dust = gameObject.GetComponentsInChildren<ParticleSystem>()[0];
            // INVALID when there is more than one particle system in children of "Player"
            dust.Play();
        }
    }

    private bool PlayerOnScreen()
    {

        /*Vector3 screenPoint = Camera.main.WorldToViewportPoint(transform.position);
        bool onScreen = screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;
        if (!onScreen)
        {
            Debug.Log("disappears!");
            return false;
        }
        return true;*/
        planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);
        return GeometryUtility.TestPlanesAABB(planes, anchor.bounds);

    }
}
