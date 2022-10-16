using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Zenject;

public class LobbyMenuView : MonoBehaviour
{
    [SerializeField] private RectTransform _loadingMenu;
    [SerializeField] private RectTransform _lobbyButtons;
    [SerializeField] private RectTransform _createRoomMenu;
    
    [SerializeField] private RectTransform _roomMenu;
    [SerializeField] private Button _findRoom;
    [SerializeField] private Button _createRoom;
    [SerializeField] private Button _quitGame;
    [SerializeField] private Button _createRoomWithName;
    [SerializeField] private TMP_InputField _roomInputField;
    [SerializeField] private TextMeshProUGUI _roomName;
    [SerializeField] private Button _leaveRoom;

    
    private RectTransform[] _availableRectTransforms;
    public TMP_InputField RoomInputField => _roomInputField;

    private UIController _uiController;
    private Launcher _launcher;


    [Inject]
    void InitInject(UIController uiController, Launcher launcher)
    {
        _launcher = launcher;
        _uiController = uiController;
        _availableRectTransforms = new[]
            {_loadingMenu, _lobbyButtons, _createRoomMenu, _roomMenu};
    }

    private void OnEnable()
    {
        Launcher.OnConnectedToMasterAction += OpenLobbyButtons;
        _createRoom.onClick.AddListener(CreateRoom);
        _createRoomWithName.onClick.AddListener(_launcher.CreateRoom);
        Launcher.OnRoomCreated += Loading;
        Launcher.OnJoinedRoomAction += OpenRoomMenu;
        _leaveRoom.onClick.AddListener(LeaveRoom);
    }

    private void LeaveRoom()
    {
        _launcher.LeaveRoom();
        Loading();
    }


    private void OnDisable()
    {
        Launcher.OnConnectedToMasterAction -= OpenLobbyButtons;
        _createRoom.onClick.RemoveListener(CreateRoom);
        _createRoomWithName.onClick.RemoveListener(_launcher.CreateRoom);
        Launcher.OnRoomCreated -= Loading;
        Launcher.OnJoinedRoomAction -= OpenRoomMenu;
        _leaveRoom.onClick.RemoveListener(_launcher.LeaveRoom);
    }

    private void Loading()
    {
        _uiController.SelectActiveUI(_loadingMenu, _availableRectTransforms);
    }


    private void OpenLobbyButtons()
    {
        _uiController.SelectActiveUI(_lobbyButtons, _availableRectTransforms);
    }

    private void CreateRoom()
    {
        _uiController.SelectActiveUI(_createRoomMenu, _availableRectTransforms);
    }
    
    private void OpenRoomMenu()
    {
        _uiController.SelectActiveUI(_roomMenu, _availableRectTransforms);
        _uiController.SetText(_roomName, _roomInputField.text);
    }
}