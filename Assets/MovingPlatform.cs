using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform pointA;  // Vị trí bắt đầu
    public Transform pointB;  // Vị trí đích
    public float speed = 2f;  // Tốc độ di chuyển
    public int requiredPlayers = 2;  // Số người chơi tối thiểu

    private int currentPlayers = 0;  // Đếm số người chơi đang đứng trên bệ
    private bool shouldMove = false;  // Kiểm tra có nên di chuyển không

    private void Start()
    {
        transform.position = pointA.position;  // Đặt bệ ở vị trí A khi bắt đầu game
        currentPlayers = 0;
        shouldMove = false;
    }

    private void Update()
    {
        if (shouldMove)
        {
            transform.position = Vector2.MoveTowards(transform.position, pointB.position, speed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, pointA.position, speed * Time.deltaTime);
        }
    }

    public void PlayerEntered()
    {
        currentPlayers++;
        Debug.Log("Player Entered | currentPlayers: " + currentPlayers);
        CheckMovement();
    }

    public void PlayerExited()
    {
        currentPlayers--;
        Debug.Log("Player Exited | currentPlayers: " + currentPlayers);
        CheckMovement();
    }

    private void CheckMovement()
    {
        shouldMove = currentPlayers >= requiredPlayers;
        Debug.Log("CheckMovement() | currentPlayers: " + currentPlayers + " | requiredPlayers: " + requiredPlayers + " | shouldMove: " + shouldMove);
    }
}
