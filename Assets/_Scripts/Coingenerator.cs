using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coingenerator : MonoBehaviour
{
    public GameObject[] cloud;
    public GameObject player;
    public float cloudGenerateRadius = 2f;
    public float maxCloudGenerateRadius = 5f;
    private IEnumerator coroutine;

    private void Start()
    {
        // make sure that the clouds don't collide with the player
        cloudGenerateRadius += player.GetComponent<SphereCollider>().radius;
        maxCloudGenerateRadius += player.GetComponent<SphereCollider>().radius;
        coroutine = GenerateCloudRoutine(2f);
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
            GenerateCloud(2);
        }
    }


    private void GenerateCloud(int quantity)
    {
        Vector3 playerLoc = player.transform.position;
        for (int i = 0; i < quantity; i++)
        {
            Vector3 newCloudLoc = new Vector3(Random.Range(-cloudGenerateRadius, maxCloudGenerateRadius) + playerLoc.x,
            Random.Range(-maxCloudGenerateRadius, -cloudGenerateRadius) + playerLoc.y,
            Random.Range(-cloudGenerateRadius, maxCloudGenerateRadius) + playerLoc.z
            );
            GameObject cloudType = cloud[Random.Range(0, cloud.Length - 1)];
            GameObject newCloud = Instantiate(cloudType, newCloudLoc, cloudType.transform.rotation);
            newCloud.transform.localScale = new Vector3(Random.Range(1f, 5f),
                Random.Range(1f, 5f),
                Random.Range(1f, 5f));
        }
    }
}
