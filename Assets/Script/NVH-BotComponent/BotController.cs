using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class BotController : MonoBehaviour
{
    [SerializeField] private float introSpeed;
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float changeDirectionTime = 1f;
    [SerializeField] private BoxCollider2D gridArea;

    [SerializeField] private Vector2 targetPosition;
    private Rigidbody2D rb;

    public GameObject bulletPrefab;
    public GameObject rocketPrefab;
    public Transform firePos1;
    public Transform firePos2;
    public Transform lauchPos;

    [SerializeField] private Slider slider;
    private float health;
    private bool isAlive = true;
    private float damageTaken;

    public BoxCollider2D boomSpawn;
    public List<GameObject> boomEffect;
    private List<GameObject> boomList = new List<GameObject>();
    void Start()
    {
        
        health = slider.value;
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(AllRoutine());
    }

    void Update()
    {
        if (isAlive)
        {
            Move();
        }
        
    }

    void Move()
    {
        Vector2 newPosition = Vector2.MoveTowards(transform.position, targetPosition, introSpeed * Time.deltaTime);
        rb.MovePosition(newPosition);
    }
    //Change random in gird area
    IEnumerator ChangeDirection()
    {
        while (true)
        {
            yield return new WaitForSeconds(changeDirectionTime);
            float x = Random.Range(gridArea.bounds.min.x, gridArea.bounds.max.x); // Giới hạn x trong gridArea
            float y = Random.Range(gridArea.bounds.min.y, gridArea.bounds.max.y); // Giới hạn y trong gridArea
            targetPosition = new Vector2(x, y); // Đặt targetPosition mới hoàn toàn trong gridArea
            introSpeed = moveSpeed;
        }
    }
    //all routine
    IEnumerator AllRoutine()
    {
        SetAllCollidersStatus(false);
        yield return new WaitForSeconds(5f);
        SetAllCollidersStatus(true);
        StartCoroutine(ChangeDirection());
        StartCoroutine(ShootRoutine());
        StartCoroutine(LauchRoutine());
    }
    //Shoot bullet
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
    void Shoot()
    {
        Instantiate(bulletPrefab, firePos1.position, firePos1.rotation);
        Instantiate(bulletPrefab, firePos2.position, firePos2.rotation);
    }
    //Lauch roccket
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
    //health
    void CheckHealth()
    {
        slider.value = health;
        if (health <= 0)
        {
            this.GetComponent<BoxCollider2D>().enabled = false;
            isAlive = false;
            StopAllCoroutines();
            //hieu ung no chi choe dung doang
            StartCoroutine(BoomRoutine());
        }
    }
    //boom
    IEnumerator BoomRoutine()
    {
        while (true)
        {
            if (boomList.Count >= 10)
            {
                for (int i = boomList.Count - 1; i >= 0; i--)
                {
                    Destroy(boomList[i]);
                }
                boomList.Clear();
                yield return new WaitForSeconds(2);
                Destroy(this.gameObject);
                yield break;
            }
            else
            {
                float x = Random.Range(boomSpawn.bounds.min.x, boomSpawn.bounds.max.x);
                float y = Random.Range(boomSpawn.bounds.min.y, boomSpawn.bounds.max.y);
                int ranNum = Random.Range(0, 2);
                GameObject xBoom = Instantiate(boomEffect[ranNum], new Vector2((int)x, (int)y), Quaternion.identity);
                xBoom.transform.SetParent(this.transform);
                boomList.Add(xBoom);
                if (boomList.Count == 7)
                {
                    this.GetComponent<Rigidbody2D>().gravityScale = 1;
                }
            }        
            yield return new WaitForSeconds(0.5f);
        }
    }
    //Others
    public void SetAllCollidersStatus(bool active)
    {
        this.GetComponent<BoxCollider2D>().enabled = active;
        this.boomSpawn.GetComponent<BoxCollider2D>().enabled = active;
    }
    //check collider
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
