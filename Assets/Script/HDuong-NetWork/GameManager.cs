using UnityEngine;
using System.Collections.Generic;
using Unity.Netcode;

public class GameManager : NetworkBehaviour
{
    public static GameManager Instance;
    private List<Transform> players = new List<Transform>();

    void Awake()
    {
        Instance = this;
    }

    public void RegisterPlayer(Transform player)
    {
        if (!players.Contains(player))
        {
            players.Add(player);
            Debug.Log("Đã đăng ký Player: " + player.name);
        }
    }

    public void UnregisterPlayer(Transform player)
    {
        if (players.Contains(player))
        {
            players.Remove(player);
            Debug.Log("Đã xóa Player: " + player.name);
        }
    }

    public List<Transform> GetPlayers()
    {
        List<Transform> players = new List<Transform>();

        foreach (var player in FindObjectsOfType<PlayerController>())  // Tìm tất cả Player đã spawn
        {
            players.Add(player.transform);
        }

        return players;
    }

}
