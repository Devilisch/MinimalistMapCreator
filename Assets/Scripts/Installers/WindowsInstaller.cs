using UnityEngine;
using Zenject;

public class WindowsInstaller : MonoInstaller
{
    [SerializeField] private BuildingPopupViewModel buildingPopup;
    [SerializeField] private BuildWindowViewModel buildWindow;
    [SerializeField] private InformationWindowViewModel informationWindow;

    public override void InstallBindings()
    {
        Container.Bind<BuildingPopupViewModel>().FromInstance( buildingPopup ).AsSingle();
        Container.Bind<BuildWindowViewModel>().FromInstance( buildWindow ).AsSingle();
        Container.Bind<InformationWindowViewModel>().FromInstance( informationWindow ).AsSingle();
    }
}