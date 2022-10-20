using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Views
{
    public class RoomMenuView : MonoBehaviour
    {
        [SerializeField] private RectTransform _roomMenu;
        [SerializeField] private RectTransform _playerListPlaceHolder;


        [SerializeField] private TextMeshProUGUI _roomName;
        [SerializeField] private Button _leaveRoom;
        [SerializeField] private Button _startGame;


        [SerializeField] private PlayerListItem _playerListItem;

        public Button StartGame => _startGame;
        public PlayerListItem PlayerListItem => _playerListItem;

        public RectTransform PlayerListPlaceHolder => _playerListPlaceHolder;

        public Button LeaveRoom => _leaveRoom;

        public TextMeshProUGUI RoomName => _roomName;


        public RectTransform RoomMenu => _roomMenu;
    }
}