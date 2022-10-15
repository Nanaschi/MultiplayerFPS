using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Photon.Pun;
using UnityEngine;

public class Launcher : MonoBehaviourPunCallbacks
{
    private void Awake()
    {
        
        print(MethodBase.GetCurrentMethod());
        
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        print(MethodBase.GetCurrentMethod());
        
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        print(MethodBase.GetCurrentMethod());
    }
}
