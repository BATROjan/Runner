using Kolya_sGame.Application.Consts;
using Kolya_sGame.Player;
using Zenject;

namespace Kolya_sGame.Timer
{
    public class TimerInstaller : Installer<TimerInstaller>
    {
        public override void InstallBindings()
        {
            Container
                .Bind<TimerConfig>()
                .FromScriptableObjectResource(ResourcesConsts.TimerConfig)
                .AsSingle()
                .NonLazy();
            
            Container
                .BindMemoryPool<TimerView, TimerView.Pool>()
                .FromComponentInNewPrefabResource(ResourcesConsts.TimerView);

            Container
                .Bind<TimerController>()
                .AsSingle()
                .NonLazy();
        }
    }
}