using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeBlackHoles : MonoBehaviour {
    public int rowCount = 36;
    public int columnCount = 30;
    public int layerCount = 3;

    public GameObject boxTemplate;

    float rotationTick;

    GameObject[] boxMeshs;

    int meshCount;
    float timeTick;
    // Use this for initialization
    void Start () {
        meshCount = rowCount * columnCount * layerCount;
        boxMeshs = new GameObject[meshCount];

        for (int i=0;i<meshCount;i++)
        {
            GameObject go = Instantiate(boxTemplate);

            go.transform.parent = this.transform;

            boxMeshs[i] = go;
        }
        
	}
	
	// Update is called once per frame
	void Update () {
        timeTick += Time.deltaTime;

        rotationTick -= Time.deltaTime * 5f ;

        transform.rotation = Quaternion.Euler(0, rotationTick, 0);
        int i = 0;

        for(float x=0;x<rowCount;x++)
        {
            float a = x / rowCount * Mathf.PI * 2;
            float X = Mathf.Cos(a) / 2f;
            float Z= Mathf.Sin(a) / 2f;
            float t = timeTick % 1f;

            for(float y=0;y<layerCount;y++)
            {
                float shift = y * Mathf.Abs(Mathf.Sin(x / 1.3f)) +
                                Mathf.Sin(x / 1.3f) +
                                Mathf.Cos(x / 1.7f) - layerCount;

                for(float z=0;z<columnCount;z++)
                {
                    float t1 = Mathf.Max(0, (3 - z) + timeTick % 1 - shift);
                    float Y = y - Mathf.Pow(t1, 3);

                    boxMeshs[i].transform.localPosition = new Vector3(X * (z + 4 - t), Y, Z * (z + 4 - t));
                    boxMeshs[i].transform.localRotation = Quaternion.Euler(0, -a* Mathf.Rad2Deg, 0);

                    i += 1;


                }
            }
        }
    }
}
