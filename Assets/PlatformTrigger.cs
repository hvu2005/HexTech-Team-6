using UnityEngine;

public class PlatformTrigger : MonoBehaviour
{
    public MovingPlatform platform;  // Tham chiếu đến MovingPlatform

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Trigger Entered by: " + collision.name + " | Tag: " + collision.tag);

        if (collision.CompareTag("Player"))
        {
            platform.PlayerEntered();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("Trigger Exited by: " + collision.name + " | Tag: " + collision.tag);

        if (collision.CompareTag("Player"))
        {
            platform.PlayerExited();
        }
    }
}
