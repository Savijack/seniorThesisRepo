using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Timeline;

public class PlayerController : MonoBehaviour
{
    public static Action OnJump;
    
    [SerializeField] float _moveSpeed = 10f;
    [SerializeField] float _jumpSpeed = 10f;
    [SerializeField] float _gravity = 9.81f;
    [SerializeField] float _extraGravity = 700f;
    [SerializeField] float _maxFallSpeed = -25f;
    [SerializeField] float _gravityDelay = .2f;
    [SerializeField] float _coyoteTime = .1f;
    [SerializeField] float _acceleration = .5f;
    [SerializeField] private float _maxSpeed = 20f;
    
    [SerializeField] Transform _groundCheck;
    [SerializeField] float _groundCheckRadius = 0.2f;
    [SerializeField] LayerMask _groundLayer;
    
    bool _isGrounded = true;
    
    PlayerInput _playerInput;
    FrameInput _frameInput;
    CapsuleCollider _playerCollider;
    Rigidbody _playerRigidBody;
    
    private BoxCollider _attackCollider;

    void OnEnable()
    {
        OnJump += ApplyJumpForce;
    }

    void OnDisable()
    {
        OnJump -= ApplyJumpForce;
    }

    void Awake()
    {
        
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _attackCollider = GetComponentInChildren<BoxCollider>();
        _attackCollider.enabled = false;
        _playerInput = GetComponent<PlayerInput>();
        _playerCollider = GetComponent<CapsuleCollider>();
        _playerRigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        GatherInput();
        Run();
        HandleJump();
        Attack();
        //CoyoteTimer();
    }

    void FixedUpdate()
    {
        Gravity();
        ExtraGravity();
    }

    void GatherInput()
    {
        _frameInput = _playerInput.FrameInput;
    }

    void HandleJump()
    {
        /*if (!_frameInput.Jump) return;
        //if (_footCollider)
        //{
        OnJump?.Invoke();
        //}
       /* else if (_coyoteTimer > 0f)
        {
            OnJump?.Invoke();
        }*/
       _isGrounded = Physics.CheckSphere(_groundCheck.position, _groundCheckRadius, _groundLayer);
       if(_frameInput.Jump && _isGrounded)
           OnJump?.Invoke();
    }

    void ApplyJumpForce()
    {
        //Vector3 jump = new Vector3(0f, _jumpSpeed, 0f);
        //transform.position += jump * Time.deltaTime;
        _playerRigidBody.AddForce(Vector3.up * _jumpSpeed, ForceMode.Impulse);
    }

    void Run()
    {
        Vector3 movement = new Vector3(_frameInput.Move.x, 0f, _frameInput.Move.y);
        transform.position += movement * _moveSpeed * Time.deltaTime;
    }

    void Gravity()
    {
        _playerRigidBody.linearVelocity -= Vector3.up * _gravity * Time.deltaTime;
    }

    void ExtraGravity()
    {
        
    }

    void Attack()
    {
        if (!_frameInput.Attack) return;
        _attackCollider.enabled = true;
        
    }
    
    void CoyoteTimer()
    {
        /*_isGrounded = Physics.CheckSphere(_groundCheck.position, _groundCheckRadius, _groundLayer);
        if (_isGrounded)
        {
            _coyoteTimer = _coyoteTime;
        }
        else
        {
            _coyoteTimer -= Time.deltaTime;
        }*/
    }
    
}
