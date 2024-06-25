using Kolya_sGame.Application.Consts;
using Zenject;

public class TutorInstaller : Installer<TutorInstaller>
{
    public override void InstallBindings()
    {
        Container
            .Bind<TutorTextConfig>()
            .FromScriptableObjectResource(ResourcesConsts.TutorTextConfig)
            .AsSingle();
        
        Container
            .Bind<TutorController>()
            .AsSingle();
    }
}
