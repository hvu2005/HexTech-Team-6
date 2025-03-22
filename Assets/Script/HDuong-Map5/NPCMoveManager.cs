using System.Collections;
using Unity.Netcode;
using UnityEngine;

public class NPCMovement : NetworkBehaviour
{
    public Transform pointA; // Điểm bắt đầu
    public Transform pointB; // Điểm kết thúc
    public float speed = 2f;

    private Vector3 targetPosition;
    private bool movingToB = true;

    private void Start()
    {
        if (IsServer) // Chỉ Server điều khiển NPC
        {
            targetPosition = pointB.position;
            StartCoroutine(MoveNPC());
        }
    }

    private IEnumerator MoveNPC()
    {
        while (true)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                movingToB = !movingToB;
                targetPosition = movingToB ? pointB.position : pointA.position;
            }

            yield return null;
        }
    }
}
