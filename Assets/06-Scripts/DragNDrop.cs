using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragNDrop : MonoBehaviour
{
    [Header("Position")]
    [SerializeField] private float _mouseZ = 5f;
    [SerializeField] private Vector3 _initialPos;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    private void OnMouseDrag()
    {
        Vector3 mousepos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, _mouseZ);
        transform.position = Camera.main.ScreenToWorldPoint(mousepos);
    }

    private void OnCollisionEnter(Collision collision)
    {
        transform.position = new Vector3(_initialPos.x, _initialPos.y, _initialPos.z);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
