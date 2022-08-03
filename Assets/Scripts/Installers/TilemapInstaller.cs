using UnityEngine;
using UnityEngine.Tilemaps;
using Zenject;

public class TilemapInstaller : MonoInstaller
{
    [SerializeField] private Tilemap tilemap;

    public override void InstallBindings()
    {
        Container.Bind<Tilemap>().FromInstance( tilemap ).AsSingle();
    }
}