using UnityEngine;

public class TrapTrigger : MonoBehaviour
{
    public FallTrap trap;

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
