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
        if (_uiController.IsRoomInputFieldFilled) return;
        print(MethodBase.GetCurrentMethod());
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
        print(MethodBase.GetCurrentMethod());
        _uiController.LaunchLoading();
        PhotonNetwork.LeaveRoom();

    }

    public override void OnLeftRoom()
    {
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        _uiController.DestroyAllRoomListItems();
        print(MethodBase.GetCurrentMethod());
        foreach (var room in roomList)
        {
            Instantiate(_uiController.GetRoomListItem, _uiController.GetRoomListItemPlaceHolder)
                .SetRoomsItems(room);
        }
        
        Debug.LogWarning(roomList.Count);
    }


    public void JoinRoom(RoomInfo info)
    {
        PhotonNetwork.JoinRoom(info.Name);
        _uiController.LaunchLoading();
    }
}