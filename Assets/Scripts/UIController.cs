using Photon.Realtime;
using TMPro;
using UnityEngine;
using Views;

public class UIController
{
    private GlobalView _globalView;


    private RectTransform[] _availableRectTransforms;
    private RoomInfo _roomInfo;

    public UIController(GlobalView globalView, Launcher launcher)
    {
        _globalView = globalView;


        _availableRectTransforms = new[]
        {
            _globalView.LoadingMenuView.LoadingMenu,
            _globalView.LobbyButtonsView.LobbyButtons,
            _globalView.CreateRoomMenuView.CreateRoomMenu,
            _globalView.RoomMenuView.RoomMenu,
            _globalView.RoomList
        };

        _globalView.LobbyButtonsView.CreateRoom.onClick.AddListener(LaunchCreateRoomMenu);
        _globalView.LobbyButtonsView.FindRoom.onClick.AddListener(LaunchFindRoom);
        _globalView.CreateRoomMenuView.CreateRoomWithName.onClick.AddListener(launcher.CreateRoom);
        _globalView.RoomMenuView.LeaveRoom.onClick.AddListener(launcher.LeaveRoom);
        _globalView.LeaveRoomsList.onClick.AddListener(LaunchLobbyButtons);
    }

    private void LaunchFindRoom()
    {
        SelectActiveUI(_globalView.RoomList, _availableRectTransforms);
    }


    public bool IsRoomInputFieldFilled =>
        string.IsNullOrWhiteSpace(_globalView.CreateRoomMenuView.RoomInputField.text);

    public string GetRoomInputFieldText =>
        _globalView.CreateRoomMenuView.RoomInputField.text;

    public void LaunchLoading() =>
        SelectActiveUI(_globalView.LoadingMenuView.LoadingMenu, _availableRectTransforms);

    public void LaunchLobbyButtons() =>
        SelectActiveUI(_globalView.LobbyButtonsView.LobbyButtons, _availableRectTransforms);


    public void LaunchCreateRoomMenu() =>
        SelectActiveUI(_globalView.CreateRoomMenuView.CreateRoomMenu, _availableRectTransforms);


    public void OpenRoomMenu()
    {
        SelectActiveUI(_globalView.RoomMenuView.RoomMenu, _availableRectTransforms);
        SetText(_globalView.RoomMenuView.RoomName, _globalView.CreateRoomMenuView.RoomInputField.text);
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


    public RoomListItem GetRoomListItem => _globalView.RoomListItem;
    public RectTransform GetRoomListItemPlaceHolder => _globalView.RoomListPlaceHolder;

    public void DestroyAllRoomListItems()
    {
        foreach (RectTransform rectTransform in GetRoomListItemPlaceHolder)
        {
            Object.Destroy(rectTransform.gameObject);
        }
    }
}