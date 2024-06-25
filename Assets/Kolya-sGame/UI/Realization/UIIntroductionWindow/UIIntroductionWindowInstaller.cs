using Zenject;

public class UIIntroductionWindowInstaller : Installer<UIIntroductionWindowInstaller>
{
    public override void InstallBindings()
    {
        Container
            .Bind<UIIntroductionWindowController>()
            .AsSingle();
    }
}