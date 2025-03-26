using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
using Unity.Netcode.Transports.UTP;
using Unity.Netcode;
using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

public class RelayManager : NetworkBehaviour
{
    private const int MAX_CONNECTIONS = 4; // Số người chơi tối đa
    private ChangeSceneNetcode changeScene;

    async void Start()
    {
        changeScene = FindAnyObjectByType<ChangeSceneNetcode>();
        if (changeScene == null)
        {
            Debug.LogError("ChangeSceneNetcode chưa được gán trong Scene!");
        }

        await UnityServices.InitializeAsync();
        if (!AuthenticationService.Instance.IsSignedIn)
        {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
        }
    }

    public async Task<string> CreateRelay()
    {
        try
        {
            Allocation allocation = await RelayService.Instance.CreateAllocationAsync(10);
            string joinCode = await RelayService.Instance.GetJoinCodeAsync(allocation.AllocationId);

            UnityTransport transport = NetworkManager.Singleton.GetComponent<UnityTransport>();
            transport.SetRelayServerData(
                allocation.RelayServer.IpV4,
                (ushort)allocation.RelayServer.Port,
                allocation.AllocationIdBytes,
                allocation.Key,
                allocation.ConnectionData
            );

            NetworkManager.Singleton.StartHost();
            Debug.Log("Relay Created! Join Code: " + joinCode);
            // Chuyển scene trước khi Start Host
            //NetworkManager.SceneManager.LoadScene("WaitRoom", LoadSceneMode.Single);
            Debug.Log("haDuong");
            changeScene.ChangeScene("WaitRoom");

            // Đợi scene load xong rồi mới StartHost
            /* NetworkManager.Singleton.SceneManager.OnLoadComplete += (clientId, sceneName, mode) =>
             {
                 if (sceneName == "WaitRoom" && clientId == NetworkManager.Singleton.LocalClientId)
                 {
                     NetworkManager.Singleton.StartHost();
                     Debug.Log("Relay Created! Join Code: " + joinCode);
                 }
             };*/

            return joinCode;
        }
        catch (RelayServiceException e)
        {
            Debug.LogError(e);
            return null;
        }
    }

    public async void JoinRelay(string joinCode)
    {
        if (string.IsNullOrEmpty(joinCode))
        {
            Debug.LogError("Join code is empty!");
            return;
        }

        try
        {
            JoinAllocation allocation = await RelayService.Instance.JoinAllocationAsync(joinCode);

            UnityTransport transport = NetworkManager.Singleton.GetComponent<UnityTransport>();
            transport.SetRelayServerData(
                allocation.RelayServer.IpV4,
                (ushort)allocation.RelayServer.Port,
                allocation.AllocationIdBytes,
                allocation.Key,
                allocation.ConnectionData,
                allocation.HostConnectionData
            );


            NetworkManager.Singleton.StartClient();
            Debug.Log("Joined Relay with Code: " + joinCode);
        }
        catch (RelayServiceException e)
        {
            Debug.LogError("Failed to join relay: " + e.Message);
        }
    }
}
