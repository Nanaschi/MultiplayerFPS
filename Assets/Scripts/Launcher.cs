using System;
using System.Collections.Generic;
using System.Reflection;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using Zenject;

public class Launcher : MonoBehaviourPunCallbacks
{
    private UIController _uiController;


    [Inject]
    void InitInject(UIController uiController)
    {
        _uiController = uiController;
    }

    private void Awake()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        print(MethodBase.GetCurrentMethod());

        PhotonNetwork.JoinLobby();

        _uiController.LaunchLoading();
    }

    public override void OnJoinedLobby()
    {
        print(MethodBase.GetCurrentMethod());
        _uiController.LaunchLobbyButtons();
    }

    public void CreateRoom()
    {
        print(MethodBase.GetCurrentMethod());
        if (_uiController.IsRoomInputFieldFilled) return;
        PhotonNetwork.CreateRoom(_uiController.GetRoomInputFieldText);
        _uiController.LaunchLoading();
    }

    public override void OnJoinedRoom()
    {
        _uiController.OpenRoomMenu();
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.LogError(message);
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        _uiController.LaunchLoading();
    }

    public override void OnLeftRoom()
    {
        print(MethodBase.GetCurrentMethod());
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        for (int i = 0; i < roomList.Count; i++)
        {
            Instantiate(_uiController.GetRoomListItem, _uiController.GetRoomListItemPlaceHolder)
                .SetRoomsItems(roomList[i]);
        }
    }


    public void JoinRoom(RoomInfo info)
    {
        PhotonNetwork.JoinRoom(info.Name);
        _uiController.LaunchLoading();
    }
}