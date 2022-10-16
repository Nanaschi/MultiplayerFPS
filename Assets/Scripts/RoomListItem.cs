using Photon.Realtime;
using TMPro;
using UnityEngine;

public class RoomListItem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textMeshProUGUI;


    private RoomInfo _roomInfo;

    public void SetRoomsItems(RoomInfo roomInfo)
    {
        _roomInfo = roomInfo;

        _textMeshProUGUI.text = _roomInfo.Name;
    }
}