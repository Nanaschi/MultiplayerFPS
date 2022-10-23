using System;
using System.Reflection;
using ExitGames.Client.Photon;
using Interfaces;
using Photon.Pun;
using Photon.Realtime;
using Unity.Mathematics;
using UnityEngine;


[RequireComponent(typeof(PhotonView))]
public class PlayerController : MonoBehaviourPunCallbacks, IDamageable
{
    [SerializeField]
    private float _mouseSensitivity, _sprintSpeed, _walkSpeed, _jumpForce, _smoothTime;

    [SerializeField] private GameObject _cameraHolder;

    [SerializeField]
    private PlayerGroundCheck _playerGroundCheck;

    [SerializeField] private Item[] _items;

    [SerializeField] private float _maxHealth;
    private float _currentHealth;

    private int _itemIndex;
    private int _previousItemIndex;

    private float _verticalLookRotation;
    private bool _grounded;
    private Vector3 _smoothMoveVelocity;
    private Vector3 _moveAmount;

    private Rigidbody _rigidbody;
    private PhotonView _photonView;

    private PlayerManager _playerManager;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _photonView = GetComponent<PhotonView>();
        _playerManager = PhotonView.Find((int) photonView.InstantiationData[0]).GetComponent<PlayerManager>();
        
        _previousItemIndex = -1;
        _currentHealth = _maxHealth;
    }

    public override void OnEnable()
    {
        base.OnEnable();
        _playerGroundCheck.OnGroundStanding += SetGroundedState;
    }

    public override void OnDisable()
    {
        base.OnDisable();
        _playerGroundCheck.OnGroundStanding -= SetGroundedState;
    }


    private void Start()
    {
        if (_photonView.IsMine)
        {
            EquipItem(0);
        }
        else
        {
            Destroy(GetComponentInChildren<Camera>().gameObject);
            Destroy(_rigidbody);
        }
    }

    private void FixedUpdate()
    {
        if (!_photonView.IsMine) return;


        Move();

        Jump();

        LookRotation();

        SwitchWeapons();

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            _items[_itemIndex].Use();
        }
    }

    private void SwitchWeapons()
    {
        for (int i = 0; i < _items.Length; i++)
        {
            if (Input.GetKeyDown((i + 1).ToString()))
            {
                EquipItem(i);
                break;
            }
        }

        if (Input.GetAxisRaw("Mouse ScrollWheel") > 0)
        {
            if (_itemIndex >= _items.Length - 1) EquipItem(0);
            else EquipItem(_itemIndex + 1);
        }
        else if (Input.GetAxisRaw("Mouse ScrollWheel") < 0)
        {
            if (_itemIndex <= 0) EquipItem(_items.Length - 1);
            else EquipItem(_itemIndex - 1);
        }
    }

    private void Move()
    {
        Vector3 moveDir =
            new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;

        _moveAmount = Vector3.SmoothDamp(_moveAmount,
            moveDir * (Input.GetKey(KeyCode.LeftShift) ? _sprintSpeed : _walkSpeed),
            ref _smoothMoveVelocity, _smoothTime);

        _rigidbody.MovePosition(_rigidbody.position +
                                transform.TransformDirection(_moveAmount) * Time.fixedDeltaTime);
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

    void EquipItem(int itemIndex)
    {
        if (itemIndex == _previousItemIndex) return;
        _itemIndex = itemIndex;

        _items[_itemIndex].ItemGameObject.SetActive(true);
        if (_previousItemIndex != -1)
        {
            _items[_previousItemIndex].ItemGameObject.SetActive(false);
        }

        _previousItemIndex = _itemIndex;

        if (_photonView.IsMine)
        {
            Hashtable hashtable = new Hashtable();
            hashtable.Add(nameof(_itemIndex), _itemIndex);
            PhotonNetwork.LocalPlayer.SetCustomProperties(hashtable);
        }
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
    {
        if (!photonView.IsMine && targetPlayer == photonView.Owner)
        {
            EquipItem((int) changedProps[nameof(_itemIndex)]);
            print((int) changedProps[nameof(_itemIndex)]);
        }
    }

    public void TakeDamage(float amountOfDamage)
    {
        _photonView.RPC(nameof(RPC_TakeDamage), RpcTarget.All, amountOfDamage);
    }

    [PunRPC]
    void RPC_TakeDamage(float amountOfDamage)
    {
        if (!_photonView.IsMine) return;
        print($"You took damage {amountOfDamage}");

        _currentHealth -= amountOfDamage;
        if (_currentHealth <= 0) Die();
    }

    private void Die()
    {
        _playerManager.Die();
    }
}