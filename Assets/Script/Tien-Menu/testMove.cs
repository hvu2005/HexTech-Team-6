using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 7f;
    private Rigidbody2D rb;
    private Vector2 movement;
    private bool isGrounded;

    [SerializeField] private float coyoteTime = 0.2f; // Thời gian coyote cho phép nhảy sau khi rời khỏi mặt đất
    private float coyoteTimeCounter;
    [SerializeField] private float jumpBufferTime = 0.2f; // Thời gian buffer cho phép nhảy ngay khi nhấn Space trước khi chạm đất
    private float jumpBufferCounter;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Lấy input từ bàn phím
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.Normalize(); // Để di chuyển không nhanh hơn khi đi chéo

        // Coyote Time - Cho phép nhảy ngay sau khi rời mặt đất
        if (isGrounded)
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        // Jump Buffer - Giúp nhảy mượt hơn nếu nhấn Space sớm
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpBufferCounter = jumpBufferTime;
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }

        // Nhảy
        if (jumpBufferCounter > 0 && coyoteTimeCounter > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            isGrounded = false;
            jumpBufferCounter = 0;
        }
    }

    void FixedUpdate()
    {
        // Di chuyển nhân vật
        rb.velocity = new Vector2(movement.x * moveSpeed, rb.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Kiểm tra nếu nhân vật đang chạm đất
        if (collision.contacts[0].normal.y > 0.5f)
        {
            isGrounded = true;
        }
    }
}