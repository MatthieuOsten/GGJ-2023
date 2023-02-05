using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;

[CustomEditor(typeof(Clock))]
public class ClockEditor : Editor
{
    [SerializeField] private Texture2D _clock;

    [SerializeField] private bool _showClock = false;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        serializedObject.Update();

        CheckReferences();
        ShowClock();

        serializedObject.ApplyModifiedProperties();

    }

    private void CheckReferences()
    {
        SerializedProperty needle = serializedObject.FindProperty("_needle");
        SerializedProperty imageFilling = serializedObject.FindProperty("_imageFilling");

        SerializedProperty boolNeedle = serializedObject.FindProperty("_fillingToNeedle");

        if (boolNeedle.boolValue)
        {
            if (needle.objectReferenceValue == null || imageFilling.objectReferenceValue == null)
            {
                _showClock = true;
            }
            else
            {
                _showClock = false;
            }
        }
        else
        {
            if (imageFilling.objectReferenceValue == null)
            {
                _showClock = true;
            }
            else
            {
                _showClock = false;
            }
        }
    }

    private void ShowClock()
    {
        GUILayout.BeginVertical();
        GUILayout.FlexibleSpace();

        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();

        if (_showClock)
        {
            _clock = GeometryGenerator.CreateTextureCircle(50,Color.blue);
            _clock = GeometryGenerator.CreateTextureCircle(ref _clock, 30, Color.yellow);

            GUILayout.Box(_clock);
        }

        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();

        GUILayout.EndVertical();
    }
}
#endif
