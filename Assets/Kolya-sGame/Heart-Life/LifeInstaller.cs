using Zenject;

namespace Kolya_sGame.Heart_Life
{
    public class LifeInstaller : Installer<LifeInstaller>
    {
        public override void InstallBindings()
        {
            Container
                .Bind<LifeConfig>()
                .FromScriptableObjectResource("LifeConfig")
                .AsSingle()
                .NonLazy();

            Container
                .Bind<LifeController>()
                .AsSingle()
                .NonLazy();
        }
    }
}