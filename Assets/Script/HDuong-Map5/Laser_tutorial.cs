using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser_tutorial : MonoBehaviour
{
    [SerializeField] private float defDistanceRay = 100;
    public Transform laserFirePoint;
    public LineRenderer m_lineRenderer;
    Transform m_tranform;

    private void Awake()
    {
        m_tranform = GetComponent<Transform>();
        m_lineRenderer = GetComponent<LineRenderer>();
        m_lineRenderer.positionCount = 2; // Đảm bảo có 2 điểm để vẽ tia
    }
    private void Update()
    {
        shootLaser();
    }
    void shootLaser()
    {
        RaycastHit2D _hit = Physics2D.Raycast(laserFirePoint.position, transform.right, defDistanceRay);

        if (_hit.collider != null)
        {
            Draw2DRay(laserFirePoint.position, _hit.point);

            // Kiểm tra nếu trúng đối tượng có tag "Player"
            if (_hit.collider.CompareTag("Player"))
            {
                Debug.Log("Laser hit the player!");
            }
        }
        else
        {
            Draw2DRay(laserFirePoint.position, (Vector2)laserFirePoint.position + (Vector2)transform.right * defDistanceRay);
        }
    }

    private void Draw2DRay(Vector2 position, Vector2 point)
    {
        m_lineRenderer.SetPosition(0, position);
        m_lineRenderer.SetPosition(1, point);
    }
}
