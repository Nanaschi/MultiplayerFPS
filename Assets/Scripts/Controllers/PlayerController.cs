using System;
using Photon.Pun;
using Unity.Mathematics;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(PhotonView))]
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
    private PhotonView _photonView;

    [SerializeField]
    private PlayerGroundCheck _playerGroundCheck;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _photonView = GetComponent<PhotonView>();
    }

    private void OnEnable()
    {
        _playerGroundCheck.OnGroundStanding += SetGroundedState;
    }

    private void OnDisable()
    {
        _playerGroundCheck.OnGroundStanding -= SetGroundedState;
    }

    private void Start()
    {
        if (!_photonView.IsMine)
        {
            Destroy(GetComponentInChildren<Camera>().gameObject);
            Destroy(_rigidbody);
        }
    }

    private void FixedUpdate()
    {
        if (!_photonView.IsMine) return;
        
        LookRotation();

        Move();

        Jump();
        
        _rigidbody.MovePosition
            (_rigidbody.position + transform.TransformDirection(_moveAmount) * Time.fixedDeltaTime);
    }

    private void Move()
    {
        Vector3 moveDir =
            new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;

        _moveAmount = Vector3.SmoothDamp(_moveAmount,
            moveDir * (Input.GetKey(KeyCode.LeftShift) ? _sprintSpeed : _walkSpeed),
            ref _smoothMoveVelocity, _smoothTime);
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _grounded)
        {
            _rigidbody.AddForce(transform.up * _jumpForce);
        }
    }

    private void SetGroundedState(bool grounded)
    {
        _grounded = grounded;
    }

    private void LookRotation()
    {
        transform.Rotate(Vector3.up * Input.GetAxisRaw("Mouse X") * _mouseSensitivity);

        _verticalLookRotation += Input.GetAxisRaw("Mouse Y") * _mouseSensitivity;

        _verticalLookRotation = math.clamp(_verticalLookRotation, -90, 90);

        _cameraHolder.transform.localEulerAngles = Vector3.left * _verticalLookRotation;
    }
}