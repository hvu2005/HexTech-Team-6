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
    //public bool isFacingRight { get; private set; } = true;

    //~~~~~~~~~~~~~Grounding~~~~~~~~~~~~~~
    public bool isGrounded {  get; private set; }
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private Vector2 groundOffset;
    [SerializeField] private float distance;
    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;

    private PlayerController _controller;

    public NetworkVariable<bool> isFacingRight = new NetworkVariable<bool>(true,
       NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

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

        Vector2 newVelocity = (_controller.move != 0) ?
            new Vector2(_controller.move * moveSpeed, Rb.velocity.y) : Vector2.zero;

        MoveServerRpc(newVelocity);
    }

    [ServerRpc]
    private void MoveServerRpc(Vector2 velocity)
    {
        Rb.velocity = velocity;
    }

    private void checkIsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast((Vector2)transform.position + groundOffset, Vector2.down, distance, whatIsGround);
        Debug.DrawRay((Vector2)transform.position + groundOffset, Vector2.down * distance, Color.red);
        isGrounded = hit.collider != null;
    }

    private void Flip()
    {
        if(CanMove)
        {
            if(_controller.move > 0 && !isFacingRight.Value)
            {
                isFacingRight.Value = !isFacingRight.Value;
                //transform.rotation = Quaternion.identity;
                FlipServerRpc(isFacingRight.Value);

            }
            else if(_controller.move < 0 && isFacingRight.Value)
            {
                isFacingRight.Value = !isFacingRight.Value;
                //transform.rotation = Quaternion.Euler(0, -180, 0);
                FlipServerRpc(isFacingRight.Value);

            }
        }
    }

    [ServerRpc]
    private void FlipServerRpc(bool facingRight)
    {
        FlipClientRpc(facingRight);
    }

    [ClientRpc]
    private void FlipClientRpc(bool facingRight)
    {
        transform.rotation = facingRight ? Quaternion.identity : Quaternion.Euler(0, 180, 0);
    }
}
