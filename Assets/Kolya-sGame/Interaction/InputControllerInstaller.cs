using UnityEngine;
using Zenject;

namespace Kolya_sGame.Interaction
{
    public class InputControllerInstaller: Installer<InputControllerInstaller>
    {
        public override void InstallBindings()
        {
            Container
                .Bind<IInputController>()
                .To<BaseInputController>()
                .AsSingle()
                .NonLazy();
        }
    }
}