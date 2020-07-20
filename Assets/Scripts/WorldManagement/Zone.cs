using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class Zone : MonoBehaviour
{
    public string debugName = "default";
    // Starting Coordinates
    public Vector2Int spawnLocation = new Vector2Int(0,0);

    // What is this zone's type?
    public SpecialType SpecialType;

    // This is the WorldGrid the zone will be in, useful for coordinate reasons.
    public WorldGrid WorldGridHolder;

    // Tile for test reasons.
    public TileBase tile1;

    public bool IsSpawned = false;

    
    // Various prefab tilemaps.
    public Tilemap PrefabLand;
    public Tilemap PrefabWater;
    public Tilemap PrefabObstacles;
    
    // Various tilemaps, post generation.
    // Todo make public?
    private Tilemap LandMap;
    private Tilemap WaterMap;
    private Tilemap ObstaclesMap;

    // The layers the tilemaps are generated onto.
    Transform LandLayer;
    Transform WaterLayer;
    Transform ObstaclesLayer;

    // Y Direction Bounds
    private int NorthernBounds;
    private int SouthernBounds;
    // X Direction Bounds
    private int EasternBounds;
    private int WesternBounds;

    // Lists generated here.
    private List<Vector2Int> surroundingCellCoordinates = new List<Vector2Int>();
    private List<Vector2Int> containingCellCoordinates = new List<Vector2Int>();


    public Zone (WorldGrid _worldGridHolder, SpecialType zoneSpecialType,
         Transform _landLayer, Transform _waterLayer, Transform _obstacleLayer)
    {
        WorldGridHolder = _worldGridHolder;
        SpecialType = zoneSpecialType;
        ObstaclesLayer = _obstacleLayer;
        WaterLayer = _waterLayer;
        LandLayer = _landLayer;
    }

    public void Spawn ()
    {
        Vector3 targetWorldCoordinates = World.Grid.CellToWorld(new Vector3Int(spawnLocation.x, spawnLocation.y, 0));

        if(PrefabLand != null)
            LandMap = InitializePrefabLocation(PrefabLand, new Vector3(targetWorldCoordinates.x, targetWorldCoordinates.y, 0), LandLayer, SpecialType);

        if(PrefabObstacles != null)
            ObstaclesMap = InitializePrefabLocation(PrefabLand, new Vector3(targetWorldCoordinates.x, targetWorldCoordinates.y, 0), ObstaclesLayer, SpecialType);

        if(PrefabWater != null)
            WaterMap = InitializePrefabLocation(PrefabLand, new Vector3(targetWorldCoordinates.x, targetWorldCoordinates.y, 0), WaterLayer, SpecialType);

    }

    Tilemap InitializePrefabLocation(Tilemap prefab, Vector3 spawnLocation, Transform gridLayer, SpecialType st)
    {
        Tilemap tm = Instantiate(prefab, spawnLocation, Quaternion.identity, gridLayer);
        IsSpawned = true;

        // // Get an enumerator for all the cells within this area.
        // BoundsInt.PositionEnumerator pe = tm.cellBounds.allPositionsWithin;
        // do
        // {
        //     if (tm.GetSprite(pe.Current) != null)
        //     {
        //         WorldGridHolder.GetCell(pe.Current).special = st;
        //         containingCellCoordinates.Add(WorldGridHolder.GetCell(pe.Current).Coordinates);
        //     }   
        // } while (pe.MoveNext());

        return tm;
    }

    // TODO make this work wtih all the different tilemap layers.
    private void PopulateContainingCells()
    {
        // TODO throw error when not spawned?
        if (!IsSpawned)
            return;
        
        Tilemap tm = LandMap;
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
                Vector2Int coords = WorldGridHolder.GetCell(pe.Current).Coordinates;
                containingCellCoordinates.Add(coords);
                if (coords.y > NorthernBounds) { NorthernBounds = coords.y; }
                if (coords.y < SouthernBounds) { SouthernBounds = coords.y; }
                if (coords.x > EasternBounds) { EasternBounds = coords.x; }
                if (coords.x < WesternBounds) { WesternBounds = coords.x; }
            }   
        } while (pe.MoveNext());
    }

    public Vector2Int GetCenter()
    {
        if(containingCellCoordinates == null 
        || containingCellCoordinates.Count == 0)
        {
            PopulateContainingCells();
        }

        return new Vector2Int(
            (WesternBounds + EasternBounds) / 2,
            (NorthernBounds + SouthernBounds) / 2
        );
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
        
        foreach(Vector2Int cellCoordinates in containingCellCoordinates) 
        {
            WorldCell cell = WorldGridHolder.GetCell(cellCoordinates);
            if(cell != null)
            {
                if(cell.Neighbor[(int)HexDirection.N] != null)
                    CheckAndAddSurroundingCell(cell.Neighbor[(int)HexDirection.N].Coordinates);

                if(cell.Neighbor[(int)HexDirection.NE] != null)
                    CheckAndAddSurroundingCell(cell.Neighbor[(int)HexDirection.NE].Coordinates);
                
                if(cell.Neighbor[(int)HexDirection.NW] != null)
                    CheckAndAddSurroundingCell(cell.Neighbor[(int)HexDirection.NW].Coordinates);
                
                if(cell.Neighbor[(int)HexDirection.S] != null)
                    CheckAndAddSurroundingCell(cell.Neighbor[(int)HexDirection.S].Coordinates);

                if(cell.Neighbor[(int)HexDirection.SE] != null)
                    CheckAndAddSurroundingCell(cell.Neighbor[(int)HexDirection.SE].Coordinates);

                if(cell.Neighbor[(int)HexDirection.SW] != null)
                    CheckAndAddSurroundingCell(cell.Neighbor[(int)HexDirection.SW].Coordinates);
            }
        }
    }

    private void CheckAndAddSurroundingCell(Vector2Int cellCoordinates)
    {
        if(cellCoordinates == null)
            return;

        if (!containingCellCoordinates.Contains(cellCoordinates) && !surroundingCellCoordinates.Contains(cellCoordinates))
        {
            surroundingCellCoordinates.Add(cellCoordinates);
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
