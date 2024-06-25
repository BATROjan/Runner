using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class UIPauseWindowInstaller : Installer<UIPauseWindowInstaller>
{
    public override void InstallBindings()
    {
        Container
            .Bind<UIPauseWindowController>()
            .AsSingle();
    }
}
