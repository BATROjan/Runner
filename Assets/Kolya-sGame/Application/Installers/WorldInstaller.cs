using System.Collections;
using System.Collections.Generic;
using HabilectMoveOut.Application;
using Kolya_sGame.Application.Consts;
using Kolya_sGame.World;
using UnityEngine;
using Zenject;

public class WorldInstaller : Installer<WorldInstaller>
{
    public override void InstallBindings()
    {
        Container
            .Bind<WorldConfig>()
            .FromScriptableObjectResource(ResourcesConsts.WorldConfig)
            .AsSingle();
        
        Container
            .BindMemoryPool<WorldView, WorldView.Pool>()
            .WithInitialSize(15)
            .FromComponentInNewPrefabResource(ResourcesConsts.WorldView)
            .UnderTransformGroup(TransformGroupConsts.WorldViewTransformGroup);
        
        Container
            .Bind<IWorldController>()
            .To<WorldController>()
            .AsSingle();
    }
}
