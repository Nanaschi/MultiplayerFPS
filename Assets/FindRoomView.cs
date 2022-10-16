using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FindRoomView : MonoBehaviour
{
    [SerializeField] private RoomListItem _roomListItem;
    [SerializeField] private RectTransform _roomListPlaceHolder;
    [SerializeField] private RectTransform _roomList;
    [SerializeField] private Button leaveRoomsList;

    public Button LeaveRoomsList => leaveRoomsList;

    public RectTransform RoomList => _roomList;

    public RectTransform RoomListPlaceHolder => _roomListPlaceHolder;

    public RoomListItem RoomListItem => _roomListItem;
}
