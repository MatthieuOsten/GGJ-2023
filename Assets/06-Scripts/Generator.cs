using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    [SerializeField] private GameObject _gameObject;
    [SerializeField] private int _nbOfGameObject = 10;
    [SerializeField] private Vector3 _spawnPoint;
    private GameObject[] _allGameObject = new GameObject[0];
    public bool isDragged;

    [SerializeField] private float _positionDrag = 5f;

#if UNITY_EDITOR

    private void OnValidate()
    {
        foreach (var item in _allGameObject)
        {
            DragNDrop drag = null;
            if (item.TryGetComponent<DragNDrop>(out drag))
            {
                drag._mouseZ = _positionDrag;
            }
        }
    }

#endif

    // Start is called before the first frame update
    void Start()
    {
        isDragged = true;
        _allGameObject = new GameObject[_nbOfGameObject];
        
        for (int i = 0; i != _nbOfGameObject; i++)
        {
            _allGameObject[i] = Instantiate(_gameObject,Vector3.zero,Quaternion.identity,transform).gameObject;
            _allGameObject[i].SetActive(false);

            DragNDrop drag = null;
            if (_allGameObject[i].TryGetComponent<DragNDrop>(out drag))
            {
                drag._mouseZ = _positionDrag;
            }
            
        }

        SpawnNewProduct();
    }
    
    // Update is called once per frame
    void Update()
    {
        int temp = 0;
        
        for (int i = 0; i != _nbOfGameObject; i++)
        {
            if (_allGameObject[i].activeSelf)
            {
                temp += 1;
            }

            if (temp == 0)
            {
                SpawnNewProduct();
            }
        }
    }

    private void SpawnNewProduct()
    {
        _allGameObject[0].transform.localPosition = Vector3.zero;
        _allGameObject[0].SetActive(true);
    }
}
