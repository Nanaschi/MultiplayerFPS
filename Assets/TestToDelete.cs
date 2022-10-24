using System;
using System.Collections;
using System.Collections.Generic;
using Controllers;
using UnityEngine;
using Zenject;

public class TestToDelete : MonoBehaviour
{
    private SpawnManager _spawnManager;

    [Inject]
    void Something(SpawnManager spawnManager)
    {
        _spawnManager = spawnManager;
    }

    private void Awake()
    {
        print(_spawnManager.gameObject.name);
    }
}
