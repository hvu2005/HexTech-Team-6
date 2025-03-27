using UnityEngine;
using System.Collections.Generic;

public class FollowGroupCamera : MonoBehaviour
{
    public Vector3 offset = new Vector3(0, 2, -10);  // Vị trí offset của camera
    public float smoothTime = 0.3f; // Thời gian trễ khi di chuyển camera

    private Vector3 velocity;
    private List<Transform> targets = new List<Transform>();  // Danh sách các nhân vật

    void LateUpdate()
    {
        // Lấy danh sách player từ GameManager
        targets = GameManager.Instance.GetPlayers();

        // Kiểm tra nếu không có nhân vật nào thì không làm gì cả
        if (targets == null || targets.Count == 0) return;

        Vector3 centerPoint = GetCenterPoint();
        Vector3 newPosition = centerPoint + offset;

        // Di chuyển camera mượt theo vị trí trung tâm nhóm nhân vật
        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smoothTime);
    }

    // Tính toán điểm trung tâm của nhóm nhân vật
    Vector3 GetCenterPoint()
    {
        if (targets.Count == 1)
            return targets[0].position;

        Bounds bounds = new Bounds(targets[0].position, Vector3.zero);
        foreach (Transform target in targets)
        {
            bounds.Encapsulate(target.position);
        }
        return bounds.center;
    }
}
