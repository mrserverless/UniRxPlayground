using Assets.Scripts.Character;
using Assets.Scripts.Observables;
using Services;
using Zenject;

public class GameInstaller : MonoInstaller<GameInstaller>
{
    public override void InstallBindings()
    {
        Container.Bind<IWebSocketClient>().To<WebsocketSharpClient>().AsSingle();
    }
}