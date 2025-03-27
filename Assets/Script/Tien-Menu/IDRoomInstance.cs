using UnityEngine;
using Unity.Netcode;

public class IDRoomInstance : MonoBehaviour
{
    public static IDRoomInstance Instance { get; private set; }
    public string IdRoom { get; set; } = ""; // Cho phép thay đổi nhưng đảm bảo có giá trị mặc định

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Giữ lại qua các scene
        }
        else
        {
            Destroy(gameObject); // Xóa bản sao nếu đã có một Instance
        }
    }
}
