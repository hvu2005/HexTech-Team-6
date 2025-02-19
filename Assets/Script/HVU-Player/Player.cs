using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(PlayerController))]
public class Player : NetworkBehaviour
{
    public Rigidbody2D Rb;

    //~~~~~~~~~~~~~Grounding~~~~~~~~~~~~~~
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private Vector2 groundOffset;
    [SerializeField] private float distance;
    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;

    private PlayerController _controller;
    

    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<PlayerController>();
        Rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (!IsServer) return;

        Rb.velocity = new Vector2(_controller.move * moveSpeed, Rb.velocity.y);

        if(checkIsGrounded() && _controller.isJumping)
        {
            Rb.velocity = new Vector2(Rb.velocity.x, jumpForce);
        }
    }

    private bool checkIsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast((Vector2)transform.position + groundOffset, Vector2.left, distance, whatIsGround );
        Debug.DrawRay((Vector2)transform.position + groundOffset, Vector2.left * distance, Color.red);
        return hit.collider != null;
    }
    
}
