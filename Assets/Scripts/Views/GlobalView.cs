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

    public RoomMenuView RoomMenuView => _roomMenuView;

    public CreateRoomMenuView CreateRoomMenuView => _createRoomMenuView;

    public LobbyButtonsView LobbyButtonsView => _lobbyButtonsView;

    public LoadingMenuView LoadingMenuView => _loadingMenuView;




    [SerializeField] private FindRoomView _roomListView;
    

    public RectTransform RoomList => _roomListView.RoomList;
    public RoomListItem RoomListItem => _roomListView.RoomListItem;
    public RectTransform RoomListPlaceHolder => _roomListView.RoomListPlaceHolder;
    public Button LeaveRoomsList => _roomListView.LeaveRoomsList;



   



  

    
}