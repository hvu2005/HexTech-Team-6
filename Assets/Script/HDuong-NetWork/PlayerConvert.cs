using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerConverter : NetworkBehaviour
{
    // Prefab của NewPlayer (không cần đăng ký trong NetworkPrefabList)
    public GameObject newPlayerPrefab;
    public string sceneCompare;

    private void Start()
    {
        // Lắng nghe sự kiện scene được load
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Chỉ server/host thực hiện chuyển đổi
        if (!NetworkManager.Singleton.IsServer)
            return;

        // Kiểm tra tên scene để chuyển đổi
        if (scene.name == sceneCompare)
        {
            Debug.Log("Bắt đầu chuyển đổi player sang prefab mới");

            // Lấy danh sách các player hiện có (giả sử tag "Player" đã được gán cho chúng)
            var players = GameObject.FindGameObjectsWithTag("Player");
            foreach (var player in players)
            {
                NetworkObject netObj = player.GetComponent<NetworkObject>();
                if (netObj != null && netObj.IsSpawned)
                {
                    // Lưu vị trí, hướng và thông tin màu sắc từ player cũ
                    Vector3 pos = player.transform.position;
                    Quaternion rot = player.transform.rotation;

                    // Lấy thông tin màu từ SpriteRenderer (mà script ChangeColorNetwork đã dùng)
                    Color currentColor = Color.white;
                    SpriteRenderer sr = player.GetComponent<SpriteRenderer>();
                    if (sr != null)
                    {
                        currentColor = sr.color;
                    }

                    // Despawn player cũ (với true để tự hủy đối tượng sau khi despawn)
                    netObj.Despawn(true);

                    // Instantiate prefab mới tại cùng vị trí và hướng
                    GameObject newPlayer = Instantiate(newPlayerPrefab, pos, rot);

                    // Gán lại màu sắc cho newPlayer
                    SpriteRenderer newSr = newPlayer.GetComponent<SpriteRenderer>();
                    if (newSr != null)
                    {
                        newSr.color = currentColor;
                    }

                    // Spawn new player qua Network
                    newPlayer.GetComponent<NetworkObject>().Spawn();

                    // Nếu có các dữ liệu khác (ví dụ: điểm, trạng thái), hãy chuyển giao tại đây
                }
            }
            Debug.Log("Chuyển đổi player hoàn tất");
        }
    }
}
