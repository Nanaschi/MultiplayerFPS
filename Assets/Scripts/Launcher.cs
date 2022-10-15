using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Photon.Pun;
using TMPro;
using UnityEngine;
using Zenject;

public class Launcher : MonoBehaviourPunCallbacks
{
    private LobbyMenuView _lobbyMenuView;
    public static event Action OnConnectedToMasterAction;
    public static event Action OnRoomCreated;
    public static event Action OnJoinedRoomAction;

    [Inject]
    void InitInject(LobbyMenuView lobbyMenuView)
    {
        _lobbyMenuView = lobbyMenuView;
    }


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

    public void CreateRoom()
    {
        if(string.IsNullOrEmpty(_lobbyMenuView.RoomInputField.text)) return;
        PhotonNetwork.CreateRoom(_lobbyMenuView.RoomInputField.text);
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