using System;
using Character;
using UniRx.WebSocket;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Installers
{
    [CreateAssetMenu(fileName = "SettingsInstaller", menuName = "Installers/SettingsInstaller")]
    public class SettingsInstaller : ScriptableObjectInstaller<SettingsInstaller>
    {
        public PlayerController.Config PlayerConfig;
        public CameraBob.Config CameraConfig;
        public RxWebSocketSharp.Config WebSocketConfig;

        public override void InstallBindings()
        {
            Container.BindInstance(PlayerConfig);
            Container.BindInstance(CameraConfig);
            Container.BindInstance(WebSocketConfig);

            Container.Bind<Uri>().FromMethod(WebSocketUrl).WhenInjectedInto<IObservableWS>();
        }

        private Uri WebSocketUrl(InjectContext context)
        {
            return new Uri(WebSocketConfig.url);
        }
    }
}