using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;

    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Animator anim;
    private bool isFacingRight = true; 

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        HandleMovement();
        HandleAnimation();
    }

    void HandleMovement()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        moveInput = new Vector2(moveX, moveY).normalized;

        rb.velocity = moveInput * speed;

        
        if (moveX > 0 && !isFacingRight)
            Flip();
        else if (moveX < 0 && isFacingRight)
            Flip();
    }

    void HandleAnimation()
    {
        anim.SetFloat("Move", moveInput.magnitude);
    }

    void Flip()
    {
        isFacingRight = !isFacingRight; 
        Vector3 scale = transform.localScale;
        scale.x *= -1; 
        transform.localScale = scale;
    }
}
