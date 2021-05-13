using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(OvalPattern))]
public class OvalPatternEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        OvalPattern ovalPattern = (OvalPattern)target;
        if (GUILayout.Button("Execute"))
        {
            ovalPattern.Execute();
        }
    }
}
