using System.Collections;
using Unity.Netcode;
using UnityEngine;

public class NPCMovement : NetworkBehaviour
{
    private Vector3 pointA;
    private Vector3 pointB;
    public float speed = 2f;

    private Vector3 targetPosition;
    private bool movingToB = true;

    private NetworkVariable<Vector3> networkPosition = new NetworkVariable<Vector3>();

    private void Start()
    {
        if (IsServer)
        {
            pointA = transform.position - new Vector3(2f, 0f, 0f);
            pointB = transform.position + new Vector3(2f, 0f, 0f);

            targetPosition = pointB;
            StartCoroutine(MoveNPC());
        }
    }

    private IEnumerator MoveNPC()
    {
        while (true)
        {
            Vector3 newPosition = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            transform.position = newPosition;
            networkPosition.Value = newPosition; // Đồng bộ vị trí với client

            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                movingToB = !movingToB;
                targetPosition = movingToB ? pointB : pointA;
            }
            yield return null;
        }
    }

    private void Update()
    {
        if (!IsServer)
        {
            transform.position = networkPosition.Value; // Cập nhật vị trí trên client
        }
    }
}

