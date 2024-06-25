using HabilectMoveOut.Application;
using Kolya_sGame.Application.Consts;
using Zenject;

public class MaskInstaller : Installer<MaskInstaller>
{
    public override void InstallBindings()
    {
        Container
            .Bind<MaskRatioConfig>()
            .FromScriptableObjectResource(ResourcesConsts.MaskRatioConfig)
            .AsSingle();
        
        Container
            .BindMemoryPool<MaskView, MaskView.Pool>()
            .WithInitialSize(1)
            .FromComponentInNewPrefabResource(ResourcesConsts.MaskView)
            .UnderTransformGroup(TransformGroupConsts.MaskViewTransformGroup);

        Container
            .Bind<MaskController>()
            .AsSingle();
    }
}
