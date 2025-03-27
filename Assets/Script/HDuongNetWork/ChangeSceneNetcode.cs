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
            DontDestroyOnLoad(gameObject); // Giữ lại qua các scene
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ChangeScene(string sceneName)
    {
        if (!IsServer && !IsHost)
        {
            Debug.LogError("Không phải Server and host! Không thể chuyển Scene.");
            return;
        }

        Debug.Log("Bắt đầu chuyển Scene: " + sceneName);
        NetworkManager.SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }
}
