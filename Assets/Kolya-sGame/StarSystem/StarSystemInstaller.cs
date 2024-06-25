using HabilectMoveOut.Application;
using Kolya_sGame.Application.Consts;
using Zenject;

public class StarSystemInstaller : Installer<StarSystemInstaller>
{
    public override void InstallBindings()
    {
        Container
            .BindMemoryPool<StarView, StarView.Pool>()
            .WithInitialSize(60)
            .FromComponentInNewPrefabResource(ResourcesConsts.StarView)
            .UnderTransformGroup(TransformGroupConsts.StarViewTransformGroup);

        Container
            .Bind<StarController>()
            .AsSingle()
            .NonLazy();
    }
}
