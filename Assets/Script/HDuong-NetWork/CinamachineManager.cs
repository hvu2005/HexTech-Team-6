using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using Cinemachine;

public class CinemachineManager : NetworkBehaviour
{
    [SerializeField] private CinemachineTargetGroup targetGroup;
    private Dictionary<ulong, Transform> clientTargets = new Dictionary<ulong, Transform>();

    public struct PlayerData : INetworkSerializable
    {
        public List<ulong> ClientIds;

        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            if (serializer.IsWriter)
            {
                serializer.GetFastBufferWriter().WriteValueSafe(ClientIds.Count);
                foreach (var id in ClientIds)
                {
                    serializer.GetFastBufferWriter().WriteValueSafe(id);
                }
            }
            else
            {
                int count;
                serializer.GetFastBufferReader().ReadValueSafe(out count);
                ClientIds = new List<ulong>(count);
                for (int i = 0; i < count; i++)
                {
                    ulong id;
                    serializer.GetFastBufferReader().ReadValueSafe(out id);
                    ClientIds.Add(id);
                }
            }
        }
    }

    public override void OnNetworkSpawn()
    {
        if (IsServer)
        {
            AddClientToCamera(NetworkManager.Singleton.LocalClientId);
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
        StartCoroutine(WaitAndAddPlayer(clientId));
    }

    private System.Collections.IEnumerator WaitAndAddPlayer(ulong clientId)
    {
        while (!NetworkManager.Singleton.ConnectedClients.ContainsKey(clientId) ||
               NetworkManager.Singleton.ConnectedClients[clientId].PlayerObject == null)
        {
            yield return null;
        }

        GameObject player = NetworkManager.Singleton.ConnectedClients[clientId].PlayerObject.gameObject;
        Transform playerTransform = player.transform;
        clientTargets[clientId] = playerTransform;

        if (IsServer)
        {
            UpdateClientsTargetGroup();
        }
    }

    private void RemoveClientFromCamera(ulong clientId)
    {
        if (clientTargets.ContainsKey(clientId))
        {
            clientTargets.Remove(clientId);
        }

        if (IsServer)
        {
            UpdateClientsTargetGroup();
        }
    }

    private void UpdateClientsTargetGroup()
    {
        List<ulong> clientIds = new List<ulong>(clientTargets.Keys);
        PlayerData data = new PlayerData { ClientIds = clientIds };
        UpdateClientsTargetGroupClientRpc(data);
    }

    [ClientRpc]
    private void UpdateClientsTargetGroupClientRpc(PlayerData data)
    {
        if (!IsClient) return;

        List<Transform> newTargets = new List<Transform>();

        foreach (var clientId in data.ClientIds)
        {
            GameObject player = FindPlayerByClientId(clientId);
            if (player)
            {
                newTargets.Add(player.transform);
            }
        }

        //targetGroup.m_Targets = new CinemachineTargetGroup.Target[newTargets.Count];


        for (int i = 0; i < newTargets.Count; i++)
        {
            /*targetGroup.m_Targets[i] = new CinemachineTargetGroup.Target
            {
                target = newTargets[i],
                weight = 1f,
                radius = 4f
            };
            */
            targetGroup.AddMember(newTargets[i], 1f, 4f);
        }
    }

    private GameObject FindPlayerByClientId(ulong clientId)
    {
        if (NetworkManager.Singleton.ConnectedClients.ContainsKey(clientId))
        {
            return NetworkManager.Singleton.ConnectedClients[clientId].PlayerObject.gameObject;
        }
        return null;
    }
}
