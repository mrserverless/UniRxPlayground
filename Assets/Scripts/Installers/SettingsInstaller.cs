using Assets.Scripts.Character;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Installers
{
    [CreateAssetMenu(fileName = "SettingsInstaller", menuName = "Installers/SettingsInstaller")]
    public class SettingsInstaller : ScriptableObjectInstaller<SettingsInstaller>
    {
        public PlayerController.Config PlayerConfig;

        public override void InstallBindings()
        {
            Container.BindInstance(PlayerConfig);
        }
    }
}