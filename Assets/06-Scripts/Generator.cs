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

    // Start is called before the first frame update
    void Start()
    {
        isDragged = true;
        _allGameObject = new GameObject[_nbOfGameObject];
        
        for (int i = 0; i != _nbOfGameObject; i++)
        {
            _allGameObject[i] = Instantiate(_gameObject).gameObject;
            _allGameObject[i].SetActive(false);
        }
        _allGameObject[0].SetActive(true);
        _allGameObject[0].transform.position = _spawnPoint;
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
                _allGameObject[0].SetActive(true);
                _allGameObject[0].transform.position = _spawnPoint;
            }
        }
    }
}
