using System;
using Photon.Pun.Demo.Cockpit;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Views;
using Zenject;

public class GlobalView : MonoBehaviour
{
    [SerializeField] private LoadingMenuView _loadingMenuView;
    [SerializeField] private LobbyButtonsView _lobbyButtonsView;
    [SerializeField] private CreateRoomMenuView _createRoomMenuView;
    [SerializeField] private RoomMenuView _roomMenuView;
    [SerializeField] private FindRoomView _roomListView;
    public RoomMenuView RoomMenuView => _roomMenuView;

    public FindRoomView FindRoomView => _roomListView;

    
    public CreateRoomMenuView CreateRoomMenuView => _createRoomMenuView;

    public LobbyButtonsView LobbyButtonsView => _lobbyButtonsView;

    public LoadingMenuView LoadingMenuView => _loadingMenuView;





    

    public RectTransform RoomList => _roomListView.RoomList;
    public RoomListItem RoomListItem => _roomListView.RoomListItem;
    
    public Button LeaveRoomsList => _roomListView.LeaveRoomsList;



   



  

    
}