using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class ContentBag : MonoBehaviour
{
    private List<string> _productsList = new List<string>();
    // Start is called before the first frame update
    void Start()
    {
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Product"))
        {
            if (Input.GetMouseButtonUp(0))
            {
                string temp = "";
                for (int i = 0; other.gameObject.name[i] != '('; i++)
                {
                    temp += other.gameObject.name[i];
                }
                _productsList.Add(temp);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
