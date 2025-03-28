using UnityEngine;
using Unity.Netcode;
using UnityEngine.SceneManagement;

public class ReloadSceneManager : NetworkBehaviour
{
    private void Update()
    {
        // Kiểm tra nếu là Host và nhấn phím R
        if (IsHost && Input.GetKeyDown(KeyCode.R))
        {
            ReloadGameServerRpc();
        }
    }

    // ServerRpc: Chỉ gọi từ client nhưng chạy trên server (host)
    [ServerRpc(RequireOwnership = false)]
    private void ReloadGameServerRpc()
    {
        if (!IsHost) return; // Chỉ Host thực thi lệnh này

        string currentSceneName = SceneManager.GetActiveScene().name;
        NetworkManager.SceneManager.LoadScene(currentSceneName, LoadSceneMode.Single);
    }
}
