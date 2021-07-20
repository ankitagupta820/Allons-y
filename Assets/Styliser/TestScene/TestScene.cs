using UnityEngine;
using System.Collections;

public class TestScene : MonoBehaviour
{
    public int Count = 20;
    public float ReturnSpeed = 0.01f;
    public float MinStartVelicity = 2f;
    public float MaxStartVelicity = 3f;
    public float MinSize = 0.5f;
    public float MaxSize = 2f;
    public float MaxBornPosition = 5f;

    PhisObj[] _objs;


    void Awake()
    {
        _objs = new PhisObj[Count];
        for (int i = 0; i < Count; i++)
        {
            _objs[i] = new PhisObj();
            _objs[i].Tr = GameObject.CreatePrimitive(GetPrimitiveType()).transform;
            _objs[i].Tr.localScale = Vector3.one * Random.Range(MinSize, MaxSize);
            _objs[i].Tr.rotation = Random.rotation;
            _objs[i].Tr.position = Random.insideUnitSphere * MaxBornPosition;
            _objs[i].Velocity = Vector3.Cross(Random.onUnitSphere, _objs[i].Tr.position) * Random.Range(MinStartVelicity, MaxStartVelicity);
        }
        Application.targetFrameRate = -1;
    }
    static PrimitiveType GetPrimitiveType()
    {
        int val = Random.Range(0, 10);
        if (val < 2) return PrimitiveType.Cube;
        return PrimitiveType.Sphere;
    }
    /*
    int _frames;
    float tt;
    string s="";
    string s2 = "";
    void Update()
    {
        if (tt < Time.time)
        {
            tt += 1f;
            s = _frames + " fps";
            _frames = 0;
        }
        _frames++;

        if (Input.GetMouseButtonDown(0))
        {
            Dithering dithering = FindObjectOfType<Dithering>();
            int st = (int) dithering.ShaderType;
            st++;
            if (st > 2) st = 0;
            dithering.ShaderType = (Dithering.Type) st;
            dithering.Repaint();
            s2 = dithering.ShaderType.ToString();
        }
    }
    void OnGUI()
    {
        GUILayout.Label(s);
        GUILayout.Label(s2);
    }
    //*/

    void FixedUpdate()
    {
        float dt = Time.fixedDeltaTime;
        for (int i = 0; i < Count; i++)
        {
            _objs[i].Velocity -= _objs[i].Tr.position * ReturnSpeed;
            _objs[i].Tr.position += _objs[i].Velocity * dt;
        }
    }

    public class PhisObj
    {
        public Transform Tr;
        public Vector3 Velocity;
    }
}
