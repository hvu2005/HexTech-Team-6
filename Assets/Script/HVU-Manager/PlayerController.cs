using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Netcode;

public class PlayerController : NetworkBehaviour
{

    [SerializeField] private PlayerInput data;

    public bool canGetAction { get; set; } = true;
    public bool isJumping { get; private set; }
    public float move { get; private set; }
    public bool anyKeyDown { get; private set; }


    public override void OnNetworkSpawn()
    {
        if(!IsOwner)
        {
            enabled = false;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsOwner) return;

        anyKeyDown = Input.anyKeyDown;

        if(!canGetAction)
        {
            return;
        }

        isJumping = data.actions["Jump"].WasPressedThisFrame();
        move = data.actions["Move"].ReadValue<float>();
    }
}
