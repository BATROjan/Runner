using HabilectMoveOut.Application;
using Kolya_sGame.Application.Consts;
using Kolya_sGame.Buff;
using Kolya_sGame.Camera;
using Kolya_sGame.Ground;
using Kolya_sGame.Interaction;
using Kolya_sGame.Obstacle;
using Kolya_sGame.Player;
using Kolya_sGame.Timer;
using Lean.Touch;
using UnityEngine;
using VGBootstrapService;
using VGCore;
using Zenject;

namespace Kolya_sGame.Application.Installers
{
    public class ApplicationInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container
                .Bind<ParametersConfig>()
                .FromScriptableObjectResource(ResourcesConsts.ParametersConfig)
                .AsSingle()
                .NonLazy();
            
            TutorInstaller.Install(Container);
            LeanTouchInstaller.Install(Container);
            AudioInstaller.Install(Container);
            SaveDataSystemInstaller.Install(Container);
            UIInstaller.Install(Container);
            MaskInstaller.Install(Container);
            ApplicationBaseModulesInstaller.Install(Container);
            InputControllerInstaller.Install(Container);
            WorldInstaller.Install(Container);
            GroundInstaller.Install(Container);
            ChangeStyleControllerInsaller.Install(Container);
            SystemScoreInstaller.Install(Container);
            CameraInstaller.Install(Container);
            PlayerInstaller.Install(Container);
            ObstacleInstaller.Install(Container);
            TimerInstaller.Install(Container);
            BuffInstaller.Install(Container);
            StarSystemInstaller.Install(Container);
            
            Container
                .Bind<ApplicationLaunchBootstrap>()
                .AsSingle()
                .NonLazy();
            
            Container
                .Bind<GameController.GameController>()
                .AsSingle()
                .NonLazy();
        }
    }
}
