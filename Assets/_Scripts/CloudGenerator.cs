using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudGenerator : MonoBehaviour
{
    public GameObject[] cloud;
    public GameObject player;
    public GameObject LeftUpper;
    public GameObject RightUpper;
    public GameObject LeftLower;
    public GameObject RightLower;
    public float interval = 3f;
    private IEnumerator coroutine;

    private void Start()
    {
        // make sure that the clouds don't collide with the player
      /*  cloudGenerateRadius += player.GetComponent<CapsuleCollider>().radius;
        maxCloudGenerateRadius += player.GetComponent<CapsuleCollider>().radius;*/
        coroutine = GenerateCloudRoutine(interval);
        StartCoroutine(coroutine);
    }

    void Update()
    {
    }

    private IEnumerator GenerateCloudRoutine(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            GenerateCloud(1);
        }
    }


    private void GenerateCloud(int quantity)
    {
        Vector3 playerLoc = player.transform.position;
        for (int i=0; i < quantity; i++)
        {
            Vector3 newCloudLoc = new Vector3(Random.Range(LeftUpper.transform.position.x, RightUpper.transform.position.x),
            Random.Range(-120f, -80f) + playerLoc.y,
            Random.Range(LeftLower.transform.position.z, RightLower.transform.position.z)
            );
            GameObject cloudType = cloud[Random.Range(0, cloud.Length - 1)];
            GameObject newCloud = Instantiate(cloudType, newCloudLoc, cloudType.transform.rotation);
            float randomScale = Random.Range(.3f, 1f);
            newCloud.transform.localScale = new Vector3(randomScale,
                randomScale,
                randomScale);
            newCloud.transform.Rotate(Vector3.up, Random.Range(0f, 180f), Space.World);
        }
    }
}
