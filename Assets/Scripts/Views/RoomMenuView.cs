using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Views
{
    public class RoomMenuView : MonoBehaviour
    {
        [SerializeField] private RectTransform _roomMenu;


        [SerializeField] private TextMeshProUGUI _roomName;
        [SerializeField] private Button _leaveRoom;

        public Button LeaveRoom => _leaveRoom;

        public TextMeshProUGUI RoomName => _roomName;


        public RectTransform RoomMenu => _roomMenu;
    }
}