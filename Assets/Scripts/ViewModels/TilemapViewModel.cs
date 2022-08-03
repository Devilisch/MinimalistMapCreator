using UnityEngine;
using UnityEngine.Tilemaps;
using Zenject;

public class TilemapViewModel : MonoBehaviour
{
    [Inject] private Tilemap tilemap;
    [Inject] private Buildings buildings;
    [Inject] private Grounds grounds;
    [Inject] private BuildWindowViewModel builldWindow;
    [Inject] private BuildingPopupViewModel builldingWindow;
    [Inject] private InformationWindowViewModel informationWindow;

    private bool _isTilemapActive = true;
    private bool _isMoveMenuActive = false;
    private Vector3 _defaultCameraPosition;

    private TileInformation CurrentTile { get; set; }
    private TileInformation PreviousTile { get; set; }

    private void Start()
    {
        _defaultCameraPosition = Camera.main.transform.position;
        CurrentTile = new TileInformation();
        PreviousTile = new TileInformation();
    }

    private void Update()
    {
        if ( Input.GetMouseButtonDown( 0 ) && _isTilemapActive )
        {
            CurrentTile.RevalidateTileInformation( tilemap );

            if ( CurrentTile.Sprite == null )
                return;

            if ( _isMoveMenuActive )
            {
                SwapTiles();

                // // If we wanna parse each type of tiles ( comment previous "SwapTiles" )
                // if ( buildings.IsBuilding( CurrentTile.Sprite ) || grounds.IsBuildingPlace( CurrentTile.Sprite ) )
                //     SwapTiles();
            }
            else
            {
                _isTilemapActive = false;

                if ( buildings.IsBuilding( CurrentTile.Sprite ) )
                    ShowBuildingWindow();
                else
                    ShowBuildWindow();

                // // If we wanna parse each type of tiles ( comment "else" branch of previous "if" )
                // if ( grounds.IsBuildingPlace( CurrentTile.Sprite ) )
                //     ShowBuildWindow();

                // if ( grounds.IsGround( CurrentTile.Sprite ) )
                // {
                //     Debug.Log( "Tap to Groud: name = " + CurrentTile.Sprite.name );
                //     _isTilemapActive = true;
                // }

                // if ( grounds.IsRoad( CurrentTile.Sprite ) )
                // {
                //     Debug.Log( "Tap to Road: name = " + CurrentTile.Sprite.name );
                //     _isTilemapActive = true;
                // }
            }
        }
    }

    private void ShowBuildingWindow()
    {
        Debug.Log( "Tap to Building: name = " + CurrentTile.Sprite.name );

        LeanTween.move( Camera.main.gameObject, new Vector3( CurrentTile.CenterPosition.x, CurrentTile.CenterPosition.y, _defaultCameraPosition.z ), 0.2f );

        BuildingPopupViewModel window = Instantiate( builldingWindow );

        window.DeleteAction   = DeleteBuildingAction;
        window.MoveAction     = MoveBuildingAction;
        window.ShowInfoAction = ShowBuildingInfoAction;
        window.CloseAction    = CloseAction;
    }

    private void DeleteBuildingAction()
    {
        BuildBuilding( CurrentTile.Cell, grounds.BuildingPlace );
    }

    private void MoveBuildingAction()
    {
        _isMoveMenuActive = true;
        PreviousTile.Sprite = CurrentTile.Sprite;
        PreviousTile.Cell = CurrentTile.Cell;
    }

    private void ShowBuildingInfoAction()
    {
        InformationWindowViewModel window = Instantiate( informationWindow );

        window.Init( CurrentTile.Sprite, buildings.GetBuilding( CurrentTile.Sprite ) );
        window.CloseAction = CloseAction;
    }

    private void ShowBuildWindow()
    {
        Debug.Log( "Tap to Building place: name = " + CurrentTile.Sprite.name );

        BuildWindowViewModel window = Instantiate( builldWindow );

        window.Init( buildings, ( sprite ) => { BuildBuilding( CurrentTile.Cell, sprite ); } );
        window.CloseAction = CloseAction;
    }

    private void SwapTiles()
    {
        Sprite swapTileSprite = tilemap.GetSprite( CurrentTile.Cell );

        BuildBuilding( CurrentTile.Cell, tilemap.GetSprite( PreviousTile.Cell ) );
        BuildBuilding( PreviousTile.Cell, swapTileSprite );
        _isMoveMenuActive = false;
    }

    private void BuildBuilding( Vector3Int position, Sprite sprite )
    {
        Tile tile = new Tile();
        tile.sprite = sprite;
        tilemap.SetTile( position, tile );
    }

    private void CloseAction()
    {
        _isTilemapActive = true;

        if ( Camera.main.transform.position != _defaultCameraPosition )
            LeanTween.move( Camera.main.gameObject, _defaultCameraPosition, 0.2f );
    }
}



public class TileInformation
{
    public Vector3Int Cell { get; set; }
    public Vector3 CenterPosition { get; set; }
    public Sprite Sprite { get; set; }

    public TileInformation()
    {
        Cell = Vector3Int.zero;
        CenterPosition = Vector3.zero;
        Sprite = null;
    }

    public void RevalidateTileInformation( Tilemap tilemap )
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint( Input.mousePosition );

        Cell           = tilemap.WorldToCell( mousePosition );
        CenterPosition = tilemap.GetCellCenterWorld( Cell );
        Sprite         = tilemap.GetSprite( Cell );
    }
}
