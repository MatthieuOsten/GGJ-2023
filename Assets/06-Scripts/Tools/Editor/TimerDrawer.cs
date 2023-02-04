using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;

[CustomPropertyDrawer(typeof(Timer))]
public class TimerDrawer : PropertyDrawer
{
    // Draw the property inside the given rect
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // Using BeginProperty / EndProperty on the parent property means that
        // prefab override logic works on the entire property.
        EditorGUI.BeginProperty(position, label, property);

        // Calculate rects
        int padding = 5;
        int valueWidthPartTime = (int)(position.width / 2.5);

        int valueWidthFinishLabel = 35,
            valueWidthPlayLabel = 35,
            valueWidthToggle = 10;

        int relativeWidthField = valueWidthPartTime / 3,
            relativeWidthFinishLabel = (valueWidthPartTime - (relativeWidthField + (valueWidthToggle * 2))) / 2,
            relativeWidthPlayLabel = valueWidthFinishLabel - 20;

        int finalWidthPlayLabel = (relativeWidthPlayLabel > valueWidthPlayLabel) ? relativeWidthPlayLabel : valueWidthPlayLabel,
            finalWidthFinishLabel = (relativeWidthFinishLabel > valueWidthFinishLabel) ? relativeWidthFinishLabel : valueWidthFinishLabel;

        float valuePositionX = position.x;

        // Draw label
        if (position.width > 250)
        {
            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

            valueWidthPartTime = (int)(position.width / 2.5);

            relativeWidthField = valueWidthPartTime / 3;
            relativeWidthFinishLabel = (valueWidthPartTime - (relativeWidthField + (valueWidthToggle * 2))) / 2;
            relativeWidthPlayLabel = valueWidthFinishLabel - 20;

            finalWidthPlayLabel = (relativeWidthPlayLabel > valueWidthPlayLabel) ? relativeWidthPlayLabel : valueWidthPlayLabel;
            finalWidthFinishLabel = (relativeWidthFinishLabel > valueWidthFinishLabel) ? relativeWidthFinishLabel : valueWidthFinishLabel;

            valuePositionX = position.x;
        }
        else if (position.width > 150)
        {
            var nameRect = new Rect(valuePositionX, position.y, relativeWidthField, position.height);
            EditorGUI.LabelField(nameRect, "Timer ");
            valuePositionX += relativeWidthField + padding;
        }
        else if (position.width > 100)
        {
            var nameRect = new Rect(valuePositionX, position.y, 10, position.height);
            EditorGUI.LabelField(nameRect, "T ");
            valuePositionX += 10 + padding;
        }


