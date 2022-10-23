using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Photon.Pun;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private PhotonView _photonView;
    private GameObject _controller;

    private void Awake()
    {
        _photonView = GetComponent<PhotonView>();
    }

    private void Start()
    {
        if (!_photonView.IsMine) return;
        CreateController();
    }

    private void CreateController()
    {
        print(MethodBase.GetCurrentMethod()  + $"{_photonView.InstantiationId}");
        
        _controller = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerController"),
            Vector3.zero, Quaternion.identity, 0, new object[]{_photonView.ViewID});        
    }

    public void Die()
    {
        PhotonNetwork.Destroy(_controller);
        CreateController();
    }
}
