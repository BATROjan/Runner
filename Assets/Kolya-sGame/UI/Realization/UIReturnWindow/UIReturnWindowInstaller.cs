using Zenject;

public class UIReturnWindowInstaller : Installer<UIReturnWindowInstaller>
{
    public override void InstallBindings()
    {
        Container
            .Bind<UIReturnWindowController>()
            .AsSingle();
    }
}
