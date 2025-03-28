using UnityEngine;
using Unity.Netcode;
using UnityEngine.SceneManagement;

public class PlayerConverter : NetworkBehaviour
{
    public GameObject newPlayerPrefab; // Prefab của NewPlayer (chưa cần đăng ký)

    private void Start()
    {
        // Lắng nghe sự kiện scene loaded
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Chỉ thực hiện trên server/host
        if (!NetworkManager.Singleton.IsServer)
            return;

        // Nếu scene là Scene3 thì chuyển đổi
        if (scene.name == "Map3 Demo")
        {
            Debug.Log("ChuyenScene");
            // Lấy danh sách các player đang tồn tại (giả sử chúng có tag "Player")
            var players = GameObject.FindGameObjectsWithTag("Player");
            foreach (var player in players)
            {
                // Kiểm tra nếu player hiện tại đang dùng prefab cũ (OldPlayer)
                NetworkObject netObj = player.GetComponent<NetworkObject>();
                if (netObj != null && netObj.IsSpawned)
                {
                    // Lưu dữ liệu cần chuyển (ví dụ: vị trí, hướng, dữ liệu game khác)
                    Vector3 pos = player.transform.position;
                    Quaternion rot = player.transform.rotation;
                    // TODO: Lưu các dữ liệu khác (ví dụ: điểm, máu, v.v.)

                    // Despawn player cũ
                    netObj.Despawn(true);

                    // Instantiate NewPlayer và áp dụng dữ liệu đã lưu
                    GameObject newPlayer = Instantiate(newPlayerPrefab, pos, rot);
                    // Copy dữ liệu cần thiết từ player cũ sang newPlayer (thực hiện thủ công)
                    // Ví dụ: newPlayer.GetComponent<PlayerData>().SetData(player.GetComponent<PlayerData>().GetData());

                    // Spawn new player qua Network
                    newPlayer.GetComponent<NetworkObject>().Spawn();

                    // Cập nhật tham chiếu cho client nếu cần (ví dụ: thông báo cho UI)
                }
            }
            Debug.Log("ChuyenScene2 ");

        }
    }
}
