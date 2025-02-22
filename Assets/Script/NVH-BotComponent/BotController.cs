using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotController : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float changeDirectionTime = 1f;
    public BoxCollider2D gridArea;
    public Vector2 moveAreaMin = new Vector2(3, -7);
    public Vector2 moveAreaMax = new Vector2(6, -3);

    private Vector2 targetPosition;
    private Rigidbody2D rb;

    public GameObject bulletPrefab;
    public GameObject rocketPrefab;
    public Transform firePos1;
    public Transform firePos2;
    public Transform lauchPos;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(ChangeDirection());
        StartCoroutine(ShootRoutine());
        StartCoroutine(LauchRoutine());
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        Vector2 newPosition = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        rb.MovePosition(newPosition);
    }

    IEnumerator ChangeDirection()
    {
        while (true)
        {
            yield return new WaitForSeconds(changeDirectionTime);
            float x = Random.Range(gridArea.bounds.min.x, gridArea.bounds.max.x);
            float y = Random.Range(gridArea.bounds.min.y, gridArea.bounds.max.y);
            //float x = Random.Range(moveAreaMin.x, moveAreaMax.x);
            //float y = Random.Range(moveAreaMin.y, moveAreaMax.y);
            targetPosition = new Vector2(x, y);
        }
    }
    IEnumerator ShootRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(2f);
            for (int i = 0; i < 5; i++)
            {
                Shoot();
                yield return new WaitForSeconds(1f);
            }
        }
    }

    IEnumerator LauchRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(5f);
            Lauch();
        }
    }
    void Lauch()
    {
        Instantiate(rocketPrefab, lauchPos.position, lauchPos.rotation);
    }
    void Shoot()
    {
        Instantiate(bulletPrefab, firePos1.position, firePos1.rotation);
        Instantiate(bulletPrefab, firePos2.position, firePos2.rotation);
    }
}
