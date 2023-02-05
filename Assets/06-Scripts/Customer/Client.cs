using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class Client : MonoBehaviour
{
    public List<string> _productGenerated = new List<string>();
    public List<Sprite> _images = new List<Sprite>();

    [SerializeField] private Transform _panel;
    [SerializeField] private Timer _timerDispawn;
    [SerializeField] private Clock _clock;
    [SerializeField] private float _timeToDispawn = 10;

    public Transform Panel
    {
        get
        {
            return _panel;
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i != _images.Count; i++)
        {
            _panel.GetChild(i).GetComponent<Image>().sprite = _images[i];
            _panel.GetChild(i).GetComponent<Image>().gameObject.SetActive(true);
        }
        _timerDispawn.TimerTo = _timeToDispawn;
        _timerDispawn.Start();
        _timerDispawn.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        int temp = 0;
        _timerDispawn.Update();
        for (int i = 0; i != _images.Count; i++)
        {
            if (_panel.GetChild(i).GetComponent<Image>().gameObject.activeSelf)
            {
                temp++;
            }
        }

        if (temp > 0)
        {
            if (_timerDispawn.IsPlaying == false)
            {
                _timerDispawn.Play();
            }
            _clock.ActualFilling = _timerDispawn.Time / _timeToDispawn;
        } 
        else
        {
            gameObject.SetActive(false);
            _timerDispawn.Restart();
            _timerDispawn.Stop();
        }

        if (_clock.ActualFilling <= 0)
        {
            gameObject.SetActive(false);
            _timerDispawn.Restart();
            _timerDispawn.Stop();
        }
        
        if (gameObject.GetComponentInChildren<CanvasScaler>().referencePixelsPerUnit < 200)
        {
            gameObject.GetComponentInChildren<CanvasScaler>().referencePixelsPerUnit++;
        }
    }
}
