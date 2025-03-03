using Zenject;

namespace VGBootstrapService 
{
    public class BootstrapSystemInstaller : Installer<BootstrapSystemInstaller>
    {
        public override void InstallBindings()
        {
            Container
                .Bind<IBootstrap>()
                .To<Bootstrap>()
                .AsSingle();
        }
    }
}