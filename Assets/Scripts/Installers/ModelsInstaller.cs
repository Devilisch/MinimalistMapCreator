using UnityEngine;
using Zenject;

public class ModelsInstaller : MonoInstaller
{
    [SerializeField] private TextAsset buildingsJSON;
    [SerializeField] private TextAsset groundsJSON;

    public override void InstallBindings()
    {
        Container.Bind<Buildings>().FromNew().AsSingle().WithArguments( buildingsJSON ).NonLazy();
        Container.Bind<Grounds>().FromNew().AsSingle().WithArguments( groundsJSON ).NonLazy();
    }
}