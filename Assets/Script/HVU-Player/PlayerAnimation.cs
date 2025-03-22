using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAnimation : NetworkBehaviour
{
    private PlayerController _controller;
    private Animator _animator;

    private NetworkVariable<bool> isMoving = new NetworkVariable<bool>(false, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    private NetworkVariable<bool> isJumping = new NetworkVariable<bool>(false, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<PlayerController>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsOwner)
        {
            // Cập nhật giá trị trên server
            isMoving.Value = _controller.move != 0;
            isJumping.Value = _controller.isJumping;
        }
        MovingAnimating();
    }

    private void MovingAnimating()
    {
        _animator.SetBool("isMoving", isMoving.Value);
    }

    private void JumpingAnimating()
    {
        _animator.SetBool("isJumping", _controller.isJumping);
    }
}
