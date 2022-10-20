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
        PhotonNetwork.JoinLobby();

        _uiController.LaunchLoading();
    }

    public override void OnJoinedLobby()
    {
        _uiController.LaunchLobbyButtonsFirstTime();
    }


    public override void OnJoinedRoom()
    {
        print(MethodBase.GetCurrentMethod());
        _uiController.OpenRoomMenu(PhotonNetwork.CurrentRoom.Name);
        _uiController.UpdatePlayerList();
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

        var activeRooms = roomList.Where(room => !room.RemovedFromList);
        for (int i = 0; i < roomList.Count; i++)
        {
            print(roomList[i].Name);
        }
  
        _uiController.UpdateRoomsList(roomList);
    }
    
    

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        print(MethodBase.GetCurrentMethod());
        _uiController.InstantiatePlayerListItem(newPlayer);
    }
    
    
}