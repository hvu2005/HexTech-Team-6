using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;

public class RelayUI : MonoBehaviour
{
    public RelayManager relayManager; // Tham chiếu đến RelayManager
    public InputField joinCodeInput; // InputField để nhập mã
    public Button hostButton;
    public Button joinButton;

    private void Start()
    {
        hostButton.onClick.AddListener(StartHost);
        joinButton.onClick.AddListener(JoinGame);
    }

    private async void StartHost()
    {
        string joinCode = await relayManager.CreateRelay();
        if (!string.IsNullOrEmpty(joinCode))
        {
            joinCodeInput.text = joinCode; // Hiển thị Join Code cho client
        }
    }

    private void JoinGame()
    {
        string joinCode = joinCodeInput.text;
        if (!string.IsNullOrEmpty(joinCode))
        {
            relayManager.JoinRelay(joinCode);
        }
    }
}
