using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class SetActiveMayBay : NetworkBehaviour
{
    public static SetActiveMayBay Instance;
    private Dictionary<ulong, Vector3> originalPositions = new Dictionary<ulong, Vector3>(); // Lưu vị trí cũ của mỗi player

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    public void Disappear()
    {
        if (IsServer)
        {
            MovePlayersAwayClientRpc();
        }
    }

    public void Reappear()
    {
        if (IsServer)
        {
            MovePlayersBackClientRpc();
        }
    }

    [ClientRpc]
    private void MovePlayersAwayClientRpc()
    {
        PlayerController player = GetComponent<PlayerController>();
        if (player != null)
        {
            if (!originalPositions.ContainsKey(NetworkManager.Singleton.LocalClientId))
            {
                originalPositions[NetworkManager.Singleton.LocalClientId] = transform.position; // Lưu vị trí cũ
            }
            transform.position = new Vector3(9999, 9999, 9999); // Di chuyển ra xa
        }
    }

    [ClientRpc]
    private void MovePlayersBackClientRpc()
    {
        if (originalPositions.TryGetValue(NetworkManager.Singleton.LocalClientId, out Vector3 originalPos))
        {
            transform.position = originalPos; // Đưa về vị trí cũ
        }
    }
}
