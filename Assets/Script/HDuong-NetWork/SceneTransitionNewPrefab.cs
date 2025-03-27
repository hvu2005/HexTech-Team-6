using System.Collections;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : NetworkBehaviour
{
    public static SceneTransitionManager Instance;

    public GameObject newPlayerPrefab; // Gán Prefab mới trong Inspector

    private void Awake()
    { 
        if (Instance == null) Instance = this;
    }

    public void ChangeScene(string sceneName)
    {
        if (IsServer)
        {
            foreach (var player in FindObjectsOfType<PlayerControl>())
            {
                player.GetComponent<NetworkObject>().Despawn(false); // Despawn nhân vật cũ
            }

            NetworkManager.SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        }
    }

    public override void OnNetworkSpawn()
    {
        if (IsServer)
        {
            StartCoroutine(SpawnNewPlayer());
        }
    }

    private IEnumerator SpawnNewPlayer()
    {
        yield return new WaitForSeconds(1f); // Chờ scene load xong

        foreach (var clientId in NetworkManager.ConnectedClientsIds)
        {
            GameObject newPlayer = Instantiate(newPlayerPrefab);
            newPlayer.GetComponent<NetworkObject>().SpawnAsPlayerObject(clientId);
        }
    }
}
