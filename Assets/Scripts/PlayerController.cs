using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    private static readonly int IsRunning = Animator.StringToHash("isRunning");
    private static readonly int IsGrounded = Animator.StringToHash("isGrounded");
    private static readonly int YVelocity = Animator.StringToHash("yVelocity");

    [Space]
    [Header("Movement Stats")]
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float jumpForce;

    [Space]
    [Header("Ground Definition")]
    [SerializeField]
    private Transform groundCheck;
    [SerializeField]
    private LayerMask whatIsGround;

    private const float GroundedRadius = .2f;

    private Rigidbody2D _playerRb;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private float _horizontalMove;
    private bool _canDoubleJump;
    private bool _isGrounded;
    private bool _jump;
    private bool _isRunning;

    private void Awake()
    {
        _playerRb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        GetInputs();
        UpdateAnimations();
        Flip();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void UpdateAnimations()
    {
        _animator.SetBool(IsRunning, _horizontalMove > 0 || _horizontalMove < 0);
        _animator.SetBool(IsGrounded, _isGrounded);
        _animator.SetFloat(YVelocity, _playerRb.velocity.y);
    }

    private void Flip()
    {
        if (_horizontalMove < 0)
            _spriteRenderer.flipX = true;
        else if (_horizontalMove > 0)
            _spriteRenderer.flipX = false;
    }

    private void GetInputs()
    {
        _horizontalMove = Input.GetAxisRaw("Horizontal") * moveSpeed;

        if (_isGrounded)
            _canDoubleJump = true;

        if (Input.GetButtonDown("Jump"))
            _jump = true;
    }

    private void Move()
    {
        _isGrounded = Physics2D.OverlapCircle(groundCheck.position, GroundedRadius, whatIsGround);

        _playerRb.velocity = new Vector2((_horizontalMove * Time.fixedDeltaTime) * 10f, _playerRb.velocity.y);


        if (_jump && !_isGrounded && !_canDoubleJump)
        {
            _jump = false;
        }

        if (!_jump) return;

        if (_isGrounded)
        {
            _playerRb.velocity = new Vector2(_playerRb.velocity.x, (jumpForce * Time.fixedDeltaTime) * 10f);
            _jump = false;
        }
        else if (_canDoubleJump && !_isGrounded)
        {
            _playerRb.velocity =
                new Vector2(_playerRb.velocity.x, ((jumpForce * Time.fixedDeltaTime) * 10f) / 1.2f);
            _canDoubleJump = false;
            _jump = false;
        }
    }
}
