using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerControl : NetworkBehaviour
{
    [SerializeField] private float moveSpeed = 2.5f;
    [SerializeField] private float jumpForce = 15f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheck;

    private Animator animator;
    private Rigidbody2D rb;
    private Vector3 lastPosition;
    private float lastScaleX;
    public bool isGrounded = false;

    // 🟢 Dùng NetworkVariable để đồng bộ vị trí, hướng xoay và animation
    private NetworkVariable<Vector3> networkPosition = new NetworkVariable<Vector3>();
    private NetworkVariable<float> networkScaleX = new NetworkVariable<float>(1);
    private NetworkVariable<bool> networkIsRunning = new NetworkVariable<bool>();
    private NetworkVariable<bool> networkIsJumping = new NetworkVariable<bool>();

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    public override void OnNetworkSpawn()
    {
        if (!IsOwner) return;

        rb.isKinematic = false;
        lastPosition = transform.position;
        lastScaleX = transform.localScale.x;
    }

    void Update()
    {
        if (IsOwner)
        {
            HandleMovement();
            HandleJump();
            UpdateAnimation();

            // 🟢 Chỉ gửi dữ liệu khi có thay đổi lớn
            if (Vector3.Distance(lastPosition, transform.position) > 0.05f || lastScaleX != transform.localScale.x)
            {
                SendStateToServerRpc(transform.position, transform.localScale.x, Mathf.Abs(rb.velocity.x) > 0.1f, !isGrounded);
                lastPosition = transform.position;
                lastScaleX = transform.localScale.x;
            }
        }
        else
        {
            // 🟢 Cập nhật dữ liệu từ NetworkVariable cho các Client khác
            transform.position = networkPosition.Value;
            transform.localScale = new Vector3(networkScaleX.Value, 1, 1);
            animator.SetBool("isRunning", networkIsRunning.Value);
            animator.SetBool("isJumping", networkIsJumping.Value);
        }
    }

    private void HandleMovement()
    {
        float moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        if (moveInput > 0) transform.localScale = new Vector3(1, 1, 1);
        else if (moveInput < 0) transform.localScale = new Vector3(-1, 1, 1);
    }

    private void HandleJump()
    {
        // 🟢 Cập nhật isGrounded trước khi nhảy
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            isGrounded = false; // Ngăn double jump ngay lập tức
        }
    }


    private void UpdateAnimation()
    {
        animator.SetBool("isRunning", Mathf.Abs(rb.velocity.x) > 0.1f);
        animator.SetBool("isJumping", !isGrounded);
    }

    // 🟢 Client gửi trạng thái lên Server (vị trí, hướng xoay, animation)
    [ServerRpc]
    private void SendStateToServerRpc(Vector3 newPosition, float scaleX, bool isRunning, bool isJumping, ServerRpcParams rpcParams = default)
    {
        networkPosition.Value = newPosition;
        networkScaleX.Value = scaleX;
        networkIsRunning.Value = isRunning;
        networkIsJumping.Value = isJumping;
    }
}
