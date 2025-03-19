using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonShooting : MonoBehaviour
{
    [SerializeField] LayerMask layerMask;
    [SerializeField] Transform spawnPos;
    [SerializeField] float distance;
    [SerializeField] Vector2 direction;
    [SerializeField] float speed;
    [SerializeField] GameObject boomEffect;
    private bool isDetectPlayer = false;
    private bool isShooting = false;

    public GameObject bulletPrefap;
    GameObject bullet;
    // Start is called before the first frame update
    void Awake()
    {
        if(Mathf.Approximately(this.transform.eulerAngles.y, 0)) direction = Vector2.left;
        if(Mathf.Approximately(this.transform.eulerAngles.y, 180)) direction = Vector2.right;
        StartCoroutine(ShootRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        //isDetectPlayer = Physics2D.Raycast(this.transform.position, direction, distance, layerMask);
        //Debug.DrawRay(transform.position, direction * distance, isDetectPlayer ? Color.red : Color.green);
        //boomEffect.SetActive(isDetectPlayer);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, distance, layerMask);
        Debug.DrawRay(transform.position, direction * distance, hit ? Color.red : Color.green);

        // Kiểm tra nếu phát hiện Player mà không có vật cản
        if (hit.collider != null && hit.collider.CompareTag("Player"))
        {
            isDetectPlayer = true;
        }
        else
        {
            isDetectPlayer = false;
        }

        boomEffect.SetActive(isDetectPlayer);

        // Nếu phát hiện Player và chưa bắn, bắt đầu bắn
        if (isDetectPlayer && !isShooting)
        {
            StartCoroutine(ShootRoutine());
        }

    }

    void Shooting()
    {
        bullet = Instantiate(bulletPrefap, spawnPos.position, spawnPos.rotation);
        bullet.GetComponent<Bullet>().setDir(direction);
        bullet.GetComponent<Bullet>().setSpeed(speed);
    }
    IEnumerator ShootRoutine()
    {
        isShooting = true; // Đánh dấu đang bắn

        while (isDetectPlayer)
        {
            Shooting();
            yield return new WaitForSeconds(2f);

            // Kiểm tra lại xem còn thấy player không
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, distance, layerMask);
            isDetectPlayer = hit.collider != null && hit.collider.CompareTag("Player");
        }

        isShooting = false; // Cho phép coroutine chạy lại nếu phát hiện Player lần sau while (isDetectPlayer)
        {
             Shooting();
             yield return new WaitForSeconds(2f);
        }  
    }
}
