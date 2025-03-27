using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Netcode;
using UnityEngine;

public class PlayerNetwork : NetworkBehaviour
{
    [SerializeField] private GameObject spawnedObjectPrefab; // Chuyển từ Transform sang GameObject
    private NetworkObject spawnedNetworkObject;

    private NetworkVariable<MyCustomData> randomNumber = new NetworkVariable<MyCustomData>(
        new MyCustomData
        {
            _int = 56,
            _bool = true,
        }, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

    public struct MyCustomData : INetworkSerializable
    {
        public int _int;
        public bool _bool;
        public FixedString128Bytes message;

        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            serializer.SerializeValue(ref _int);
            serializer.SerializeValue(ref _bool);
            serializer.SerializeValue(ref message);
        }
    }

    public override void OnNetworkSpawn()
    {
        randomNumber.OnValueChanged += (MyCustomData previousValue, MyCustomData newValue) =>
        {
            Debug.Log(OwnerClientId + "; " + newValue._int + "; " + newValue._bool + "; " + newValue.message);
        };
    }

    private void Update()
    {
        if (!IsOwner) return;

        if (Input.GetKeyDown(KeyCode.T))
        {
            SpawnObjectServerRpc();
        }

        if (Input.GetKeyDown(KeyCode.K) && spawnedNetworkObject != null)
        {
            spawnedNetworkObject.Despawn(true);
            spawnedNetworkObject = null; // Reset lại để tránh lỗi
        }
    }

    [ServerRpc]
    private void SpawnObjectServerRpc(ServerRpcParams rpcParams = default)
    {
        if (spawnedObjectPrefab == null)
        {
            Debug.LogError("SpawnObjectServerRpc: Prefab chưa được gán!");
            return;
        }

        GameObject obj = Instantiate(spawnedObjectPrefab);
        spawnedNetworkObject = obj.GetComponent<NetworkObject>();

        if (spawnedNetworkObject == null)
        {
            Debug.LogError("SpawnObjectServerRpc: Prefab không có NetworkObject!");
            Destroy(obj);
            return;
        }

        spawnedNetworkObject.Spawn(true);
    }

    [ServerRpc]
    private void TestServerRpc(ServerRpcParams serverRpcParams)
    {
        Debug.Log("TestServerRpc " + OwnerClientId + "; " + serverRpcParams.Receive.SenderClientId);
    }

    [ClientRpc]
    private void TestClientRpc(ClientRpcParams clientRpcParams)
    {
        Debug.Log("TestClientRpc");
    }
}
