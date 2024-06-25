using Kolya_sGame.Application.Consts;
using Kolya_sGame.UI.Realization.UIStartWindow;
using VGUIService;
using Zenject;

public class UIInstaller : Installer<UIInstaller>
{
    public override void InstallBindings()
    {
        Container
            .Bind<IUIRoot>()
            .FromComponentInNewPrefabResource(ResourcesConsts.UIRootSource)
            .AsSingle();
        
        Container
            .Bind<IUIService>()
            .To<StarMasterUIService>()
            .AsSingle();
        UIBlackoutWindowInstaller
            .Install(Container);
        UIIntroductionWindowInstaller
            .Install(Container);
        UIStartWindowInstaller
            .Install(Container);
        UIPlayingSceneWindowInstaller
            .Install(Container);
        UIRestartWindowInstaller
            .Install(Container);
        UIPauseWindowInstaller
            .Install(Container);
        UIReturnWindowInstaller
            .Install(Container);
        UIExitInstaller
            .Install(Container);
        UISettingsWindowInstaller
            .Install(Container);
        UITutorialWindowInstaller
            .Install(Container);
    }
}