        if (position.width > 250)
        {

            var timeRect = new Rect(valuePositionX, position.y, relativeWidthField, position.height);
            valuePositionX += relativeWidthField + padding;
            var timePassedRect = new Rect(valuePositionX, position.y, relativeWidthField, position.height);
            valuePositionX += relativeWidthField + padding;
            var timerToRect = new Rect(valuePositionX, position.y, relativeWidthField, position.height);
            valuePositionX += relativeWidthField + 15;

            var playLabelRect = new Rect(valuePositionX, position.y, finalWidthPlayLabel, position.height);
            valuePositionX += valueWidthPlayLabel - 10 + padding;
            var playRect = new Rect(valuePositionX, position.y, valueWidthToggle, position.height);
            valuePositionX += valueWidthToggle + padding;
            var finishLabelRect = new Rect(valuePositionX, position.y, finalWidthFinishLabel, position.height);
            valuePositionX += valueWidthFinishLabel + padding;
            var finishRect = new Rect(valuePositionX, position.y, valueWidthToggle, position.height);

            // Draw fields - passs GUIContent.none to each so they are drawn without labels
            EditorGUI.BeginDisabledGroup(true);
            EditorGUI.FloatField(timeRect, property.FindPropertyRelative("_time").floatValue);
            EditorGUI.FloatField(timePassedRect, property.FindPropertyRelative("_timePassed").floatValue);
            EditorGUI.EndDisabledGroup();
            property.FindPropertyRelative("_timer").floatValue = EditorGUI.FloatField(timerToRect, property.FindPropertyRelative("_timer").floatValue);
            EditorGUI.LabelField(playLabelRect, "Play ");
            property.FindPropertyRelative("_play").boolValue = EditorGUI.Toggle(playRect, property.FindPropertyRelative("_play").boolValue);
            EditorGUI.BeginDisabledGroup(true);
            EditorGUI.LabelField(finishLabelRect, "Finish ");
            EditorGUI.Toggle(finishRect, property.FindPropertyRelative("_finish").boolValue);
            EditorGUI.EndDisabledGroup();
        }
        else if (position.width > 150)
        {
            finalWidthPlayLabel = 10;
            finalWidthFinishLabel = 10;

            // Calculate rects
            var timeRect = new Rect(valuePositionX, position.y, relativeWidthField, position.height);
            valuePositionX += relativeWidthField + padding;
            var timePassedRect = new Rect(valuePositionX, position.y, relativeWidthField, position.height);
            valuePositionX += relativeWidthField + padding;
            var timerToRect = new Rect(valuePositionX, position.y, relativeWidthField, position.height);
            valuePositionX += relativeWidthField + 15;

            var playLabelRect = new Rect(valuePositionX, position.y, finalWidthPlayLabel, position.height);
            valuePositionX += finalWidthPlayLabel + padding;
            var playRect = new Rect(valuePositionX, position.y, valueWidthToggle, position.height);
            valuePositionX += valueWidthToggle + padding;
            var finishLabelRect = new Rect(valuePositionX, position.y, finalWidthFinishLabel, position.height);
            valuePositionX += finalWidthFinishLabel + padding;
            var finishRect = new Rect(valuePositionX, position.y, valueWidthToggle, position.height);

            // Draw fields - passs GUIContent.none to each so they are drawn without labels
            EditorGUI.BeginDisabledGroup(true);
            EditorGUI.FloatField(timeRect, property.FindPropertyRelative("_time").floatValue);
            EditorGUI.FloatField(timePassedRect, property.FindPropertyRelative("_timePassed").floatValue);
            EditorGUI.EndDisabledGroup();
            property.FindPropertyRelative("_timer").floatValue = EditorGUI.FloatField(timerToRect, property.FindPropertyRelative("_timer").floatValue);
            EditorGUI.LabelField(playLabelRect, "P ");
            property.FindPropertyRelative("_play").boolValue = EditorGUI.Toggle(playRect, property.FindPropertyRelative("_play").boolValue);
            EditorGUI.BeginDisabledGroup(true);
            EditorGUI.LabelField(finishLabelRect, "F ");
            EditorGUI.Toggle(finishRect, property.FindPropertyRelative("_finish").boolValue);
            EditorGUI.EndDisabledGroup();
        }
        else if (position.width > 100)
        {
            finalWidthPlayLabel = 10;

            // Calculate rects
            var timeRect = new Rect(valuePositionX, position.y, relativeWidthField, position.height);
            valuePositionX += relativeWidthField + padding;
            var timerToRect = new Rect(valuePositionX, position.y, relativeWidthField, position.height);
            valuePositionX += relativeWidthField + 15;

            var playLabelRect = new Rect(valuePositionX, position.y, finalWidthPlayLabel, position.height);
            valuePositionX += finalWidthPlayLabel + padding;
            var playRect = new Rect(valuePositionX, position.y, valueWidthToggle, position.height);
            valuePositionX += valueWidthToggle + padding;
            var finishRect = new Rect(valuePositionX, position.y, valueWidthToggle, position.height);

            // Draw fields - passs GUIContent.none to each so they are drawn without labels
            EditorGUI.BeginDisabledGroup(true);
            EditorGUI.FloatField(timeRect, property.FindPropertyRelative("_time").floatValue);
            EditorGUI.EndDisabledGroup();
            property.FindPropertyRelative("_timer").floatValue = EditorGUI.FloatField(timerToRect, property.FindPropertyRelative("_timer").floatValue);
            EditorGUI.LabelField(playLabelRect, "P ");
            property.FindPropertyRelative("_play").boolValue = EditorGUI.Toggle(playRect, property.FindPropertyRelative("_play").boolValue);
            EditorGUI.BeginDisabledGroup(true);
            EditorGUI.Toggle(finishRect, property.FindPropertyRelative("_finish").boolValue);
            EditorGUI.EndDisabledGroup();
        }

        EditorGUI.EndProperty();
    }
}

#endif