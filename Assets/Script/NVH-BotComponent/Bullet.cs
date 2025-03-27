using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage = 10f;
    public float lifeTime = 5f;
    public float speed;
    public Vector2 dir;

    public void setSpeed(float speed)
    {
        this.speed = speed;
    }
        
    public void setDir(Vector2 dir)
    {
        this.dir = dir;
    }
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
        }
        if (other.CompareTag("Ship") && !this.CompareTag("OfBot"))
        {
            Destroy(gameObject);
            //do sth
        }
    }
}
