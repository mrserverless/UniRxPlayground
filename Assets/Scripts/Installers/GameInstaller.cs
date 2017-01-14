using Assets.Scripts.Observers;
using Zenject;

public class GameInstaller : MonoInstaller<GameInstaller>
{
    public override void InstallBindings()
    {
        Container.Bind<Inputs>().FromGameObject();
//        Container.BindAllInterfacesAndSelf<PlayerController>().To<PlayerController>().AsSingle();
    }
}