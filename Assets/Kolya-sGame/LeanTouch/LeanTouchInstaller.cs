using HabilectMoveOut.Application;
using Zenject;

public class LeanTouchInstaller : Installer<LeanTouchInstaller>
{
    public override void InstallBindings()
    {
        Container
            .Bind<SwipeDown>()
            .FromComponentInNewPrefabResource("SwipeDown")
            .UnderTransformGroup(TransformGroupConsts.LeanTouchTransformGroup)
            .AsSingle()
            .NonLazy();
    }
}
