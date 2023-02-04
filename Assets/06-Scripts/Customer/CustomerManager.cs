using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    [Space(5), Header("SPAWN"), Space(5)]
    [SerializeField] private Transform[] _pointsOfSpawn = new Transform[0];
    [SerializeField] private bool[] _spawnIsAvailible = new bool[0];

    [Header("COOLDOWN")]
    [SerializeField] private Timer _timerSpawn;

    [Space(5), Header("PREFABS"), Space(5)]
    [SerializeField] private GameObject[] _prefabsCustomers = new GameObject[0];
    [SerializeField] private List<GameObject[]> _transformsCustomers = new List<GameObject[]>();

    private void Start()
    {
        GenerateCustomers();
    }

    private void Update()
    {
        // Spawn customer after intervale
        if (_timerSpawn.Update())
        {
            ActivateCustomer();
        }
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        // Set number of bool from spawn on point of spawn list
      if (_pointsOfSpawn.Length != _spawnIsAvailible.Length) { _spawnIsAvailible = new bool[_pointsOfSpawn.Length]; }  
    }
#endif

    /// <summary>
    /// Active a random customer
    /// </summary>
    private void ActivateCustomer()
    {
        // Get customer
        int indexCustomer = Random.Range(0, _prefabsCustomers.Length);

        GameObject actualCustomer = GetInactiveCustomer(indexCustomer);

        if (actualCustomer == null) { return; }

        // Get availible spawn of customer
        List<Transform> availibleSpawn = new List<Transform>();

        for (int i = 0; i < _pointsOfSpawn.Length; i++)
        {
            if (i < _spawnIsAvailible.Length && _pointsOfSpawn[i] != null)
            {
                if (_spawnIsAvailible[i])
                {
                    availibleSpawn.Add(_pointsOfSpawn[i]);
                }
            }
        }

        // Teleport Customer on point of spawn

        actualCustomer.transform.position = availibleSpawn[Random.Range(0, availibleSpawn.Count)].position;
        actualCustomer.SetActive(true);

    }

    /// <summary>
    /// Generate customers on gameobject 
    /// </summary>
    private void GenerateCustomers()
    {
        _transformsCustomers = new List<GameObject[]>();

        _spawnIsAvailible = new bool[_pointsOfSpawn.Length];
        for (int i = 0; i < _spawnIsAvailible.Length; i++)
        {
            _spawnIsAvailible[i] = true;
        }

        // Create the number of list from number of prefabs
        for (int i = 0; i < _prefabsCustomers.Length; i++)
        {
            // Initialise tab from number of spawn point
            _transformsCustomers.Add(new GameObject[_pointsOfSpawn.Length]);

            for (int j = 0; j < _pointsOfSpawn.Length; j++)
            {
                _transformsCustomers[i][j] = Instantiate(_prefabsCustomers[i], transform.position, transform.rotation, transform);
                _transformsCustomers[i][j].SetActive(false);
            }
        }
    }

    /// <summary>
    /// Get active customer from index of variant
    /// </summary>
    /// <param name="index">index of prefabs</param>
    /// <returns></returns>
    private GameObject GetInactiveCustomer(int index)
    {
        foreach (var customer in _transformsCustomers[index])
        {
            if (customer.gameObject.activeSelf)
            {
                return customer;
            }
        }

        return null;
    }

}
