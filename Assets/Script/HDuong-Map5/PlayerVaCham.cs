using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    private GameObject[] list1;
    private GameObject[] list2;
    private void Start()
    {
        list1 = GameObject.FindGameObjectsWithTag("BlockDoor1");
        list2 = GameObject.FindGameObjectsWithTag("BlockDoor2");
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Key1")
        {
            foreach (GameObject go in list1)
            {
                go.SetActive(false);
            }
        }
        if (collision.gameObject.tag == "Key2")
        {
            foreach (GameObject go in list2)
            {
                go.SetActive(false);
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Spine"))
        {
            Debug.Log("Dead");
        }
    }
}
