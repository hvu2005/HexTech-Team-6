using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BotController : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float changeDirectionTime = 1f;
    public BoxCollider2D gridArea;
    //public Vector2 moveAreaMin = new Vector2(3, -7);
    //public Vector2 moveAreaMax = new Vector2(6, -3);

    private Vector2 targetPosition;
    private Rigidbody2D rb;

    public GameObject bulletPrefab;
    public GameObject rocketPrefab;
    public Transform firePos1;
    public Transform firePos2;
    public Transform firePos3;
    public Transform lauchPos;

    public Slider slider;
    float health;
    void Start()
    {
        health = slider.value;
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(ChangeDirection());
        StartCoroutine(ShootRoutine());
        StartCoroutine(LauchRoutine());
    }

    void Update()
    {
        Move();
        CheckHealth();
    }

    void Move()
    {
        Vector2 newPosition = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        rb.MovePosition(newPosition);
    }

    void CheckHealth()
    {
        slider.value = health;
        if (health <= 0)
        {
            StopAllCoroutines();
            //hieu ung no chi choe dung doang
        }
    }

    IEnumerator ChangeDirection()
    {
        while (true)
        {
            yield return new WaitForSeconds(changeDirectionTime);
            //float x = Random.Range(gridArea.bounds.min.x, gridArea.bounds.max.x);
            float y = Random.Range(gridArea.bounds.min.y, gridArea.bounds.max.y);
            //float x = Random.Range(moveAreaMin.x, moveAreaMax.x);
            //float y = Random.Range(moveAreaMin.y, moveAreaMax.y);
            targetPosition = new Vector2(this.transform.position.x, y);
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
        Instantiate(bulletPrefab, firePos3.position, firePos3.rotation);
    }
}
