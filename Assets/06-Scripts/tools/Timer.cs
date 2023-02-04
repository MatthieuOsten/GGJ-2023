using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[System.Serializable]
public class Timer
{
    [Header("TIME")]
    [SerializeField] private float _time = 0;
    [SerializeField] private float _timePassed = 0;
    [SerializeField] private float _timer;
    [SerializeField, Range(0.5f,3)] private float _speed = 1;

    [Header("STATE")]
    [SerializeField] private bool _play = true;
    [SerializeField] private bool _finish = false;

    public bool IsPlaying { get { return _play; } private set { _play = value; } }
    public bool IsFinish { get { return _finish; } private set { _finish = value; } }

    /// <summary>
    /// Actual time
    /// </summary>
    public float Time { 
        get { return _time; }
        set {
            if (value > TimerTo) { value = TimerTo; }

            _timePassed = (TimerTo - value);
            _time = value; 
        }
    }

    /// <summary>
    /// Timer max value
    /// </summary>
    public float TimerTo { 
        get { return _timer; } 
        set { 
            if (value > 0) _timer = value;
            else _timer = 1;
        } 
    }

    /// <summary>
    /// Time passed on time
    /// </summary>
    public float TimePassed
    {
        get { return _timePassed; }
        set {
            if (value < 0) { value = 0; }
            else if (value > TimerTo) { value = TimerTo; }

            _time = (TimerTo - value);
            _timePassed = value; 
        }
    }

    /// <summary>
    /// Constructor of timer
    /// </summary>
    /// <param name="timer">Take the end of timer</param>
    public Timer(float timer)
    {
        TimerTo = timer;
    }

    /// <summary>
    /// Return Time, Time Passed and Timer To
    /// </summary>
    /// <returns>Return Time, Time Passed and Timer To</returns>
    public override string ToString()
    {
        // Format the timer values as desired

        if (TimerTo <= 0) { return "TimerTo is a zero or negative value"; }

        return string.Format("Timeer -> ActualTime: {0}, Time Passed: {1}, Timer To: {2}", Time, TimePassed, TimerTo);
    }

    /// <summary>
    /// Start the timer and play this
    /// </summary>
    public void Start()
    {
        if (Time <= 0)
        {
            IsFinish = false;
            IsPlaying = true;
            Time = TimerTo;
        }
    }

    /// <summary>
    /// Stop playing the timer
    /// </summary>
    public void Stop()
    {
        IsPlaying = false;
    }

    /// <summary>
    /// Play the timer
    /// </summary>
    public void Play()
    {
        IsPlaying = true;
    }

    /// <summary>
    /// Restart timer, reset Time and playing
    /// </summary>
    public void Restart()
    {
        IsFinish = false;
        IsPlaying = true;
        Time = TimerTo;
    }

    /// <summary>
    /// Reset comportement of the timer
    /// </summary>
    public void Reset()
    {
        if (Time <= 0)
        {
            IsFinish = false;
            IsPlaying = true;
            Time = 0.01f;
        }
    }

    /// <summary>
    /// Finish artificial timer
    /// </summary>
    public void FinishTimer()
    {
        IsFinish = true;
        IsPlaying = true;
        Time = 0f;
    }

    /// <summary>
    /// Forward the timer
    /// </summary>
    /// <param name="value">Time to forward</param>
    public void FastForward(float value)
    {
        Time -= value;
    }

    /// <summary>
    /// Update the timer 
    /// </summary>
    /// <returns>If true time < 0 else is false</returns>
    public bool Update()
    {
        if (IsPlaying)
        {
            if (Time > 0)
            {
                Time -= UnityEngine.Time.deltaTime * _speed;
                if (Time == 0) { Time -= 1; }

                return false;
            }
            else if (Time < 0)
            {
                IsFinish = true;

                Time = 0;
                return true;
            }
            else
            {
                IsPlaying = false;
                return false;
            }
        }
        else
        {
            return false;
        }
    }

}

#if UNITY_EDITOR

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
        if ( position.width > 250 )
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
        else if ( position.width > 150 ) 
        {
            var nameRect = new Rect(valuePositionX, position.y, relativeWidthField, position.height);
            EditorGUI.LabelField(nameRect,"Timer ");
            valuePositionX += relativeWidthField + padding;
        }
        else if (position.width > 100) {
            var nameRect = new Rect(valuePositionX, position.y, 10, position.height);
            EditorGUI.LabelField(nameRect, "T ");
            valuePositionX += 10 + padding;
        }


        if ( position.width > 250 )
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
        } else if (position.width > 100)
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