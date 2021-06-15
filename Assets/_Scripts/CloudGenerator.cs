using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudGenerator : MonoBehaviour
{
    public GameObject[] cloud;
    public GameObject player;
    public GameObject mainCamera;
    /*   public float cloudGenerateRadius = 5f;
       public float maxCloudGenerateRadius = 10f;*/
    public float xBoundaryOffsetLow = 1f;
    public float xBoundaryOffsetHigh = 2f;
    public float yBoundaryOffsetLow = -120f;
    public float yBoundaryOffsetHigh = -80f;
    public float zBoundaryOffsetLow = -1f;
    public float zBoundaryOffsetHigh = -2f;
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
        Vector3 cameraLoc = mainCamera.transform.position;
        for (int i=0; i < quantity; i++)
        {
            float randomYDistance = Random.Range(yBoundaryOffsetLow, yBoundaryOffsetHigh);
            /*float randomXDistance = (cameraLoc.y - playerLoc.y) randomYDistance*/
            float XLowDis = (xBoundaryOffsetLow / (cameraLoc.y - playerLoc.y)) * (cameraLoc.y - playerLoc.y + randomYDistance);
            float XHighDis = (xBoundaryOffsetHigh / (cameraLoc.y - playerLoc.y)) * (cameraLoc.y - playerLoc.y + randomYDistance);
            float ZLowDis = (zBoundaryOffsetLow / (cameraLoc.y - playerLoc.y)) * (cameraLoc.y - playerLoc.y + randomYDistance);
            float ZHighDis = (zBoundaryOffsetHigh / (cameraLoc.y - playerLoc.y)) * (cameraLoc.y - playerLoc.y + randomYDistance);




            Vector3 newCloudLoc = new Vector3(Random.Range(XLowDis, XHighDis),
            randomYDistance + playerLoc.y,
            Random.Range(ZLowDis, ZHighDis)
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
