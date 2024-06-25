using Zenject;

namespace Kolya_sGame.Camera
{
    public class CameraInstaller : Installer<CameraInstaller>
    {
        public override void InstallBindings()
        {
            Container
                .Bind<CameraController>()
                .AsSingle()
                .NonLazy();
        }
    }
}