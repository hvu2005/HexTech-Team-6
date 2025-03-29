using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserManager : MonoBehaviour
{
    [SerializeField] GameObject staticLaser;
    [SerializeField] GameObject instantlyDeadLaser;
    [SerializeField] GameObject dynamicLaserLeft;
    [SerializeField] GameObject dynamicLaserRight;
    [SerializeField] Transform startToLeft;
    [SerializeField] Transform startToRight;
    [SerializeField] Transform endToLeft;
    [SerializeField] Transform endToRight;
    Vector3 direction;
    private void Start()
    {
        StartCoroutine(StaticLaserCoroutine());
        StartCoroutine(dynamicLaserCoroutine(dynamicLaserLeft, 1));
        StartCoroutine(dynamicLaserCoroutine(dynamicLaserRight, 0));
    }

    IEnumerator StaticLaserCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(3);
            staticLaser.SetActive(false);
            yield return new WaitForSeconds(3);
            staticLaser.SetActive(true);
        }
    }
    IEnumerator dynamicLaserCoroutine(GameObject laser, int bit)
    {
        while (true)
        {
            switch (bit)
            {
                case 0:
                    direction = Vector2.right;
                    if (laser.transform.position.x <= endToLeft.position.x)
                    {
                        laser.transform.position = startToRight.position;
                    }
                    break;
                case 1:
                    direction = Vector2.left;
                    if (laser.transform.position.x >= endToRight.position.x)
                    {
                        laser.transform.position = startToLeft.position;
                    }
                    break;
            }
            yield return new WaitForSeconds(0.2f);
            laser.transform.position += direction;
        }
    }
    public GameObject getDynamicLaserToTheLeft()
    {
        return dynamicLaserLeft;
    }
    public GameObject getDynamicLaserToTheRight()
    {
        return dynamicLaserRight;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            instantlyDeadLaser.SetActive(true);
        }
    }
}
