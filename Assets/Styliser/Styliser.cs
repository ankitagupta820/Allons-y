using UnityEngine;

[ExecuteInEditMode]
public class Styliser: MonoBehaviour
{
    public Shader Shader;
    public Texture Texture;
    public float TextureSize = 8;
    public float Rotation = 0;
    [Range(0, 2)]
    public float Softness = 1f;
    [Range(0, 1)]
    public float TransitionSize = 0.5f;
    public Step[] Steps = {new Step {Color = Color.black, StartLightness = 0.2f}, new Step {Color = Color.white, StartLightness = 0.8f}};

    Material _material;
    const int MaxLength = 8;
    readonly float[] _starts = new float[MaxLength];
    readonly Color[] _colors = new Color[MaxLength];
    int _textureSizeID, _startsID, _colorsID, _textureID, _softnessID, _rotationMatrixID, _transitionSizeID;


    void Awake()
    {
        _textureSizeID = Shader.PropertyToID("_TextureSize");
        _startsID = Shader.PropertyToID("_Starts");
        _colorsID = Shader.PropertyToID("_Colors");
        _textureID = Shader.PropertyToID("_Texture");
        _softnessID = Shader.PropertyToID("_Softness");
        _rotationMatrixID = Shader.PropertyToID("_RotationMatrix");
        _transitionSizeID = Shader.PropertyToID("_TransitionSize");
    }

    void OnValidate()
    {
        if (Steps.Length > MaxLength)
            Debug.LogError("You reach the limit of steps count");

    }

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (_material == null)
        {
            _material = new Material(Shader) {hideFlags = HideFlags.DontSave};
        }
        
        UpdateSettings();

        Graphics.Blit(source, destination, _material);
    }

    void UpdateSettings()
    {
        for (int i = 0; i < MaxLength; i++)
        {
            if (i < Steps.Length)
            {
                _starts[i] = Steps[i].StartLightness;
                _colors[i] = Steps[i].Color;
            }
            else
            {
                _starts[i] = float.MaxValue;
                _colors[i] = Color.black;
            }
        }

        _material.SetFloat(_textureSizeID, TextureSize);
        _material.SetFloatArray(_startsID, _starts);
        _material.SetColorArray(_colorsID, _colors);
        _material.SetTexture(_textureID, Texture);
        _material.SetFloat(_softnessID, 0.5f * TextureSize / Softness);
        _material.SetVector(_rotationMatrixID, GetRotationMatrix(Rotation));
        _material.SetFloat(_transitionSizeID, 1f / TransitionSize);
    }

    static Vector4 GetRotationMatrix(float rotation)
    {
        rotation *= Mathf.Deg2Rad;
        float cos = Mathf.Cos(rotation);
        float sin = Mathf.Sin(rotation);
        return new Vector4(cos, -sin, sin, cos);
    }

    [System.Serializable]
    public class Step
    {
        [Range(0, 1)]
        public float StartLightness = 1f;
        public Color Color;
    }
}