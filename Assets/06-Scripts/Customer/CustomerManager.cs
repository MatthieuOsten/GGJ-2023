using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
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
    
    [SerializeField] private GameObject[] _bagList = new GameObject[0];

    [Space(5), Header("EVENTS"), Space(5)]
    [SerializeField] private UnityEvent eventSpawnCustomer;

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
        AudioManager.Instance.Play("Ambiance_Forest");
        AudioManager.Instance.Play("InGameMusic_Choix1");
        GenerateCustomers();
        _timerSpawn.Start();

        foreach (var point in _pointsOfSpawn)
        {
            point.SetAvaible(true);
        }
    }

    private void Update()
    {

        // Spawn customer after intervale
        if (_timerSpawn.Update())
        {
            ActivateCustomer();
            _timerSpawn.Restart();
        }

        for (int i = 0; i < _pointsOfSpawn.Length; i++)
        {
            int temp = 0;
            for (int j = 0; j < _prefabsCustomers.Length; j++)
            {
                foreach (var customer in _transformsCustomers[j])
                {
                    if (customer.transform.position.z == _pointsOfSpawn[i].point.position.z && customer.gameObject.activeSelf)
                    {
                        ActivateBag(i,true);
                        temp++;
                    }
                }
            }
            if (temp == 0)
            {
                ActivateBag(i, false);
                _pointsOfSpawn[i].isAvaible = true;
            }
        }

        CheckInBag();

    }

    private void ActivateBag(int index, bool actived)
    {
        if (index < _bagList.Length)
        {
            _bagList[index].gameObject.SetActive(actived);
        }
    }


    private void CheckInBag()
    {
        for (int i = 0; i < _pointsOfSpawn.Length; i++)
        {
            for (int j = 0; j < _prefabsCustomers.Length; j++)
            {
                foreach (var customer in _transformsCustomers[j])
                {
                    if (customer.gameObject.activeSelf && customer.transform.position.z == _pointsOfSpawn[i].point.position.z)
                    {
                        if (i >= _bagList.Length)
                        {
                            Debug.Log(i + " is to big ... Size of _bagList not egals of _pointsOfSpawn");
                            continue;
                        }

                        Client theCustomer = customer.GetComponent<Client>();

                        if (theCustomer != null)
                        {
                            ContentBag bag = theCustomer.GetComponent<ContentBag>();

                            customer.GetComponent<Client>()._inTheBag = _bagList[i].GetComponent<ContentBag>()._productsList;
                            customer.GetComponent<Client>()._bag = _bagList[i];
                            Debug.Log("HERE");
                            Debug.Log(theCustomer._inTheBag.Count);
                        }


                    }
                }
            }
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
        for (int i = 0; i < _pointsOfSpawn.Length; i++)
        {
            if (_pointsOfSpawn[i].isAvaible)
            {

                eventSpawnCustomer.Invoke();
                actualCustomer.transform.position = _pointsOfSpawn[i].point.position;
                actualCustomer.SetActive(true);

                _pointsOfSpawn[i].isAvaible = false;
                List<string> _productGenerated = new List<string>();
                List<Sprite> _images = new List<Sprite>();
                for (int j = 0; j < UnityEngine.Random.Range(1, 4); j++)
                {
                    int randNb = UnityEngine.Random.Range(0, _products.Count);
                    if (randNb >= _products.Count) { continue; }
                    _productGenerated.Add(_products[randNb].name);
                    _images.Add(_products[randNb].sprite);
                }
                Client client = null;

                if (actualCustomer.TryGetComponent<Client>(out client))
                {
                    actualCustomer.GetComponent<Client>()._productGenerated = _productGenerated;
                    actualCustomer.GetComponent<Client>()._images = _images;
                }
                
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
                _transformsCustomers[i][j] = Instantiate(_prefabsCustomers[i], transform);
                _transformsCustomers[i][j].SetActive(false);

                Client actualClient = null;

                if (_transformsCustomers[i][j].TryGetComponent(out actualClient))
                {
                    List<Image> _image = new List<Image>();
                    for (int k = 0; k != _nbOfImages; k++)
                    {
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
