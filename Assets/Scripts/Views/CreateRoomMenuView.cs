using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CreateRoomMenuView: MonoBehaviour
{
    [SerializeField] private RectTransform _createRoomMenu;
    [SerializeField] private Button _createRoomWithName;
    [SerializeField] private TMP_InputField _roomInputField;
    public Button CreateRoomWithName => _createRoomWithName;

    public RectTransform CreateRoomMenu => _createRoomMenu;
    public TMP_InputField RoomInputField => _roomInputField;
}