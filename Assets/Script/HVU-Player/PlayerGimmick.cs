using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class PlayerGimmick : MonoBehaviour
{
    public Rigidbody2D Rb;

   
    [SerializeField] private float moveSpeed;
    [SerializeField] private float fireRate;
    [SerializeField] private ObjectPool shooter;

    private float _countFireRateTime;
    private PlayerController _controller;


    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<PlayerController>();
        Rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Rb.velocity = new Vector2(_controller.xMove * moveSpeed, _controller.yMove * moveSpeed);

        if(_controller.isActing) Shoot();
    }

    private void Shoot()
    {
        _countFireRateTime += Time.deltaTime;
        if(_countFireRateTime > fireRate)
        {
            _countFireRateTime = 0;
            GameObject bullet = shooter.GetObject();
        }
        
    }

    private void onDying()
    {
        Debug.Log("thua");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Obstacle"))
        {
            onDying();
        }
    }

    
}
