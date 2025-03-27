using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingMachine : MonoBehaviour
{
    GameObject player;
    [SerializeField] GameObject active;
    [SerializeField] GameObject deactive;
    bool isFly;
    [SerializeField] float pushForce;
    // Start is called before the first frame update
    void Awake()
    {
        isFly = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isFly)
        {
            player.GetComponent<Rigidbody2D>().AddForce(new Vector2(0,pushForce), ForceMode2D.Impulse);
        }
        active.SetActive(isFly);
        deactive.SetActive(!isFly);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isFly = true;
            player = other.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isFly = false;
            player = null;
        }
    }
}
