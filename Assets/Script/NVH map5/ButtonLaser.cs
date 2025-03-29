using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonLaser : MonoBehaviour
{
    [SerializeField] GameObject on;
    [SerializeField] GameObject off;
    [SerializeField] LaserManager laserManager;
    bool isActive;
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isActive = false;
            on.SetActive(isActive);
            off.SetActive(!isActive);
            laserManager.getDynamicLaserToTheLeft().SetActive(isActive);
            laserManager.getDynamicLaserToTheRight().SetActive(isActive);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isActive = true;
            on.SetActive(isActive);
            off.SetActive(!isActive);
            laserManager.getDynamicLaserToTheLeft().SetActive(isActive);
            laserManager.getDynamicLaserToTheRight().SetActive(isActive);
        }
    }
}
