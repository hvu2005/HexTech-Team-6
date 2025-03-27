using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class FinishMap : NetworkBehaviour
{
    public GameObject buttonNextMap;

    public int colliderCount = 0;
    public int countMax;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("1");
        Debug.Log(colliderCount+"");
        colliderCount++;
        Debug.Log("2");
        // 🟢 Kiểm tra nếu đủ số người chơi → Hiện button
        if (colliderCount == countMax && IsServer)
        {
            buttonNextMap.SetActive(true);
        }
        Debug.Log(colliderCount + "");
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        colliderCount--;
    }
}
