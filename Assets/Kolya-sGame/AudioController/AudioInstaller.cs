using HabilectMoveOut.Application;
using Kolya_sGame.Application.Consts;
using Zenject;

public class AudioInstaller : Installer<AudioInstaller>
{
    public override void InstallBindings()
    {
        Container
            .Bind<AudioModelConfig>()
            .FromScriptableObjectResource(ResourcesConsts.AudioModelConfig)
            .AsSingle()
            .NonLazy();
        
        Container
            .BindMemoryPool<AudioView, AudioView.Pool>()
            .WithInitialSize(10)
            .FromComponentInNewPrefabResource(ResourcesConsts.AudioView)
            .UnderTransformGroup(TransformGroupConsts.AudioViewTransformGroup);
        
        Container
            .Bind<IAudioController>()
            .To<AudioController>()
            .AsSingle();
    }
}
