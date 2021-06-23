using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ObjectGenerator : MonoBehaviour
{
    public GameObject[] objects;
    public GameObject player;
    //public int _NumofBonds = 4;
   [Header("Bounding Coners")]
    public GameObject LeftUpper;
    public GameObject RightUpper;
    public GameObject LeftLower;
    public GameObject RightLower;
    [Header("Generation Specs")]
    [Tooltip("Time interval  between each generation")]
    public float interval = 3f;
    [Tooltip("Amount of the random objects generated each time")]
    public int amount = 1;
    [Tooltip("Random vertical distance between min vertical distance and max vertical distance," +
        " choose between -inf to inf")]
    public float minVerticalDistance = -120f;
    [Tooltip("Random vertical distance between min vertical distance and max vertical distance," +
        " choose between -inf to inf")]
    public float maxVerticalDistance = -80f;


    [Tooltip("Random scale down, choose between 0 to inf")]
    public float minScale = 0.3f;
    [Tooltip("Random scale up, choose between 0 to inf")]
    public float maxScale = 1f;
    [Header("Rotation Axis")]
    public bool rotateX;
    public bool rotateY = true;
    public bool rotateZ;
    [Tooltip("Minimum rotate degrees, choose between 0 to 360")]
    public float minRotate = 0f;
    [Tooltip("Maximum rotate degrees, choose between 0 to 360")]
    public float maxRotate = 180f;

    private IEnumerator coroutine;

    private void Reset()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        LeftUpper = GameObject.Find("LeftUpper");
        RightUpper = GameObject.Find("RightUpper");
        LeftLower = GameObject.Find("LeftLower");
        RightLower = GameObject.Find("RightLower");
    }

    private void Start()
    {
        // make sure that the clouds don't collide with the player
        /*  cloudGenerateRadius += player.GetComponent<CapsuleCollider>().radius;
          maxCloudGenerateRadius += player.GetComponent<CapsuleCollider>().radius;*/
        coroutine = GeneratorRoutine(interval);
        StartCoroutine(coroutine);
    }

    void Update()
    {
    }

    private IEnumerator GeneratorRoutine(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            GenerateObj(amount);
        }
    }


    private void GenerateObj(int quantity)
    {
        Vector3 playerLoc = player.transform.position;
        for (int i = 0; i < quantity; i++)
        {
            Vector3 newObjLoc = new Vector3(Random.Range(LeftUpper.transform.position.x, RightUpper.transform.position.x),
            Random.Range(minVerticalDistance, maxVerticalDistance) + playerLoc.y,
            Random.Range(LeftLower.transform.position.z, RightLower.transform.position.z)
            );
            GameObject objType = objects[Random.Range(0, objects.Length - 1)];
            GameObject newCloud = Instantiate(objType, newObjLoc, objType.transform.rotation);


            if (minScale < 0) minScale = 0;
            if (maxScale < 0) maxScale = 0;
            float randomScale = Random.Range(minScale, maxScale);
            newCloud.transform.localScale = new Vector3(randomScale,
                randomScale,
                randomScale);

            Vector3 rotateAxis = new Vector3(System.Convert.ToSingle(rotateX),
                System.Convert.ToSingle(rotateY),
                System.Convert.ToSingle(rotateZ));
            newCloud.transform.Rotate(rotateAxis, Random.Range(minRotate, maxRotate), Space.World);
        }
    }
}

//[CustomEditor(typeof(ObjectGenerator))]
//public class ObjectGeneratorEditor:Editor
//{
//    public override void OnInspectorGUI()
//    {
//        var objectGenerator = target as ObjectGenerator;
//        objectGenerator._NumofBonds = EditorGUILayout.IntField("Number of Bounds", objectGenerator._NumofBonds);

//        using (var group = new EditorGUILayout.FadeGroupScope(System.Convert.ToSingle(objectGenerator._NumofBonds>0)))
//        {
//            if (group.visible == true)
//            {
//                EditorGUI.indentLevel++;
//                objectGenerator.LeftUpper = (GameObject)EditorGUILayout.ObjectField("LeftUpper", objectGenerator.LeftUpper, typeof(GameObject), true);
//                objectGenerator.RightUpper = (GameObject)EditorGUILayout.ObjectField("RightUpper", objectGenerator.RightUpper, typeof(GameObject), true);
//                objectGenerator.LeftLower = (GameObject)EditorGUILayout.ObjectField("LeftLower", objectGenerator.LeftLower, typeof(GameObject), true);
//                objectGenerator.RightLower = (GameObject)EditorGUILayout.ObjectField("RightLower", objectGenerator.RightLower, typeof(GameObject), true);

//                EditorGUI.indentLevel--;
//            }
//        }
//    }
//}