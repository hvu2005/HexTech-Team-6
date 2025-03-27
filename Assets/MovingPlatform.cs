using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform pointA;  // Vị trí bắt đầu
    public Transform pointB;  // Vị trí đích
    public float speed = 2f;  // Tốc độ di chuyển
    public int requiredPlayers = 4;  // Số người chơi tối thiểu

    private int currentPlayers = 0;  // Đếm số người chơi trên bệ
    private bool shouldMove = false;  // Kiểm tra điều kiện di chuyển

    private void Update()
    {
        if (shouldMove)
        {
            // Nếu đủ người chơi, di chuyển đến B
            transform.position = Vector2.MoveTowards(transform.position, pointB.position, speed * Time.deltaTime);
        }
        else
        {
            // Nếu chưa đủ, quay về A
            transform.position = Vector2.MoveTowards(transform.position, pointA.position, speed * Time.deltaTime);
        }
    }

    // Hàm để tăng số người chơi
    public void PlayerEntered()
    {
        currentPlayers++;
        CheckMovement();
    }

    // Hàm để giảm số người chơi
    public void PlayerExited()
    {
        currentPlayers--;
        CheckMovement();
    }

    private void CheckMovement()
    {
        if (currentPlayers >= requiredPlayers)
        {
            shouldMove = true;
        }
        else
        {
            shouldMove = false;
        }
    }

}
