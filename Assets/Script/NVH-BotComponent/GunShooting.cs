using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunShooting : MonoBehaviour
{
    public GameObject playerBullet;
    public Transform spawn;
    // Start is called before the first frame update
    void Start()
    {
        playerBullet.GetComponent<Bullet>().dir = new Vector2(1, 0);
        StartCoroutine(TestShootRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator TestShootRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            Shoot();
        }
    }

    void Shoot()
    {
        Instantiate(playerBullet, spawn.position, spawn.rotation);
    }
}
