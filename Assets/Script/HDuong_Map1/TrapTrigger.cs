using UnityEngine;

public class TrapTrigger : MonoBehaviour
{
    public FallTrap trap;
    private PlayerControl playerController;
    private void Start()
    {
        playerController = FindAnyObjectByType<PlayerControl>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player entered trigger - Activating Trap!");
            trap.DropTrap();
            Destroy(gameObject); // Xóa vùng kích hoạt sau khi bẫy đã rơi
        }
    }
}