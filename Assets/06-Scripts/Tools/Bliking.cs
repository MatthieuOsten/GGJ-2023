using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class Bliking : MonoBehaviour
{
    [SerializeField] private Timer _timerBlink = new Timer(0.2f);
    [SerializeField] private Timer _timerEnabled = new Timer(2f);

    [SerializeField] private GameObject _target;

    [SerializeField] private bool _isActive;
    [SerializeField] private bool _infinity;

    public bool IsActive { get { return _isActive; } }

    private void Start()
    {
        _timerBlink.Start();
        _timerEnabled.Start();
    }

    // Update is called once per frame
    void Update()
    {
        if (_target != null && _isActive)
        {
            Blink();
        }
    }

    /// <summary>
    /// Blink in difinite time in _timerBlink
    /// </summary>
    private void Blink()
    {

        if (_timerBlink.Update())
        {
            if (_target.activeSelf)
            {
                _target.SetActive(false);
            }
            else
            {
                _target.SetActive(true);
            }

            _timerBlink.Restart();
        }

        if (!_infinity)
        {
            if (_timerEnabled.Update())
            {
                StopBlink();
            }
        }

    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    public void SetSpeed(float value)
    {
        _timerBlink.TimerTo = value;
    }

    /// <summary>
    /// Start blink with unlimited time
    /// </summary>
    public void StartBlink()
    {
        _infinity = true;
        _isActive = true;
    }

    /// <summary>
    /// Start blink with finish time
    /// </summary>
    /// <param name="timeTo">Time to stop blink</param>
    public void StartBlink(float timeTo)
    {
        _infinity = false;

        _timerEnabled.TimerTo = timeTo;
        _timerEnabled.Restart();

        _isActive = true;
    }

    /// <summary>
    /// Start blink with other object
    /// </summary>
    /// <param name="target">the object who blink</param>
    public void StartBlink(GameObject target)
    {
        _infinity = true;

        if (target == null) {
            Debug.LogWarning("Selected target is NULL");
            return; 
        }

        _target = target;

        _isActive = true;
    }

    /// <summary>
    /// Start blink, change the target and difine time to finish blink
    /// </summary>
    /// <param name="target"></param>
    /// <param name="timeTo"></param>
    public void StartBlink(GameObject target, float timeTo)
    {
        _infinity = false;

        if (target == null)
        {
            Debug.LogWarning("Selected target is NULL");
            return;
        }

        _target = target;

        _timerEnabled.TimerTo = timeTo;
        _timerEnabled.Restart();

        _isActive = true;
    }

    /// <summary>
    /// Stop the blink of object
    /// </summary>
    public void StopBlink()
    {
        _target.SetActive(true);

        _infinity = false;
        _isActive = false;

        _timerBlink.Restart();
        _timerEnabled.Restart();
    }

    /// <summary>
    /// For test timer
    /// </summary>
    public void StartTimer()
    {
        _infinity = false;
        _timerEnabled.Restart();
    }

}

#if UNITY_EDITOR

[CustomEditor(typeof(Bliking))]
public class BlinkingEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        Bliking myScript = (Bliking)target;

        if (GUILayout.Button("Start")) { myScript.StartBlink(); }
        if (GUILayout.Button("Stop")) { myScript.StopBlink(); }
        if (GUILayout.Button("StartTime")) { myScript.StartTimer(); }
    }

}

#endif
