using UnityEngine;
using Unity.Netcode;
using UnityEngine.UI;

public class NetworkManagerUI : MonoBehaviour
{
    public Button hostButton;
    public Button clientButton;
    public Button serverButton;

    private void Start()
    {
        hostButton.onClick.AddListener(() => NetworkManager.Singleton.StartHost());
        clientButton.onClick.AddListener(() => NetworkManager.Singleton.StartClient());
        serverButton.onClick.AddListener(() => NetworkManager.Singleton.StartServer());
    }

}