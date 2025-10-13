using UnityEngine;
using UnityEngine.InputSystem; 

public class PlayerInput : MonoBehaviour
{
    public FrameInput FrameInput { get; private set; }
    PlayerControls _playerInputActions;
    InputAction _move;
    InputAction _jump;
    InputAction _roll;
    InputAction _attack;
    InputAction _parry;
    InputAction _dropDash;
    
    
    void Awake()
    {
        _playerInputActions = new PlayerControls();
        _move = _playerInputActions.FindAction("Move");
        _jump = _playerInputActions.FindAction("Jump");
        _roll = _playerInputActions.FindAction("Roll");
        _attack = _playerInputActions.FindAction("Attack");
        _parry = _playerInputActions.FindAction("Parry");
        _dropDash = _playerInputActions.FindAction("DropDash");
    }
    
    void OnEnable()
    {
        _playerInputActions.Enable();
    }
    
    void OnDisable()
    {
        _playerInputActions.Disable();
    }
    
    void Update()
    {
        FrameInput = GatherInput();
    }
    
    FrameInput GatherInput()
    {
        return new FrameInput
        {
            Move = _move.ReadValue<Vector2>(),
            Jump = _jump.WasPressedThisFrame(),
            //Roll = _roll.WasPressedThisFrame(),
            //Parry = _parry.WasPressedThisFrame(),
            //DropDash = _dropDash.WasPressedThisFrame(),
            Attack = _attack.WasPressedThisFrame(),
        };
    }
}

public struct FrameInput
{
    public Vector3 Move;
    public bool Jump;
    //public bool Roll;
    //public bool Parry;
    //public bool DropDash;
    public bool Attack;
}
