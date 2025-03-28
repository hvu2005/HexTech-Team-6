using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAnimation : MonoBehaviour
{
    private PlayerController _controller;
    private Animator _animator;
    private Player _player;

    // Start is called before the first frame update
    void Start()
    {
        _player = GetComponent<Player>(); 
        _controller = GetComponent<PlayerController>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        MovingAnimating();
        JumpingAnimating();
    }

    private void MovingAnimating()
    {
        _animator.SetBool("isMoving", _controller.xMove != 0);
        
    }

    private void JumpingAnimating()
    {
        _animator.SetBool("isJumping", !_player.isGrounded);
    }
}
