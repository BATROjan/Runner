using Zenject;

public class UISettingsWindowInstaller : Installer<UISettingsWindowInstaller>
{
    public override void InstallBindings()
    {
        Container
            .Bind<UISettingsWindowController>()
            .AsSingle()
            .NonLazy();
    }
}
