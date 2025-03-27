using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAnimation : MonoBehaviour
{
    private PlayerController _controller;
    private Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<PlayerController>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        MovingAnimating();
    }

    private void MovingAnimating()
    {
        _animator.SetBool("isMoving", _controller.move != 0);
    }

    private void JumpingAnimating()
    {
        _animator.SetBool("isJumping", _controller.isJumping);
    }
}
