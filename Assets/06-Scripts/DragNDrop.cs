using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragNDrop : MonoBehaviour
{
    [Header("Position")]
    [SerializeField] public float _mouseZ = 5f;
    // Start is called before the first frame update

    private void OnMouseDrag()
    {
        if (gameObject.activeSelf)
        {
            Vector3 mousepos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, _mouseZ);
            transform.position = Camera.main.ScreenToWorldPoint(mousepos);
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetMouseButtonUp(0) == true)
        {
            if (other.gameObject.CompareTag("Bag"))
            {
                AudioManager.Instance.Play("DragAndDrop_DropPaperBag");
                gameObject.SetActive(false);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            gameObject.SetActive(false);
        }
    }

    private void OnMouseDown()
    {
        AudioManager.Instance.Play("DragAndDrop_Take");
    }

    // Update is called once per frame
    void Update()
    {
    }
}
