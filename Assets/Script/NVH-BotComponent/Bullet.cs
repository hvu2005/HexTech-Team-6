using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //public float damage;
    public float lifeTime = 5f;
    public float speed;
    public Vector2 dir;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(dir * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
            //do sth
        }
        if (other.CompareTag("Shield"))
        {
            Destroy(gameObject);
            //do sth
        }
    }
}
