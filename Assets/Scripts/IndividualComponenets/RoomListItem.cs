using System;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RoomListItem : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private TextMeshProUGUI _textMeshProUGUI;
    [SerializeField] private Button _button;

    public Button Button => _button;

    private RoomInfo _roomInfo;

    public void SetRoomsItems(RoomInfo roomInfo)
    {
        _roomInfo = roomInfo;

        _textMeshProUGUI.text = _roomInfo.Name;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        print("You clicked on me ");
    }
}