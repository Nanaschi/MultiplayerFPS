using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using Views;


//DIVIDE into photon logic and UI
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
        _globalView.CreateRoomMenuView.CreateRoomWithName.onClick.AddListener(CreateRoom);
        _globalView.CreateRoomMenuView.Leave.onClick.AddListener(LaunchLobbyButtons);
        _globalView.RoomMenuView.LeaveRoom.onClick.AddListener(LeaveRoom);
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


    private void LaunchCreateRoomMenu() =>
        SelectActiveUI(_globalView.CreateRoomMenuView.CreateRoomMenu, _availableRectTransforms);


    public void OpenRoomMenu()
    {
        SelectActiveUI(_globalView.RoomMenuView.RoomMenu, _availableRectTransforms);
        SetText(_globalView.RoomMenuView.RoomName,
            _globalView.CreateRoomMenuView.RoomInputField.text);
    }

    public void OpenRoomMenuAlt(string currentRoomName)
    {
        SelectActiveUI(_globalView.RoomMenuView.RoomMenu, _availableRectTransforms);
        SetText(_globalView.RoomMenuView.RoomName, currentRoomName);
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


    private RoomListItem GetRoomListItem => _globalView.RoomListItem;

    private Transform GetRoomListItemPlaceHolder =>
        _globalView.FindRoomView.RoomListPlaceHolder;



    public void UpdateRoomsList(IEnumerable<RoomInfo> roomInfos)
    {
        DestroyAllRoomListItems();
        InstantiateActiveRooms(roomInfos);
    }

    private void DestroyAllRoomListItems()
    {
        foreach (Transform rectTransform in GetRoomListItemPlaceHolder)
        {
            Object.Destroy(rectTransform.gameObject);
        }
    }

    private void LeaveRoom()
    {
        Debug.Log(MethodBase.GetCurrentMethod());
        LaunchLoading();
        PhotonNetwork.LeaveRoom();
    }

    private void CreateRoom()
    {
        if (IsRoomInputFieldFilled) return;
        Debug.Log(MethodBase.GetCurrentMethod());
        PhotonNetwork.CreateRoom(GetRoomInputFieldText);
        LaunchLoading();
    }

    private void InstantiateActiveRooms(IEnumerable<RoomInfo> roomList)
    {
        
        var activeRoomList = roomList.Where(room => !room.RemovedFromList);
        foreach (RoomInfo room in activeRoomList)
        {
            var roomListItem = Object.Instantiate(GetRoomListItem, GetRoomListItemPlaceHolder);
                roomListItem.SetRoomsItems(room);
                roomListItem.Button.onClick.AddListener(() => JoinRoom(room));
        }
    }

    private void JoinRoom(RoomInfo info)
    {
        PhotonNetwork.JoinRoom(info.Name);
        LaunchLoading();
        Debug.Log(MethodBase.GetCurrentMethod());
    }
}