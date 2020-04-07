using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private Rigidbody2D playerRb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask whatIsGround;

    private const float GroundedRadius = .2f;
    private bool _canDoubleJump;
    private bool _isGrounded;


    private void Update()
    {
        playerRb.velocity = new Vector2(moveSpeed * Input.GetAxisRaw("Horizontal"), playerRb.velocity.y);

        _isGrounded = Physics2D.OverlapCircle(groundCheck.position, GroundedRadius, whatIsGround);

        if (_isGrounded)
            _canDoubleJump = true;

        if (Input.GetButtonDown("Jump"))
        {
            if (_isGrounded)
            {
                playerRb.velocity = new Vector2(playerRb.velocity.x, jumpForce);
            }
            else if (_canDoubleJump)
            {
                playerRb.velocity = new Vector2(playerRb.velocity.x, jumpForce);
                _canDoubleJump = false;
            }
        }
    }
}
