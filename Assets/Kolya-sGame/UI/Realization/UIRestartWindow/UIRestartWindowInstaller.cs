using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class UIRestartWindowInstaller : Installer<UIRestartWindowInstaller>
{
    public override void InstallBindings()
    {
        Container
            .Bind<UIRestartWindowController>()
            .AsSingle()
            .NonLazy();
    }
}
