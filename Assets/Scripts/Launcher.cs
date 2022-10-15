using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Photon.Pun;
using UnityEngine;
using Zenject;

public class Launcher : MonoBehaviourPunCallbacks
{
    public static event Action OnConnectedToMasterAction;
    public static event Action OnRoomCreated;
    public static event Action OnJoinedRoomAction;

    private void Awake()
    {
        print(MethodBase.GetCurrentMethod());

        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        print(MethodBase.GetCurrentMethod());

        OnConnectedToMasterAction?.Invoke();

        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        print(MethodBase.GetCurrentMethod());
    }

    public void CreateRoom(string roomName)
    {
        if(string.IsNullOrWhiteSpace(roomName)) return;
        PhotonNetwork.CreateRoom(roomName);
        OnRoomCreated?.Invoke();
    }

    public override void OnJoinedRoom()
    {
        OnJoinedRoomAction?.Invoke();
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        base.OnCreateRoomFailed(returnCode, message);
    }
}