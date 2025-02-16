using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : Singleton<InputManager>
{

    [SerializeField] private PlayerInput data;
    
    public PlayerInput Data => data;

    public bool canGetAction {  get; set; }
    public bool isJumping { get; private set; }
    public float move { get; private set; }
    public bool anyKeyDown { get; private set; }


    protected override void Awake()
    {
        base.Awake();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        anyKeyDown = Input.anyKeyDown;

        if(!canGetAction)
        {
            return;
        }

        isJumping = data.actions["Jumping"].WasPressedThisFrame();
        move = data.actions["Move"].ReadValue<float>();
    }
}
