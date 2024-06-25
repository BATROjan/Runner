using Zenject;

public class UIBlackoutWindowInstaller : Installer<UIBlackoutWindowInstaller>
{
    public override void InstallBindings()
    {
        Container
            .Bind<UIBlackoutWindowController>()
            .AsSingle()
            .NonLazy();
    }
}
