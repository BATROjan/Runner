using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SystemScoreInstaller : Installer<SystemScoreInstaller>
{
    public override void InstallBindings()
    {
        Container
            .Bind<ISystemScore>()
            .To<SystemScore>()
            .AsSingle();
    }
}
