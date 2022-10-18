using UnityEngine;
using UnityEngine.UI;

namespace Views
{
    public class LobbyButtonsView : MonoBehaviour
    {
        [SerializeField] private Button _findRoom;
        [SerializeField] private Button _createRoom;
        [SerializeField] private Button _quitGame;
        [SerializeField] private RectTransform _lobbyButtons;
        public RectTransform LobbyButtons => _lobbyButtons;


        public Button QuitGame => _quitGame;

        public Button CreateRoom => _createRoom;

        public Button FindRoom => _findRoom;
    }
}