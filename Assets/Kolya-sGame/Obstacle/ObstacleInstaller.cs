using Kolya_sGame.Application.Consts;
using Kolya_sGame.Player;
using Zenject;

namespace Kolya_sGame.Obstacle
{
    public class ObstacleInstaller :Installer<ObstacleInstaller>
    {
        public override void InstallBindings()
        {
            Container
                .Bind<ObstacleConfig>()
                .FromScriptableObjectResource(ResourcesConsts.ObstacleConfig)
                .AsSingle()
                .NonLazy();
            
            Container
                .Bind<ObstacleColliderConfig>()
                .FromScriptableObjectResource(ResourcesConsts.ObstacleColliderConfig)
                .AsSingle()
                .NonLazy();
            
            Container
                .BindMemoryPool<ObstacleView, ObstacleView.Pool>()
                .FromComponentInNewPrefabResource(ResourcesConsts.Obstacle);

            Container
                .Bind<ObstacleController>()
                .AsSingle()
                .NonLazy();
            
            Container
                .Bind<ObstacleAnimationController>()
                .AsSingle()
                .NonLazy();
            
            Container
                .Bind<ObstacleTileConfig>()
                .FromScriptableObjectResource(ResourcesConsts.ObstacleTileConfig)
                .AsSingle()
                .NonLazy();

            Container
                .Bind<ObstacleTileController>()
                .AsSingle()
                .NonLazy();
        }
    }
}