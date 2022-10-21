using System.Reflection;
using UnityEngine;
using Views;
using Zenject;

public class SceneInstaller : MonoInstaller
{
    [SerializeField] private GlobalView globalView;
    [SerializeField] private Launcher _launcher;

    public override void InstallBindings()
    {
        var uiController = new UIController(globalView, _launcher);
        Container.BindInstance(uiController).AsSingle();
        Container.BindInstance(_launcher).AsSingle();
    }
}