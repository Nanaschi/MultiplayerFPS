using System;
using Unity.Mathematics;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float _mouseSensitivity, _sprintSpeed, _walkSpeed, _jumpForce, _smoothTime;

    [SerializeField] private GameObject _cameraHolder;
    private float _verticalLookRotation;
    private bool _grounded;
    private Vector3 _smoothMoveVelocity;
    private Vector3 _moveAmount;

    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        LookRotation();
    }

    private void LookRotation()
    {
        transform.Rotate(Vector3.up * Input.GetAxisRaw("Mouse X") * _mouseSensitivity);

        _verticalLookRotation += Input.GetAxisRaw("Mouse Y") * _mouseSensitivity;

        _verticalLookRotation = math.clamp(_verticalLookRotation, -90, 90);

        _cameraHolder.transform.localEulerAngles = Vector3.left * _verticalLookRotation;
    }
}