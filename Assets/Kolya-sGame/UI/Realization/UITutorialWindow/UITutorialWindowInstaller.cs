using Zenject;

public class UITutorialWindowInstaller : Installer<UITutorialWindowInstaller>
{
    public override void InstallBindings()
    {
        Container
            .Bind<UITutorialWindowController>()
            .AsSingle();
    }
}
