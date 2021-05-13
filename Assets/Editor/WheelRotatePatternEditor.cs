using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(WheelRotatePattern))]
public class WheelRotatePatternEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        WheelRotatePattern wheelRotatePattern = (WheelRotatePattern)target;
        if (GUILayout.Button("Execute"))
        {
            wheelRotatePattern.Execute();
        }
    }
}
