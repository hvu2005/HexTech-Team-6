using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserManager : MonoBehaviour
{
    [SerializeField] GameObject staticLaser;
    [SerializeField] GameObject instantlyDeadLaser;
    [SerializeField] GameObject dynamicLaserLeft;
    [SerializeField] GameObject dynamicLaserRight;

    private void Start()
    {
        StartCoroutine(StaticLaserCoroutine());
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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            instantlyDeadLaser.SetActive(true);
        }
    }
}
