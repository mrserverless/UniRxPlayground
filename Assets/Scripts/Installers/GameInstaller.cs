using Assets.Scripts.Observers;
using Zenject;

public class GameInstaller : MonoInstaller<GameInstaller>
{
    public override void InstallBindings()
    {
        Container.BindAllInterfacesAndSelf<Inputs>().FromGameObject();
//        Container.BindAllInterfacesAndSelf<PlayerController>().To<PlayerController>().AsSingle();
    }
}