using UnityEngine;
using Unity.Netcode;
using UnityEngine.SceneManagement;

public class ChangeSceneNetcode : NetworkBehaviour
{
    public static ChangeSceneNetcode Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ChangeScene(string sceneName)
    {
        if (!IsServer) return; // Chỉ Server/Host mới có quyền đổi scene

        NetworkManager.SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }
}
