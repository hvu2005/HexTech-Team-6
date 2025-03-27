using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.Networking.Transport;
using UnityEngine;

public class Network : MonoBehaviour
{
    void Start()
    {
        // Gọi phương thức để khởi động server hoặc client
    }

    public void StartHost()
    { 
        NetworkManager.Singleton.StartHost();
        Debug.Log("Host started.");
    }

    public void StartClient()
    {
        NetworkManager.Singleton.StartClient();
        Debug.Log("Client started.");
    }
}
