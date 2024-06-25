using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SaveDataSystemInstaller : Installer<SaveDataSystemInstaller>
{
    public override void InstallBindings()
    {
        Container
            .Bind<SaveDataSystem>()
            .AsSingle();
    }
}
