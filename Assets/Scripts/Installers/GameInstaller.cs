using System;
using UniRx.WebSocket;
using Zenject;

public class GameInstaller : MonoInstaller<GameInstaller>
{
    public override void InstallBindings()
    {
        Container.Bind<IObservableWS>().To<RxWebSocketSharp>().AsSingle();
    }


}