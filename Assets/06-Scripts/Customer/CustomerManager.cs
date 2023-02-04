using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    [Space(5), Header("SPAWN"), Space(5)]
    [SerializeField] private SpawnPoint[] _pointsOfSpawn = new SpawnPoint[0];

    [Header("COOLDOWN")]
    [SerializeField] private Timer _timerSpawn;

    [Space(5), Header("PREFABS"), Space(5)]
    [SerializeField] private GameObject[] _prefabsCustomers = new GameObject[0];
    [SerializeField] private List<GameObject[]> _transformsCustomers = new List<GameObject[]>();

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
        _timerSpawn.Start();
        GenerateCustomers();
        ActivateCustomer();
    }

    private void Update()
    {

        // Spawn customer after intervale
        if (_timerSpawn.Update())
        {
            ActivateCustomer();
        }
    }

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

        foreach (var spawn in _pointsOfSpawn)
        {
            if (spawn.isAvaible)
            {
                // Teleport Customer on point of spawn
                actualCustomer.transform.position = spawn.point.position;
                actualCustomer.SetActive(true);

                spawn.SetAvaible(false);

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
