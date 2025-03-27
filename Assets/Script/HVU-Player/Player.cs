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
        if (!IsServer) return;

        Flip();
        checkIsGrounded();

        // Cập nhật tốc độ di chuyển
        Rb.velocity = new Vector2(_controller.xMove * moveSpeed, Rb.velocity.y);

        // Nhảy
        if (isGrounded && _controller.isJumping)
        {
            Rb.velocity = new Vector2(Rb.velocity.x, jumpForce);
            isGrounded = false; // Ngăn nhảy liên tục
        }
    }


    private void checkIsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast((Vector2)transform.position + groundOffset, Vector2.down, distance, whatIsGround);
        Debug.DrawRay((Vector2)transform.position + groundOffset, Vector2.down * distance, Color.red);
        isGrounded = hit.collider != null;
    }


    private void Flip()
    {
        if (!CanMove) return;

        if (_controller.xMove > 0 && !isFacingRight)
        {
            isFacingRight = true;
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (_controller.xMove < 0 && isFacingRight)
        {
            isFacingRight = false;
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

}
