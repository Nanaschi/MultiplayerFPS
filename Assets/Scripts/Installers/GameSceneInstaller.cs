using System.Reflection;
using Controllers;
using UnityEngine;
using Views;
using Zenject;

public class GameSceneInstaller : MonoInstaller
{
    [SerializeField]
    private SpawnManager _spawnManager;

    public override void InstallBindings()
    {
        Container.BindInstance(_spawnManager).AsSingle();
        print(_spawnManager.name);
    }
}