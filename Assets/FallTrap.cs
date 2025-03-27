using UnityEngine;

public class FallTrap : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private bool hasFallen = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        rb.bodyType = RigidbodyType2D.Kinematic; // Để bẫy không rơi ngay
        spriteRenderer.enabled = false; // Ẩn bẫy ngay từ đầu

        Debug.Log("FallTrap Initialized - Hidden");
    }

    public void DropTrap()
    {
        if (!hasFallen)
        {
            spriteRenderer.enabled = true; // Hiện bẫy ra
            rb.bodyType = RigidbodyType2D.Dynamic; // Cho phép rơi xuống
            rb.gravityScale = 2f;
            hasFallen = true;

            Debug.Log("Trap Activated - Falling!");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (hasFallen)
        {
            rb.bodyType = RigidbodyType2D.Static; // Khi chạm đất thì đứng yên
            Debug.Log("Trap Landed - Now Static");
        }
    }
}
