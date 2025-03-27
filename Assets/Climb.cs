using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClimb : MonoBehaviour
{
    Rigidbody2D rb;
    float defaultGravityScale;

    float vertical;
    bool canClimb;
    [SerializeField] float climbSpeed;
    // Start is called before the first frame update
    void Awake()
    {
        canClimb = false;
        rb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        defaultGravityScale = rb.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        Climb();
        //rb.AddForce(Vector2.up, ForceMode2D.Impulse);
    }

    void Climb()
    {
        if (canClimb)
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                vertical = Input.GetAxis("Vertical");
                rb.gravityScale = 0;
                rb.velocity = new Vector2(rb.velocity.x, vertical * climbSpeed);
                //Instantiate()
            }
        }
        else
        {
            rb.gravityScale = defaultGravityScale;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other != null && other.CompareTag(rb.gameObject.tag))
        {
            canClimb = true;
            //Debug.Log("CanClimb: "+ canClimb);
        }
    }



    private void OnTriggerExit2D(Collider2D other)
    {
        if (other != null && other.CompareTag(rb.gameObject.tag))
        {
            canClimb = false;
            //Debug.Log("CanClimb: " + canClimb);
        }
    }
}
