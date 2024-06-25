using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class UIExitInstaller : Installer<UIExitInstaller>
{
    public override void InstallBindings()
    {
        Container
            .Bind<UIExitController>()
            .AsSingle()
            .NonLazy();
    }
}
