using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Zenject;

public class TilemapViewModel : MonoBehaviour
{
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private TileBase workshopTile;
    [SerializeField] private BuildWindowViewModel builldWindow;
    [SerializeField] private BuildingWindowViewModel builldingWindow;
    [SerializeField] private InformationWindowViewModel informationWindow;
    [Inject] private Buildings buildings;
    [Inject] private Grounds grounds;

    private bool isTilemapActive = true;
    private bool isMoveMenuActive = false;
    private Vector3 defaultCameraPosition;
    private Vector3Int currentTilePosition;
    private Vector3Int previousTilePosition;
    private Vector3 currentTileCenter;
    private Sprite currentSprite;
    private Sprite previousSprite;

    private void Start()
    {
        defaultCameraPosition = Camera.main.transform.position;
    }

    private void Update()
    {
        if ( Input.GetMouseButtonDown( 0 ) && isTilemapActive )
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint( Input.mousePosition );

            currentTilePosition = tilemap.WorldToCell( mousePosition );

            currentTileCenter = new Vector3( 
                ( (Vector3)currentTilePosition + 0.5f * Vector3.one ).x * tilemap.cellSize.x,
                ( (Vector3)currentTilePosition + 0.5f * Vector3.one ).y * tilemap.cellSize.y,
                ( (Vector3)currentTilePosition + 0.5f * Vector3.one ).z * tilemap.cellSize.z
            );

            currentSprite = tilemap.GetSprite( currentTilePosition );

            if ( currentSprite == null )
                return;

            if ( !isMoveMenuActive )
            {
                isTilemapActive = false;

                if ( buildings.IsBuilding( currentSprite ) )
                    ShowBuildingWindow();

                if ( grounds.IsBuildingPlace( currentSprite ) )
                    ShowBuildWindow();

                if ( grounds.IsGround( currentSprite ) )
                {
                    Debug.Log( "Tap to Groud: name = " + currentSprite.name );
                    isTilemapActive = true;
                }

                if ( grounds.IsRoad( currentSprite ) )
                {
                    Debug.Log( "Tap to Road: name = " + currentSprite.name );
                    isTilemapActive = true;
                }
            }
            else
            {
                if ( buildings.IsBuilding( currentSprite ) )
                    SwapBuildings();

                if ( grounds.IsBuildingPlace( currentSprite ) )
                    MoveBuildingToCurrentPosition();
            }
        }
    }

    private void ShowBuildingWindow()
    {
        Debug.Log( "Tap to Building: name = " + currentSprite.name );

        Camera.main.transform.position = new Vector3( currentTileCenter.x, currentTileCenter.y, defaultCameraPosition.z );

        BuildingWindowViewModel window = Instantiate( builldingWindow );
        window.Init( OnWindowClosed, DeleteBuilding, MoveBuilding, GetBuildingInfo );
    }

    private void ShowBuildWindow()
    {
        Debug.Log( "Tap to Building place: name = " + currentSprite.name );

        BuildWindowViewModel window = Instantiate( builldWindow );
        window.Init( buildings, OnWindowClosed, Build );
    }

    private void DeleteBuilding()
    {
        Tile tile = new Tile();
        tile.sprite = grounds.BuildingPlace;
        tilemap.SetTile( currentTilePosition, tile );
    }

    private void MoveBuilding()
    {
        isMoveMenuActive = true;
        previousSprite = currentSprite;
        previousTilePosition = currentTilePosition;
    }
    private void SwapBuildings()
    {
        Sprite swapTileSprite = tilemap.GetSprite( currentTilePosition );

        SetBuilding( currentTilePosition, tilemap.GetSprite( previousTilePosition ) );
        SetBuilding( previousTilePosition, swapTileSprite );
        isMoveMenuActive = false;
    }

    private void MoveBuildingToCurrentPosition()
    {
        SetBuilding( currentTilePosition, tilemap.GetSprite( previousTilePosition ) );
        SetBuilding( previousTilePosition, grounds.BuildingPlace );
        isMoveMenuActive = false;
    }

    private void GetBuildingInfo()
    {
        InformationWindowViewModel window = Instantiate( informationWindow );
        window.Init( currentSprite, buildings.GetBuilding( currentSprite ), OnWindowClosed );
    }

    private void SetBuilding( Vector3Int position, Sprite sprite )
    {
        Tile tile = new Tile();
        tile.sprite = sprite;
        tilemap.SetTile( position, tile );
    }

    private void Build( Sprite sprite )
    {
        SetBuilding( currentTilePosition, sprite );
    }

    private void OnWindowClosed()
    {
        isTilemapActive = true;
        Camera.main.transform.position = defaultCameraPosition;
    }
}
