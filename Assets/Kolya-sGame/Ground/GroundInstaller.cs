using System.ComponentModel;
using Kolya_sGame.Application.Consts;
using Kolya_sGame.Player;
using Zenject;

namespace Kolya_sGame.Ground
{
    public class GroundInstaller : Installer<GroundInstaller>
    {
        public override void InstallBindings()
        {
            Container
                .Bind<GroundConfig>()
                .FromScriptableObjectResource(ResourcesConsts.GroundConfig)
                .AsSingle()
                .NonLazy();
                                                                   
            Container
                .BindMemoryPool<GroundView, GroundView.Pool>()
                .FromComponentInNewPrefabResource(ResourcesConsts.GroundView);
                                                       
            Container
                .Bind<GroundController>()
                .AsSingle()
                .NonLazy();
        }
    }
}