using System;
using System.Reflection;
using Photon.Pun;
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
        print(MethodBase.GetCurrentMethod());

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
}