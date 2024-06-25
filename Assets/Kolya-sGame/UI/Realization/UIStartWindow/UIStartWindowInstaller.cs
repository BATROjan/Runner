using Zenject;

namespace Kolya_sGame.UI.Realization.UIStartWindow
{
    public class UIStartWindowInstaller : Installer<UIStartWindowInstaller>
    {
        public override void InstallBindings()
        {
            Container
                .Bind<UIStartWindowController>()
                .AsSingle()
                .NonLazy();
        }
    }
}