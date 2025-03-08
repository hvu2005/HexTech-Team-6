using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BotController : MonoBehaviour
{
    [SerializeField]private float moveSpeed = 3f;
    [SerializeField]private float changeDirectionTime = 1f;
    [SerializeField]private BoxCollider2D gridArea;

    private Vector2 targetPosition;
    private Rigidbody2D rb;

    public GameObject bulletPrefab;
    public GameObject rocketPrefab;
    public Transform firePos1;
    public Transform firePos2;
    public Transform lauchPos;

    [SerializeField] private Slider slider;
    private float health;
    private float damageTaken;
    //public Animation boomEffect;
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
            //float x = Random.Range(gridArea.bounds.min.x, gridArea.bounds.max.x);
            float y = Random.Range(gridArea.bounds.min.y, gridArea.bounds.max.y);
            targetPosition = new Vector2(this.transform.position.x, y);
        }
    }
    IEnumerator ShootRoutine()
    {
        Vector2 newDir;
        while (true)
        {
            yield return new WaitForSeconds(1f);
            for (int i = 0; i < 3; i++)
            {
                newDir = new Vector2(0,0);
                if (i == 0) { newDir = new Vector2(-1f, 0.5f); }
                else if (i == 1) { newDir = new Vector2(-1, 0); }
                else if (i == 2) {newDir = new Vector2(-1f, -0.5f); }
                bulletPrefab.GetComponent<Bullet>().setDir(newDir);
                Shoot();
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
    void CheckHealth()
    {
        slider.value = health;
        if (health <= 0)
        {
            StopAllCoroutines();
            //hieu ung no chi choe dung doang
            //BoxCollider2D abc = this.GetComponent<BoxCollider2D>();
            //float x = Random.Range(abc.bounds.min.x, abc.bounds.max.x);
            //float y = Random.Range(abc.bounds.min.y, abc.bounds.max.y);
            //Instantiate(boomEffect, new Vector2(x, y), new Quaternion(x, y, 0, 0));
            //boomEffect.Play();
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerBullet"))
        {
            damageTaken = other.GetComponent<Bullet>().damage;
            health -= damageTaken;
            CheckHealth();
        }

    }
}
