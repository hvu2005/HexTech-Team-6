using UnityEngine;

public class JumpPad : MonoBehaviour
{
    public float jumpForce = 10f; // Lực nhảy có thể điều chỉnh
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }

            // Kích hoạt animation
            if (animator != null)
            {
                animator.SetBool("Hit", true);
                Invoke("ResetAnimation", 0.5f); // Đặt lại sau 0.5s (tuỳ thời gian animation)
            }
        }
    }

    private void ResetAnimation()
    {
        if (animator != null)
        {
            animator.SetBool("Hit", false);
        }
    }
}
