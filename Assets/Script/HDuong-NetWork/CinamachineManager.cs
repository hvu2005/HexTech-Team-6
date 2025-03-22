using Unity.Collections;
using Unity.Netcode;
using UnityEngine;
using Cinemachine;
using System.Collections;
using System.Collections.Generic;

public class CinemachineManager : NetworkBehaviour
{
    [SerializeField] private CinemachineTargetGroup targetGroup;
    private Dictionary<ulong, Transform> clientTargets = new Dictionary<ulong, Transform>();

    private IEnumerator WaitAndAddPlayer(ulong clientId)
    {
        while (!NetworkManager.Singleton.ConnectedClients.ContainsKey(clientId) ||
               NetworkManager.Singleton.ConnectedClients[clientId].PlayerObject == null)
        {
            yield return null;
        }

        GameObject player = NetworkManager.Singleton.ConnectedClients[clientId].PlayerObject.gameObject;
        Transform playerTransform = player.transform;

        if (clientTargets.ContainsKey(clientId)) yield break;
        clientTargets[clientId] = playerTransform;

        if (IsServer)
        {
            List<Vector3> playerPositions = GetAllPlayerPositions();
            UpdateClientsTargetGroupClientRpc(ConvertToFixedList(playerPositions));
        }
    }

    public override void OnNetworkSpawn()
    {
        if (IsServer)
        {
            NetworkManager.Singleton.OnClientConnectedCallback += AddClientToCamera;
            NetworkManager.Singleton.OnClientDisconnectCallback += RemoveClientFromCamera;
        }
    }

    public override void OnNetworkDespawn()
    {
        if (IsServer)
        {
            NetworkManager.Singleton.OnClientConnectedCallback -= AddClientToCamera;
            NetworkManager.Singleton.OnClientDisconnectCallback -= RemoveClientFromCamera;
        }
    }

    private void AddClientToCamera(ulong clientId)
    {
        if (clientTargets.ContainsKey(clientId)) return;
        StartCoroutine(WaitAndAddPlayer(clientId));
    }

    private void RemoveClientFromCamera(ulong clientId)
    {
        if (clientTargets.ContainsKey(clientId))
        {
            clientTargets.Remove(clientId);

            if (IsServer)
            {
                List<Vector3> playerPositions = GetAllPlayerPositions();
                UpdateClientsTargetGroupClientRpc(ConvertToFixedList(playerPositions));
            }
        }
    }

    // 🔥 Chuyển List<Vector3> thành FixedList
    private FixedList512Bytes<Vector3> ConvertToFixedList(List<Vector3> positions)
    {
        FixedList512Bytes<Vector3> fixedList = new FixedList512Bytes<Vector3>();
        foreach (var pos in positions)
        {
            if (fixedList.Length < fixedList.Capacity)
                fixedList.Add(pos);
        }
        return fixedList;
    }

    // 🎯 Gửi danh sách positions bằng FixedList
    [ClientRpc]
    private void UpdateClientsTargetGroupClientRpc(FixedList512Bytes<Vector3> playerPositions)
    {
        if (!IsClient) return;

        targetGroup.m_Targets = new CinemachineTargetGroup.Target[playerPositions.Length];

        for (int i = 0; i < playerPositions.Length; i++)
        {
            GameObject newTarget = new GameObject($"PlayerTarget_{i}");
            newTarget.transform.position = playerPositions[i];

            targetGroup.m_Targets[i] = new CinemachineTargetGroup.Target
            {
                target = newTarget.transform,
                weight = 1f,
                radius = 4f
            };
        }

        Debug.Log("🔄 Client đã cập nhật targetGroup!");
    }

    private List<Vector3> GetAllPlayerPositions()
    {
        List<Vector3> positions = new List<Vector3>();
        foreach (var kvp in clientTargets)
        {
            positions.Add(kvp.Value.position);
        }
        return positions;
    }
}
