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


    public Button LeaveRoom => _leaveRoom;

    public TextMeshProUGUI RoomName => _roomName;

    public Button CreateRoomWithName => _createRoomWithName;

    public Button QuitGame => _quitGame;

    public Button CreateRoom => _createRoom;

    public Button FindRoom => _findRoom;

    public RectTransform RoomMenu => _roomMenu;

    public RectTransform CreateRoomMenu => _createRoomMenu;

    public RectTransform LobbyButtons => _lobbyButtons;

    public RectTransform LoadingMenu => _loadingMenu;

    public TMP_InputField RoomInputField => _roomInputField;
}