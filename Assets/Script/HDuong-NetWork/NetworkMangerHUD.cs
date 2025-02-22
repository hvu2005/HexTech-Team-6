using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Network : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnGUI()
    {
        if (GUILayout.Button("Start host")){
            NetworkManager.Singleton.StartHost();
        }
        if(GUILayout.Button("Start client"))
        {
            NetworkManager.Singleton.StartClient();
        }
        if(GUILayout.Button("Start server"))
        {
            NetworkManager.Singleton.StartServer();
        }
    }
}
