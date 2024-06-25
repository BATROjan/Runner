using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class UIPlayingSceneWindowInstaller : Installer<UIPlayingSceneWindowInstaller>
{
    public override void InstallBindings()
    {
        Container
            .Bind<UIPlayingSceneWindowController>()
            .AsSingle();
    }
}
