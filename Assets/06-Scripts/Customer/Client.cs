using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Client : MonoBehaviour
{
    public List<string> _productGenerated = new List<string>();
    public List<Sprite> _images = new List<Sprite>();

    [SerializeField] private Transform _panel;

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
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.GetComponentInChildren<CanvasScaler>().referencePixelsPerUnit < 200)
        {
            gameObject.GetComponentInChildren<CanvasScaler>().referencePixelsPerUnit++;
        }
    }
}
