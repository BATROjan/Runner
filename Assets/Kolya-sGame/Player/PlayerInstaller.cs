using Kolya_sGame.Application.Consts;
using Kolya_sGame.Heart_Life;
using Zenject;

namespace Kolya_sGame.Player
{
    public class PlayerInstaller : Installer<PlayerInstaller>
    {
        public override void InstallBindings()
        {
            Container
                .Bind<PlayerConfig>()
                .FromScriptableObjectResource(ResourcesConsts.PlayerConfig)
                .AsSingle()
                .NonLazy();
            Container
                .Bind<PlayerColliderConfig>()
                .FromScriptableObjectResource(ResourcesConsts.PlayerColliderConfig)
                .AsSingle()
                .NonLazy();
            
            Container
                .BindMemoryPool<PlayerView, PlayerView.Pool>()
                .FromComponentInNewPrefabResource(ResourcesConsts.Player);

            Container
                .Bind<PlayerController>()
                .AsSingle()
                .NonLazy();
            
            LifeInstaller.Install(Container);
        }
    }
}