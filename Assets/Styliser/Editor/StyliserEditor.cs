using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Styliser),true)]
public class StyliserEditor : Editor
{
    Styliser _styliser;
    public override void OnInspectorGUI()
    {
        if (_styliser == null) _styliser = (Styliser)target;


        _styliser.Texture = (Texture)EditorGUILayout.ObjectField("Pattern", _styliser.Texture, typeof (Texture), true);

        _styliser.TextureSize = EditorGUILayout.FloatField("Pattern Size", _styliser.TextureSize);
        _styliser.Rotation = EditorGUILayout.FloatField("Rotation", _styliser.Rotation);
        _styliser.Softness = EditorGUILayout.Slider("Softness", _styliser.Softness, 0, 2);
        _styliser.TransitionSize = EditorGUILayout.Slider("Transition Size", _styliser.TransitionSize, 0, 1);

        GUILayout.Space(16);
        GUILayout.Label("Steps");
        for (int i = 0; i < _styliser.Steps.Length; i++)
        {
            Styliser.Step step = _styliser.Steps[i];
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("[" + i + "]", GUILayout.ExpandWidth(false), GUILayout.MaxWidth(30));
            EditorGUILayout.BeginVertical();
            step.StartLightness = EditorGUILayout.Slider("Start Lightness", step.StartLightness, 0, 1);
            step.Color = EditorGUILayout.ColorField("Color", step.Color);
            EditorGUILayout.EndVertical();
            if (GUILayout.Button("X", GUILayout.ExpandWidth(false)))
            {
                ArrayUtility.RemoveAt(ref _styliser.Steps, i);
            }
            EditorGUILayout.EndHorizontal();
        }
        EditorGUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        if (GUILayout.Button("+", GUILayout.ExpandWidth(false)))
        {
            Styliser.Step lastStep = _styliser.Steps[_styliser.Steps.Length - 1];
            ArrayUtility.Add(ref _styliser.Steps, new Styliser.Step {Color = lastStep.Color, StartLightness = lastStep.StartLightness});
        }
        EditorGUILayout.EndHorizontal();
    }
}
