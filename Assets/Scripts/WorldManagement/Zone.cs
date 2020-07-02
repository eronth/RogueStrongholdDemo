using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class Zone : MonoBehaviour
{
    // Starting Coordinates
    public Vector3Int spawnLocation = new Vector3Int(0,0,0);

    // What is this zone's type?
    public SpecialType SpecialType;

    // This is the WorldGrid the zone will be in, useful for coordinate reasons.
    public WorldGrid World;

    // Tile for test reasons.
    public TileBase tile1;

    
    // Various prefab tilemaps.
    public Tilemap PrefabLand;
    public Tilemap PrefabWater;
    public Tilemap PrefabObstacles;
    
    // Various tilemaps, post generation.
    // Todo make public?
    private Tilemap Land;
    private Tilemap Water;
    private Tilemap Obstacles;

    private int NorthernBounds;
    private int SouthernBounds;
    private int EasternBounds;
    private int WesternBounds;

    // Lists generated here.
    private List<Vector2Int> surroundingCellCoordinates;
    private List<Vector2Int> containingCellCoordinates;

    public Zone (WorldGrid _world, SpecialType zoneSpecialType, Transform LandLayer, Transform WaterLayer, Transform ObstacleLayer)
    {
        World = _world;
        SpecialType = zoneSpecialType;

        InitializePrefabLocation(PrefabLand, spawnLocation, LandLayer, SpecialType);
    }

    Tilemap InitializePrefabLocation(Tilemap prefab, Vector3Int spawnLocation, Transform gridLayer, SpecialType st)
    {
        Tilemap tm = Instantiate(prefab, spawnLocation, Quaternion.identity, gridLayer);

        // Get an enumerator for all the cells within this area.
        BoundsInt.PositionEnumerator pe = tm.cellBounds.allPositionsWithin;
        do
        {
            if (tm.GetSprite(pe.Current) != null)
            {
                World.GetCell(pe.Current).special = st;
                containingCellCoordinates.Add(World.GetCell(pe.Current).Coordinates);
            }   
        } while (pe.MoveNext());

        return tm;
    }

    // TODO make this work wtih all the different tilemap layers.
    private void PopulateContainingCells()
    {
        Tilemap tm = Land;
        NorthernBounds = -10000000;
        SouthernBounds =  10000000;
        EasternBounds = -10000000;
        WesternBounds =  10000000;

        // Get an enumerator for all the cells within this area.
        BoundsInt.PositionEnumerator pe = tm.cellBounds.allPositionsWithin;
        do
        {
            if (tm.GetSprite(pe.Current) != null)
            {
                Vector2Int coords = World.GetCell(pe.Current).Coordinates;
                containingCellCoordinates.Add(coords);
                if (coords.x > NorthernBounds) { NorthernBounds = coords.x; }
                if (coords.x < SouthernBounds) { SouthernBounds = coords.x; }
                if (coords.y > EasternBounds) { EasternBounds = coords.y; }
                if (coords.y < WesternBounds) { WesternBounds = coords.y; }
            }   
        } while (pe.MoveNext());
    }

    // TODO make this work wtih all the different tilemap layers.
    private void PopulateSurroundingCells()
    {
        // If we gotta populate surrounding, make sure containing is up to date.
        if(containingCellCoordinates == null
        || containingCellCoordinates.Count == 0)
        {
            PopulateContainingCells();
        }
        
        foreach(Vector2Int cell in containingCellCoordinates) 
        {
            CheckAndAddSurroundingCell(World.GetCell(cell).Neighbor[(int)HexDirection.N].Coordinates);
            CheckAndAddSurroundingCell(World.GetCell(cell).Neighbor[(int)HexDirection.NE].Coordinates);
            CheckAndAddSurroundingCell(World.GetCell(cell).Neighbor[(int)HexDirection.NW].Coordinates);
            CheckAndAddSurroundingCell(World.GetCell(cell).Neighbor[(int)HexDirection.S].Coordinates);
            CheckAndAddSurroundingCell(World.GetCell(cell).Neighbor[(int)HexDirection.SE].Coordinates);
            CheckAndAddSurroundingCell(World.GetCell(cell).Neighbor[(int)HexDirection.SW].Coordinates);
        }
    }

    private void CheckAndAddSurroundingCell(Vector2Int cell)
    {
        if (cell != null && !containingCellCoordinates.Contains(cell) && !surroundingCellCoordinates.Contains(cell))
        {
            surroundingCellCoordinates.Add(cell);
        }
    }

    // Return a list of populated cells
    public List<Vector2Int> GetSurroundingCells()
    {
        if (surroundingCellCoordinates == null || surroundingCellCoordinates.Count == 0)
        {
            PopulateSurroundingCells();
        }

        return containingCellCoordinates;
    }

    // Return a list of surrounding cells.
    public List<Vector2Int> GetContainingCells()
    {  
        if (containingCellCoordinates == null || containingCellCoordinates.Count == 0)
        {
            PopulateContainingCells();
        }

        return containingCellCoordinates;
    }
}
