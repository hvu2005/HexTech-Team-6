using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserManager : MonoBehaviour
{
    [SerializeField] GameObject staticLaser;
    [SerializeField] GameObject instantlyKillLaser;
    [SerializeField] GameObject dynamicLaserToTheLeft;
    [SerializeField] GameObject dynamicLaserToTheRight;
    [SerializeField] Transform startToTheLeft;
    [SerializeField] Transform endToTheLeft;
    [SerializeField] Transform startToTheRight;
    [SerializeField] Transform endToTheRight;

    Vector3 direction;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(staticLaserCoroutine());
        StartCoroutine(dynamicLaserCoroutine(dynamicLaserToTheLeft, 0));
        StartCoroutine(dynamicLaserCoroutine(dynamicLaserToTheRight, 1));
    }

    public GameObject getDynamicLaserToTheLeft()
    {
        return dynamicLaserToTheLeft;
    }
    public GameObject getDynamicLaserToTheRight()
    {
        return dynamicLaserToTheRight;
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator staticLaserCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(3f);
            staticLaser.SetActive(false);
            yield return new WaitForSeconds(3f);
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
                    if (laser.transform.position.x <= endToTheLeft.position.x)
                    {
                        laser.transform.position = startToTheLeft.position;
                    }
                    break;
                case 1: 
                    direction = Vector2.left;
                    if (laser.transform.position.x >= endToTheRight.position.x)
                    {
                        laser.transform.position = startToTheRight.position;
                    }
                    break;
            }
            yield return new WaitForSeconds(0.2f);
            laser.transform.position += direction;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other != null && other.CompareTag("Player"))
        {
            instantlyKillLaser.SetActive(true);
        }
    }
}
