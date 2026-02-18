using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(User))]
public class LocalUserEditor : Editor
{
    public override void OnInspectorGUI()
    {

        serializedObject.Update();

        DrawDefaultInspector();

        var script = (User)target;


        if (script != null)
        {
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Runtime User Debug", EditorStyles.boldLabel);
            //EditorGUILayout.LabelField("GlobalName", script.UserModel.globalName ?? "null");
        }
        

        serializedObject.ApplyModifiedProperties();

        Repaint();
    }
}
