using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using Zenject;

public class Launcher : MonoBehaviourPunCallbacks
{
    private UIController _uiController;
    private List<RoomInfo> _roomList;


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


    public override void OnCreatedRoom()
    {
        print(MethodBase.GetCurrentMethod());
        _uiController.OpenRoomMenuAlt(PhotonNetwork.CurrentRoom.Name);
    }

    public override void OnJoinedRoom()
    {
        print(MethodBase.GetCurrentMethod());
        _uiController.OpenRoomMenuAlt(PhotonNetwork.CurrentRoom.Name);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.LogError(message);
    }


    public override void OnLeftRoom()
    {
        print(MethodBase.GetCurrentMethod());
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        
        print(MethodBase.GetCurrentMethod());
        _uiController.UpdateRoomsList(roomList);
    }
    
    
    
    
}