using System.Collections;
using System.Collections.Generic;
using Kolya_sGame.SceneObjectStorage;
using UnityEngine;
using VGCore;
using Zenject;

public class ApplicationBaseModulesInstaller : Installer<ApplicationBaseModulesInstaller>
{
    public override void InstallBindings()
    {
        Container
            .Bind<CommandStorage>()
            .AsSingle();
            
        Container
            .Bind<ISceneObjectsStorage>()
            .To<SceneObjectsStorage>()
            .AsSingle();
    }
}
