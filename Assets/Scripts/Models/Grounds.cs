using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Grounds
{
    private List<Sprite> _roadsDictionary = new List<Sprite>();

    public GroundsData GroundsData { get; private set; }
    public Sprite Ground { get; private set; }
    public Sprite BuildingPlace { get; private set; }

    public Grounds( TextAsset json )
    {
        GroundsData = JsonUtility.FromJson<GroundsData>( json.text );

        Ground = Resources.Load<Tile>( GroundsData.empty ).sprite;
        BuildingPlace = Resources.Load<Tile>( GroundsData.buildingPlace ).sprite;

        foreach ( var road in GroundsData.roads )
        {
            Sprite roadSprite = Resources.Load<Tile>( road ).sprite;

            if ( GetRoadSprite( roadSprite.name ) == null )
                _roadsDictionary.Add( roadSprite );
        }
    }

    public Sprite GetRoadSprite( string name )
    {
        foreach ( var road in _roadsDictionary )
            if ( road.name == name )
                return road;

        return null;
    }

    public bool IsRoad( Sprite sprite )
    {
        return GetRoadSprite( sprite.name ) != null;
    }

    public bool IsGround( Sprite sprite )
    {
        return Ground.name == sprite.name;
    }

    public bool IsBuildingPlace( Sprite sprite )
    {
        return BuildingPlace.name == sprite.name;
    }
}

[System.Serializable]
public class GroundsData
{
    public string empty;
    public string buildingPlace;
    public string[] roads;
}