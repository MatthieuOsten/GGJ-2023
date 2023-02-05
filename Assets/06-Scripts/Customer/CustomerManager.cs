using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class CustomerManager : MonoBehaviour
{
    [Space(5), Header("SPAWN"), Space(5)]
    [SerializeField] private SpawnPoint[] _pointsOfSpawn = new SpawnPoint[0];

    [Header("COOLDOWN")]
    [SerializeField] private Timer _timerSpawn;

    [Space(5), Header("PREFABS"), Space(5)]
    [SerializeField] private GameObject[] _prefabsCustomers = new GameObject[0];
    [SerializeField] private List<GameObject[]> _transformsCustomers = new List<GameObject[]>();

    [SerializeField] private List<Product> _products = new List<Product>();
    private int _nbOfImages = 3;
    [SerializeField] private GameObject _imagePrefab;

    [System.Serializable]
    private struct SpawnPoint
    {
        public Transform point;
        public bool isAvaible;

        public void SetAvaible(bool value)
        {
            isAvaible= value;
        }
    }

    private void Start()
    {
        GenerateCustomers();
        _timerSpawn.Start();
    }

    private void Update()
    {

        // Spawn customer after intervale
        if (_timerSpawn.Update())
        {
            ActivateCustomer();
            _timerSpawn.Restart();
        }
    }

    /// <summary>
    /// Active a random customer
    /// </summary>
    public void ActivateCustomer()
    {
        // Get customer
        int indexCustomer = Random.Range(0, _prefabsCustomers.Length);

        GameObject actualCustomer = GetInactiveCustomer(indexCustomer);

        if (actualCustomer == null) {
            Debug.LogWarning("Customer is null");
            return; 
        }

        // Get availible spawn of customer
        for (int i = 0; i < 5; i++)
        {
            if (_pointsOfSpawn[i].isAvaible)
            {
                actualCustomer.transform.position = _pointsOfSpawn[i].point.position;
                actualCustomer.SetActive(true);

                _pointsOfSpawn[i].isAvaible = false;
                List<string> _productGenerated = new List<string>();
                List<Sprite> _images = new List<Sprite>();
                for (int j = 0; j < UnityEngine.Random.Range(1, 4); j++)
                {
                    int randNb = UnityEngine.Random.Range(0, _products.Count);
                    _productGenerated.Add(_products[randNb].name);
                    _images.Add(_products[randNb].sprite);
                }

                actualCustomer.GetComponent<Client>()._productGenerated = _productGenerated;
                actualCustomer.GetComponent<Client>()._images = _images;
                break;
            }
        }
    }

    /// <summary>
    /// Generate customers on gameobject 
    /// </summary>
    private void GenerateCustomers()
    {
        _transformsCustomers = new List<GameObject[]>();
        Image _temp2 = new GameObject().AddComponent<Image>();

        // Create the number of list from number of prefabs
        for (int i = 0; i < _prefabsCustomers.Length; i++)
        {
            // Initialise tab from number of spawn point
            _transformsCustomers.Add(new GameObject[_pointsOfSpawn.Length]);

            for (int j = 0; j < _pointsOfSpawn.Length; j++)
            {
                _transformsCustomers[i][j] = Instantiate(_prefabsCustomers[i], transform.position, transform.rotation, transform);
                _transformsCustomers[i][j].SetActive(false);

                Client actualClient = null;

                if (_transformsCustomers[i][j].TryGetComponent(out actualClient))
                {
                    List<Image> _image = new List<Image>();
                    for (int k = 0; k != _nbOfImages; k++)
                    {
                        Debug.Log("ETTD");
                        Image _temp = Instantiate(_imagePrefab,actualClient.Panel).GetComponent<Image>();
                        _image.Add(_temp);
                        _temp.gameObject.SetActive(false);
                    }
                }
            }
        }
        Destroy(_temp2);
    }

    /// <summary>
    /// Get active customer from index of variant
    /// </summary>
    /// <param name="index">index of prefabs</param>
    /// <returns>If null not have disponible customer</returns>
    private GameObject GetInactiveCustomer(int index)
    {
        foreach (var customer in _transformsCustomers[index])
        {
            if (!customer.gameObject.activeSelf)
            {
                return customer;
            }
        }

        return null;
    }

}
