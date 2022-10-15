using System.Reflection;
using UnityEngine;
using Zenject;

public class SceneInstaller : MonoInstaller
{

    [SerializeField] private LobbyMenuView _lobbyMenuView;
    [SerializeField] private Launcher _launcher;
    public override void InstallBindings()
    {
        print(MethodBase.GetCurrentMethod());

        Container.BindInstance(_lobbyMenuView).AsSingle();
        Container.BindInstance(_launcher).AsSingle();
        Container.Bind<UIController>().AsSingle();
    }
}