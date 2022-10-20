using System.Reflection;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;

public class PlayerListItem : MonoBehaviourPunCallbacks
{
    [SerializeField] private TextMeshProUGUI _textMeshProUGUI;

    private Player _player;

    public void SetPlayerListItem(Player player)
    {
        _player = player;
        _textMeshProUGUI.text = _player.NickName;
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        print("it does not go further");
        if (Equals(_player, otherPlayer))
        {
            print(MethodBase.GetCurrentMethod());
            Destroy(gameObject);
        }
    }

    public override void OnLeftRoom()
    {
        print(MethodBase.GetCurrentMethod());
        Destroy(gameObject);
    }
}