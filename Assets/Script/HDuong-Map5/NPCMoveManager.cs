using System.Collections;
using Unity.Netcode;
using UnityEngine;

public class NPCMovement : NetworkBehaviour
{
    private Vector3 pointA; // Tọa độ điểm A
    private Vector3 pointB;  // Tọa độ điểm B
    public float speed = 2f;

    private Vector3 targetPosition;
    private bool movingToB = true;

    private void Start()
    {
        pointA = transform.position - new Vector3(2f, 0f, 0f);
        pointB = transform.position + new Vector3(2f, 0f, 0f);
        if (IsServer) // Chỉ Server điều khiển NPC
        {
            targetPosition = pointB;
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
                targetPosition = movingToB ? pointB : pointA;
            }
            yield return null;
        }
    }
}
