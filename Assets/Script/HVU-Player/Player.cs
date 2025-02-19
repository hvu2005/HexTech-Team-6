using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(PlayerController))]
public class Player : NetworkBehaviour
{
    public Rigidbody2D Rb;

    [SerializeField] private float moveSpeed;

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
        
    }

    private void FixedUpdate()
    {
        if (!IsServer) return;

        Rb.velocity = new Vector2(_controller.move * moveSpeed, Rb.velocity.y);
    }

    
}
