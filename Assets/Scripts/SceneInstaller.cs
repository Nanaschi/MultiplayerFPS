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

        var uiController = new UIController(_lobbyMenuView, _launcher);
        Container.BindInstance(uiController).AsSingle();
        Container.BindInstance(_launcher).AsSingle();
        
    }
}