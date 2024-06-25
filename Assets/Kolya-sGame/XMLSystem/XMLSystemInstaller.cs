using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class XMLSystemInstaller : Installer<XMLSystemInstaller>
{
    public override void InstallBindings()
    {
        Container
            .Bind<XMLSourceConfig>()
            .FromScriptableObjectResource("XMLSourceConfig")
            .AsSingle();

        Container
            .Bind<IXMLSystem>()
            .To<XMLSystem>()
            .AsSingle();
    }
}
