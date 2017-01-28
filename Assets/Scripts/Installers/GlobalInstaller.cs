using Character;
using UnityEngine;
using Zenject;

public class GlobalInstaller : MonoInstaller<GlobalInstaller>
{
    public override void InstallBindings()
    {
        Container.Bind<PlayerContext>().AsSingle();
    }
}