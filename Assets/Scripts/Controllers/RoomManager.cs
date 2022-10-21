using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomManager : MonoBehaviourPunCallbacks
{
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public override void OnEnable()
    {
        base.OnEnable();
        SceneManager.sceneLoaded += SceneLoaded;
    }



    public override void OnDisable()
    {
        base.OnDisable();
        SceneManager.sceneLoaded -= SceneLoaded;
    }
    
    private void SceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        if (arg0.buildIndex == (int) ScenesEnum.Game)
        {
            PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerManager"),
                Vector3.zero, Quaternion.identity);
        }
    }
    
    
}
