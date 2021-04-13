using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CharacterController))]
public class FPVMoveController : MonoBehaviour
{
    [SerializeField] [Range(0f, 100f)] private float _speed = 0f;
    [SerializeField] [Range(0f, 10f)] private float _jumpHeight = 0f;
    [SerializeField] private Transform _groundCheck = null;
    private Transform _transform;
    private CharacterController _characterController;
    private float _jumpVelocity;
    private readonly float _gravityValue = Physics.gravity.y;

    private void Awake()
    {
        _transform = transform;
        _characterController = GetComponent<CharacterController>();
        if (!_characterController) Debug.LogWarning("Missing CharacterController component", this);
        if (!_groundCheck) Debug.LogWarning("Missing Ground Check reference in FPVMoveController", this);
    }
    private void Update()
    {
        Vector3 xMovement = Input.GetAxis("Horizontal") * _transform.right;
        Vector3 zMovement = Input.GetAxis("Vertical") * _transform.forward;
        Vector3 movement = (xMovement + zMovement) * Time.deltaTime * _speed;
        if (_characterController)
        {
            _characterController.Move(movement);
        }

        HandleJump();
        HandleCrouch();
    }

    private void HandleJump()
    {
        if (!_groundCheck) return;

        bool isGrounded = Physics.CheckSphere(_groundCheck.position, 0.1f);
        if (isGrounded && _jumpVelocity < 0)
        {
            _jumpVelocity = 0f;
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            _jumpVelocity += Mathf.Sqrt(_jumpHeight * -3.0f * _gravityValue);
        }

        _jumpVelocity += _gravityValue * Time.deltaTime;
        _characterController.Move(new Vector3(0f, _jumpVelocity, 0f) * Time.deltaTime);
    }
    
    private void HandleCrouch()
    {
        if (Input.GetKeyDown("left ctrl"))
        {
            _characterController.height = 1;
            transform.position -= new Vector3(0, 0.5f, 0);
            _groundCheck.transform.position += new Vector3(0, 0.5f, 0);
        }
        else if (Input.GetKeyUp("left ctrl"))
        {
            _characterController.height = 2;
            transform.position += new Vector3(0, 0.5f, 0);
            _groundCheck.transform.position -= new Vector3(0, 0.5f, 0);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(_groundCheck.position, 0.1f);
    }
}
        