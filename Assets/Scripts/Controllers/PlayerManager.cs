using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Controllers;
using Photon.Pun;
using UnityEngine;
using Zenject;

public class PlayerManager : MonoBehaviour
{
    private PhotonView _photonView;
    private GameObject _controller;
    private SpawnManager _spawnManager;

    [Inject]
    void InitInject(SpawnManager spawnManager)
    {
        _spawnManager = spawnManager;
    }
    
    private void Awake()
    {
        _photonView = GetComponent<PhotonView>();
    }

    private void Start()
    {
        if (!_photonView.IsMine) return;
        CreateController();
    }

    private void CreateController()
    {
        var randomSpawnPoint = SpawnManager.Instance.GetRandomSpawnPoint();

        _controller = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerController"),
            randomSpawnPoint.position,randomSpawnPoint.rotation, 0, new object[]{_photonView.ViewID});        
    }

    public void Die()
    {
        PhotonNetwork.Destroy(_controller);
        CreateController();
    }
}
