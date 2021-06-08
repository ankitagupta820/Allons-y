using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoojaPlayerController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 15f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Time.deltaTime * _moveSpeed * Vector3.down;
        transform.position += Time.deltaTime * _moveSpeed * Vector3.forward;


        if (Input.GetKey(KeyCode.RightArrow) && transform.position.x < 10)
            transform.position += Time.deltaTime * _moveSpeed * Vector3.right;

        if (Input.GetKey(KeyCode.LeftArrow) && transform.position.x > -10)
            transform.position += Time.deltaTime * _moveSpeed * Vector3.left;

        if (Input.GetKey(KeyCode.UpArrow) && transform.position.z < 10)
            transform.position += Time.deltaTime * _moveSpeed * Vector3.forward;

        if (Input.GetKey(KeyCode.DownArrow) && transform.position.z > -1)
            transform.position += Time.deltaTime * _moveSpeed * Vector3.back;


        if (Input.GetKey(KeyCode.Space))
            transform.position = new Vector3(0, 0.5f, 0);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("SlowDown"))
        {
            other.gameObject.SetActive(false);
            _moveSpeed = 3f;
        }
        if (other.gameObject.CompareTag("NormalSpeed"))
        {
            other.gameObject.SetActive(false);
            _moveSpeed = 15f;
        }
        //if (other.gameObject.CompareTag("Bumpy"))
        //{
        //    other.gameObject.SetActive(false);
        //    _moveSpeed = 15f;
        //}


    }
}
