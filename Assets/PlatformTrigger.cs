using UnityEngine;

public class PlatformTrigger : MonoBehaviour
{
    public MovingPlatform platform; // Tham chiếu đến MovingPlatform

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            platform.PlayerEntered();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            platform.PlayerExited();
        }
    }
}
