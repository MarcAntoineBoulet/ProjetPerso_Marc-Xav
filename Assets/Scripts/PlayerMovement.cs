using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody _rb;

    private float _horizontalAxis;
    private float _verticalAxis;

    [SerializeField] private float _playerSpeed = 5f;
    [SerializeField] private float _playerJumpForce = 5f;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private LayerMask _ground;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        PlayerMovements();
    }

    private void PlayerMovements()
    {
        _horizontalAxis = Input.GetAxis("Horizontal");
        _verticalAxis = Input.GetAxis("Vertical");
        // ------ jump ------- //
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            Jump();
        }
        // ------ Mouvements (left,right,up,down) ------- //
        _rb.velocity = new Vector3(_horizontalAxis * _playerSpeed, _rb.velocity.y, _verticalAxis * _playerSpeed);
    }

    private void Jump()
    {
        _rb.velocity = new Vector3(_rb.velocity.x, _playerJumpForce, _rb.velocity.z);
    }

    /// <summary>
    /// OnCollisionEnter is called when this collider/rigidbody has begun
    /// touching another rigidbody/collider.
    /// </summary>
    /// <param name="other">The Collision data associated with this collision.</param>
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy Head"))
        {
            Destroy(collision.transform.parent.gameObject);
            Jump();
        }
    }

    bool IsGrounded()
    {
        return Physics.CheckSphere(_groundCheck.position, .1f, _ground);
    }
}
