using System.Collections.Generic;
using UnityEngine;

public class Buildings
{
    private BuildingsData _buildingsData;
    private Dictionary<Building, Sprite> _buildingsDictionary = new Dictionary<Building, Sprite>();

    public Buildings( TextAsset json )
    {
        _buildingsData = JsonUtility.FromJson<BuildingsData>( json.text );

        foreach ( var buildingData in BuildingsData )
        {
            string path = buildingData.resourcePath;
            Sprite buildingSprite = Resources.Load<Sprite>( path );

            if ( !_buildingsDictionary.ContainsKey( buildingData ) )
                _buildingsDictionary.Add( buildingData, buildingSprite );
        }
    }

    public Building[] BuildingsData
    {
        get { return _buildingsData.data; } 
    }

    public Building GetBuilding( Sprite buildingSprite )
    {
        foreach ( var buildingPair in _buildingsDictionary )
            if ( buildingPair.Value.name == buildingSprite.name )
                return buildingPair.Key;

        return null;
    }

    public Sprite GetSprite( string buildingName )
    {
        foreach ( var buildingPair in _buildingsDictionary )
            if ( buildingPair.Key.name == buildingName )
                return buildingPair.Value;

        return null;
    }

    public bool IsBuilding( Sprite buildingSprite )
    {
        return GetBuilding( buildingSprite ) != null;
    }
}



[System.Serializable]
public class BuildingsData
{
    public Building[] data;
}



[System.Serializable]
public class Building
{
    public string name;
    public string type;
    public string description;
    public string resourcePath;
}