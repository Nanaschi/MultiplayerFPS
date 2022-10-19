using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RoomListItem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textMeshProUGUI;
    [SerializeField] private Button _button;

    public Button Button => _button;

    private RoomInfo _roomInfo;

    public TextMeshProUGUI TextMeshProUGUI
    {
        get => _textMeshProUGUI;
        set => _textMeshProUGUI = value;
    }

    public RoomInfo RoomInfo
    {
        get => _roomInfo;
        set => _roomInfo = value;
    }

    public void SetRoomsItems(RoomInfo roomInfo)
    {
        _roomInfo = roomInfo;

        _textMeshProUGUI.text = _roomInfo.Name;
    }
}