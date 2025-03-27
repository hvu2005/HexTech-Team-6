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
        m_lineRenderer.material.color = Color.red;
    }
    private void Update()
    {
        shootLaser();
    }
    void shootLaser()
    {
        if(Physics2D.Raycast(m_tranform.position, transform.right))
        {
            RaycastHit2D _hit = Physics2D.Raycast(laserFirePoint.position, transform.right);
            Draw2DRay(laserFirePoint.position, _hit.point);
        }
        else
        {
            Draw2DRay(laserFirePoint.position, laserFirePoint.transform.right * defDistanceRay);
        }
    }

    private void Draw2DRay(Vector2 position, Vector2 point)
    {
        m_lineRenderer.SetPosition(0, position);
        m_lineRenderer.SetPosition(1, point);
    }
}
