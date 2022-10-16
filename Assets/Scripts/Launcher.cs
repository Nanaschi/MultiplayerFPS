using System;
using System.Reflection;
using Photon.Pun;
using UnityEngine;
using Zenject;

public class Launcher : MonoBehaviourPunCallbacks
{
    private UIController _uiController;
    public static event Action OnConnectedToMasterAction;
    public static event Action OnRoomCreated;
    public static event Action OnJoinedRoomAction;


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

        OnConnectedToMasterAction?.Invoke();

        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        print(MethodBase.GetCurrentMethod());
    }

    public void CreateRoom()
    {
        if (_uiController.IsRoomInputFieldFilled) return;
        PhotonNetwork.CreateRoom(_uiController.GetRoomInputFieldText);
        OnRoomCreated?.Invoke();
    }

    public override void OnJoinedRoom()
    {
        OnJoinedRoomAction?.Invoke();
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.LogError(message);
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
        print(MethodBase.GetCurrentMethod());
        OnConnectedToMasterAction?.Invoke();
    }
}