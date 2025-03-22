using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(PlayerController))]
public class Player : NetworkBehaviour
{
    public Rigidbody2D Rb;

    public bool CanMove { get; set; } = true;
    public bool isFacingRight { get; private set; } = true;

    //~~~~~~~~~~~~~Grounding~~~~~~~~~~~~~~
    public bool isGrounded {  get; private set; }
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
        if (!IsOwner) return;


        Flip();
        checkIsGrounded();

        Rb.velocity = new Vector2(_controller.xMove * moveSpeed, Rb.velocity.y);

        if(isGrounded && _controller.isJumping)
        {
            Rb.velocity = new Vector2(Rb.velocity.x, jumpForce);
        }
    }

    private void checkIsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast((Vector2)transform.position + groundOffset, Vector2.left, distance, whatIsGround );
        Debug.DrawRay((Vector2)transform.position + groundOffset, Vector2.left * distance, Color.red);
        isGrounded = hit.collider != null;
    }
    
    private void Flip()
    {
        if(CanMove)
        {
            if(_controller.xMove > 0 && !isFacingRight)
            {
                isFacingRight = !isFacingRight;
                transform.rotation = Quaternion.identity;
            }
            else if(_controller.xMove < 0 && isFacingRight)
            {
                isFacingRight = !isFacingRight;
                transform.rotation = Quaternion.Euler(0, -180, 0);
            }
        }
    }
}
