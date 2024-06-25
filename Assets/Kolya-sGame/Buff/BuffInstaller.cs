using System.ComponentModel;
using Kolya_sGame.Application.Consts;
using Kolya_sGame.Player;
using Zenject;

namespace Kolya_sGame.Buff
{
    public class BuffInstaller : Installer<BuffInstaller>
    {
        public override void InstallBindings()
        {
            Container
                .Bind<BuffConfig>()
                .FromScriptableObjectResource(ResourcesConsts.BuffConfig)
                .AsSingle()
                .NonLazy();
            
            Container
                .BindMemoryPool<BuffView, BuffView.Pool>()
                .FromComponentInNewPrefabResource(ResourcesConsts.BuffView);

            Container
                .Bind<BuffController>()
                .AsSingle()
                .NonLazy();
        }
    }
}