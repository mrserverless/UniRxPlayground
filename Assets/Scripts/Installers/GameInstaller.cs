using System;
using UniRx.WebSocket;
using Zenject;

public class GameInstaller : MonoInstaller<GameInstaller>
{
    public override void InstallBindings()
    {
//        Container.BindInstance().WithId("websocket_url");
        Container.Bind<IObservableWS>().To<RxWebSocketSharp>().AsSingle();
    }


}