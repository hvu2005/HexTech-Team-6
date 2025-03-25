using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Rigidbody2D rb;
    public int speed;
    float dir;
    // Start is called before the first frame update
    void Start()
    {
        //rb = this.gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        dir = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(dir * speed, rb.velocity.y);
    }
}
