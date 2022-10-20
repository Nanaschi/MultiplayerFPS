using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class FindRoomView : MonoBehaviour
{
    [SerializeField] private RoomListItem _roomListItem;
    [SerializeField] private RectTransform _roomListPlaceHolder;
    [SerializeField] private RectTransform _roomList;
    [FormerlySerializedAs("leaveRoom")] [FormerlySerializedAs("leaveRoomsList")] [SerializeField] private Button _leaveRoom;

    public Button LeaveRoom => _leaveRoom;

    public RectTransform RoomList => _roomList;

    public Transform RoomListPlaceHolder => _roomListPlaceHolder;

    public RoomListItem RoomListItem => _roomListItem;
}
