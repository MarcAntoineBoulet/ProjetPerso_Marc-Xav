using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private Rigidbody _rb;
    private float _horizontalAxis;
    private float _verticalAxis;
    private float _angle;
    private float _angleTarget;
    private float _angleSmouthTurnTime = 0.1f;
    private float _angleSmouthTurnVelocity;

    [SerializeField] private Transform _playerCam;
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
        Vector3 direction = new Vector3(_horizontalAxis, 0f, _verticalAxis).normalized;

        // ------ jump ------- //
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            Jump();
        }

        // ------ Mouvements (left,right,up,down) ------- //
        _rb.velocity = new Vector3(_horizontalAxis * _playerSpeed, _rb.velocity.y, _verticalAxis * _playerSpeed);


        //----Calcule l'angle du perso selon sa direction----//
        if (direction.magnitude >= 0.1f)
        {
            _angleTarget = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            _angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, _angleTarget, ref _angleSmouthTurnVelocity, _angleSmouthTurnTime);
            transform.rotation = Quaternion.Euler(0f, _angle, 0f);
        }
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
