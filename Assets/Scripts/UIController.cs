using Photon.Realtime;
using TMPro;
using UnityEngine;
using Zenject;

public class UIController
{
    private LobbyMenuView _lobbyMenuView;


    private RectTransform[] _availableRectTransforms;
    private RoomInfo _roomInfo;

    public UIController(LobbyMenuView lobbyMenuView, Launcher launcher)
    {
        _lobbyMenuView = lobbyMenuView;


        _availableRectTransforms = new[]
        {
            _lobbyMenuView.LoadingMenu,
            _lobbyMenuView.LobbyButtons,
            _lobbyMenuView.CreateRoomMenu,
            _lobbyMenuView.RoomMenu,
            _lobbyMenuView.RoomList
        };

        _lobbyMenuView.CreateRoom.onClick.AddListener(LaunchCreateRoomMenu);
        _lobbyMenuView.FindRoom.onClick.AddListener(LaunchFindRoom);
        _lobbyMenuView.CreateRoomWithName.onClick.AddListener(launcher.CreateRoom);
        _lobbyMenuView.LeaveRoom.onClick.AddListener(launcher.LeaveRoom);
    }

    private void LaunchFindRoom() =>
        SelectActiveUI(_lobbyMenuView.RoomList, _availableRectTransforms);

    public bool IsRoomInputFieldFilled =>
        string.IsNullOrWhiteSpace(_lobbyMenuView.RoomInputField.text);

    public string GetRoomInputFieldText =>
        _lobbyMenuView.RoomInputField.text;

    public void LaunchLoading() =>
        SelectActiveUI(_lobbyMenuView.LoadingMenu, _availableRectTransforms);

    public void LaunchLobbyButtons() =>
        SelectActiveUI(_lobbyMenuView.LobbyButtons, _availableRectTransforms);


    public void LaunchCreateRoomMenu() =>
        SelectActiveUI(_lobbyMenuView.CreateRoomMenu, _availableRectTransforms);


    public void OpenRoomMenu()
    {
        SelectActiveUI(_lobbyMenuView.RoomMenu, _availableRectTransforms);
        SetText(_lobbyMenuView.RoomName, _lobbyMenuView.RoomInputField.text);
    }

    private void SelectActiveUI(params RectTransform[] rectTransform)
    {
        for (int i = 0; i < rectTransform.Length; i++)
        {
            rectTransform[i].gameObject.SetActive(i == 0);
        }
    }

    private void SelectActiveUI(RectTransform rectTransform, RectTransform[] rectTransforms)
    {
        for (int i = 0; i < rectTransforms.Length; i++)
        {
            rectTransforms[i].gameObject.SetActive(rectTransform == rectTransforms[i]);
        }
    }

    private void SetText(TextMeshProUGUI textMeshProUGUI, string targetText)
    {
        textMeshProUGUI.text = targetText;
    }

    public void CreateRoomItems()
    {
    }

    public RoomListItem GetRoomListItem => _lobbyMenuView.RoomListItem;
    public RectTransform GetRoomListItemPlaceHolder => _lobbyMenuView.RoomListPlaceHolder;
}