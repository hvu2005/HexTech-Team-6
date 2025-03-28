using UnityEngine;
using UnityEngine.Tilemaps;

public class GroundTrap : MonoBehaviour
{
    public GameObject trap;  // Gán Trap trong Inspector
    private bool isPlayerOn = false; // Kiểm tra người chơi có đang đứng trên block không
    private float timer = 0f;  // Bộ đếm thời gian
    public float delayBeforeFall = 3f;  // Thời gian trước khi block sập
    private Rigidbody2D _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        if (trap != null)
        {
            trap.SetActive(false); // Ẩn trap ban đầu
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerOn = true;
            timer = 0f;  // Reset bộ đếm
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerOn = false;
            timer = 0f;  // Huỷ đếm thời gian nếu người chơi rời đi
        }
    }

    private void Update()
    {
        if (isPlayerOn)
        {
            timer += Time.deltaTime;  // Đếm thời gian người chơi đứng trên block

            if (timer >= delayBeforeFall)
            {
                TriggerTrap();  // Kích hoạt bẫy sau 3 giây
            }
        }
    }

    private void TriggerTrap()
    {
        _rb.bodyType = RigidbodyType2D.Dynamic;
        GetComponent<TilemapCollider2D>().enabled = false;
        if (trap != null)
        {
            trap.SetActive(true); // Hiện Trap
        }
    }
}
