using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragNDrop : MonoBehaviour
{
    [Header("Position")]
    [SerializeField] private float _mouseZ = 5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    private void OnMouseDrag()
    {
        Vector3 mousepos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, _mouseZ);
        transform.position = Camera.main.ScreenToWorldPoint(mousepos);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
