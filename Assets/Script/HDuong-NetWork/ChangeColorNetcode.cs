using Unity.Netcode;
using UnityEngine;

public class ChangeColorNetwork : NetworkBehaviour
{
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();

        if (IsClient && IsOwner) // 🟢 Chỉ Client nào sở hữu object mới có thể đổi màu
        {
            if (Input.GetKeyDown(KeyCode.C)) // Nhấn phím C để đổi màu
            {
                RequestChangeColorServerRpc();
            }
        }
    }

    private void Update()
    {
        if (IsClient && IsOwner) // 🟢 Kiểm tra nếu là client sở hữu
        {
            if (Input.GetKeyDown(KeyCode.C)) // Nhấn phím C để đổi màu
            {
                RequestChangeColorServerRpc();
            }
        }
    }

    // 🟢 Client yêu cầu server đổi màu
    [ServerRpc(RequireOwnership = false)]
    private void RequestChangeColorServerRpc(ServerRpcParams rpcParams = default)
    {
        Color newColor = new Color(Random.value, Random.value, Random.value, 1f);
        ApplyColorClientRpc(newColor);
    }

    // 🟢 Server gửi màu mới đến tất cả client
    [ClientRpc]
    private void ApplyColorClientRpc(Color newColor)
    {
        spriteRenderer.color = newColor;
    }
}
