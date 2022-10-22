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
            _globalView.FindRoomView.RoomList
        };

        _globalView.LobbyButtonsView.CreateRoom.onClick.AddListener(LaunchCreateRoomMenu);
        _globalView.LobbyButtonsView.FindRoom.onClick.AddListener(LaunchFindRoom);
        _globalView.CreateRoomMenuView.CreateRoomWithName.onClick.AddListener(CreateRoom);
        _globalView.CreateRoomMenuView.Leave.onClick.AddListener(LaunchLobbyButtonsFirstTime);
        _globalView.RoomMenuView.LeaveRoom.onClick.AddListener(LeaveRoom);
        _globalView.RoomMenuView.StartGame.onClick.AddListener(StartGame);
        _globalView.FindRoomView.LeaveRoom.onClick.AddListener(LeaveRoomList);
    }

    private void LeaveRoomList()
    {
        OpenLobbyButtons();
    }

    private void LaunchFindRoom()
    {
        SelectActiveUI(_globalView.FindRoomView.RoomList, _availableRectTransforms);
    }


    private bool IsRoomInputFieldFilled =>
        string.IsNullOrWhiteSpace(_globalView.CreateRoomMenuView.RoomInputField.text);

    private string GetRoomInputFieldText =>
        _globalView.CreateRoomMenuView.RoomInputField.text;

    public void LaunchLoading() =>
        SelectActiveUI(_globalView.LoadingMenuView.LoadingMenu, _availableRectTransforms);

    public void LaunchLobbyButtonsFirstTime()
    {
        OpenLobbyButtons();
        PhotonNetwork.NickName = "Player " + Random.Range(0, 1000).ToString("0000");
    }

    private void OpenLobbyButtons()
    {
        SelectActiveUI(_globalView.LobbyButtonsView.LobbyButtons, _availableRectTransforms);
    }


    private void LaunchCreateRoomMenu()
    {
        SelectActiveUI(_globalView.CreateRoomMenuView.CreateRoomMenu, _availableRectTransforms);
    }
       


    public void OpenRoomMenu()
    {
        SelectActiveUI(_globalView.RoomMenuView.RoomMenu, _availableRectTransforms);
        SetText(_globalView.RoomMenuView.RoomName,
            _globalView.CreateRoomMenuView.RoomInputField.text);
    }

    public void OpenRoomMenu(string currentRoomName)
    {
        SelectActiveUI(_globalView.RoomMenuView.RoomMenu, _availableRectTransforms);
        SetText(_globalView.RoomMenuView.RoomName, currentRoomName);
        DisplayGameButtonForMaster();
    }

    public void DisplayGameButtonForMaster()
    {
        _globalView.RoomMenuView.StartGame.gameObject.SetActive(PhotonNetwork.IsMasterClient);
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


    private RoomListItem GetRoomListItem => _globalView.FindRoomView.RoomListItem;

    private Transform GetRoomListItemPlaceHolder =>
        _globalView.FindRoomView.RoomListPlaceHolder;


    public void UpdateRoomsList(IEnumerable<RoomInfo> roomInfos)
    {
        DestroyAllRoomListItems();
        InstantiateActiveRooms(roomInfos);
    }




    public void InstantiatePlayerListItem(Player player)
    {
        Object.Instantiate(_globalView.RoomMenuView.PlayerListItem,
            _globalView.RoomMenuView.PlayerListPlaceHolder).SetPlayerListItem(player);
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

    private void StartGame()
    {
        PhotonNetwork.LoadLevel((int)ScenesEnum.Game);
    }

    private void CreateRoom()
    {
        if (IsRoomInputFieldFilled) return;
        Debug.Log(MethodBase.GetCurrentMethod());
        PhotonNetwork.CreateRoom(GetRoomInputFieldText, GettingRoomOptions());
        LaunchLoading();
    }

    private RoomOptions GettingRoomOptions(byte maxPlayers = 4)
    {
        RoomOptions options = new RoomOptions
        {
            MaxPlayers = maxPlayers,
            BroadcastPropsChangeToAll = true
        };
        return options;
    }

    private void InstantiateActiveRooms(IEnumerable<RoomInfo> roomList)
    {
        foreach (RoomInfo room in roomList)
        {
            if(room.RemovedFromList) continue;
            var roomListItem = Object.Instantiate(GetRoomListItem, GetRoomListItemPlaceHolder);
            roomListItem.SetRoomsItems(room);
            roomListItem.Button.onClick.AddListener(() => JoinRoom(room));
        }
    }


    private void JoinRoom(RoomInfo info)
    {
        Debug.Log($"You joined {info.Name}");
        PhotonNetwork.JoinRoom(info.Name);
        LaunchLoading();
    }


    public void UpdatePlayerList()
    {
        DestroyAllPlayers();
        InstantiateAllActivePlayers();
    }

    private void DestroyAllPlayers()
    {
        foreach (Transform rectTransform in _globalView.RoomMenuView.PlayerListPlaceHolder)
        {
            Object.Destroy(rectTransform.gameObject);
        }
    }

    private void InstantiateAllActivePlayers()
    {
        Player[] playerList = PhotonNetwork.PlayerList;
        var activePlayerList = playerList.Where(player => !player.IsInactive);

        foreach (var player in activePlayerList)
        {
            InstantiatePlayerListItem(player);
        }
    }
}