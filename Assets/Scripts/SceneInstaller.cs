using System.Reflection;
using Zenject;

public class SceneInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        print(MethodBase.GetCurrentMethod());


        Container.Bind<UIController>().AsSingle();
    }
}