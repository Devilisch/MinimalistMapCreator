using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Buildings
{
    private BuildingsArray buildingsArray;
    private Dictionary<Building, Sprite> buildingsDictionary = new Dictionary<Building, Sprite>();

    public Buildings( TextAsset json )
    {
        buildingsArray = JsonUtility.FromJson<BuildingsArray>( json.text );

        foreach ( var building in BuildingsArray )
        {
            string path = building.resourcePath;
            Sprite buildingSprite = Resources.Load<Tile>( path ).sprite;

            if ( !buildingsDictionary.ContainsKey( building ) )
                buildingsDictionary.Add( building, buildingSprite );
        }
    }

    public Building[] BuildingsArray { get { return buildingsArray.buildings; } }

    public string GetName( Sprite buildingSprite )
    {
        foreach ( var buildingPair in buildingsDictionary )
            if ( buildingPair.Value.name == buildingSprite.name )
                return buildingPair.Key.name;

        return null;
    }

    public Building GetBuilding( Sprite buildingSprite )
    {
        foreach ( var buildingPair in buildingsDictionary )
            if ( buildingPair.Value.name == buildingSprite.name )
                return buildingPair.Key;

        return null;
    }

    public Sprite GetSprite( string buildingName )
    {
        foreach ( var buildingPair in buildingsDictionary )
            if ( buildingPair.Key.name == buildingName )
                return buildingPair.Value;

        return null;
    }

    public bool IsBuilding( Sprite buildingSprite )
    {
        foreach ( var buildingPair in buildingsDictionary )
            if ( buildingPair.Value.name == buildingSprite.name )
                return true;

        return false;
    }
}

[System.Serializable]
public class BuildingsArray
{
    public Building[] buildings;
}

[System.Serializable]
public class Building
{
    public string name;
    public string type;
    public string description;
    public string resourcePath;
}