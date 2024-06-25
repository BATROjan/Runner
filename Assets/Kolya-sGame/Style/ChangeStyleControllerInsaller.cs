using Kolya_sGame.Style;
using UnityEngine;
using Zenject;

public class ChangeStyleControllerInsaller : Installer<ChangeStyleControllerInsaller>
{
    public override void InstallBindings()
    {
        Container
            .Bind<ChangeStyleController>()
            .AsSingle()
            .NonLazy();
    }
}
