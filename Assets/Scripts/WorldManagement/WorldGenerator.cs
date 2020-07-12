using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WorldGenerator : MonoBehaviour
{
    [Tooltip("The Tilemap to draw onto")]
    public Tilemap tilemap;
    
    [Tooltip("The tile to draw")]
    public TileBase tile1;
    public TileBase tile2;
    public TileBase tile3;

    // -- World and grid items -- //
    public WorldGrid world = new WorldGrid();
    public Transform LandLayer;
    public Transform ObstacleLayer;
    
    #region Key Locations
    // Starting Zone Related Objects
    public Tilemap StartingZonePrefab;
    Zone StartingZone;

    // Fortress Related Objects
    public Tilemap FortressPrefab;
    Zone FortressZone;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
         
        // Initialize some locations.
        Vector3Int startingZoneSpawnLocation = new Vector3Int(0, 0, 0);
        Vector3Int fortressSpawnLocation = new Vector3Int(0, world.Height/3, 0);

        // Render the location visuals.
        // -- Make Zones from Prefabs -- //
        StartingZone = new Zone(world, SpecialType.Starter, LandLayer, null, null)
        {
            spawnLocation = startingZoneSpawnLocation,
            PrefabLand = StartingZonePrefab,
            PrefabWater = null,
            PrefabObstacles = null,
            tile1 = tile1,

        };

        FortressZone = new Zone(world, SpecialType.HomeFortress, LandLayer, null, null)
        {
            spawnLocation = fortressSpawnLocation,
            PrefabLand = FortressPrefab,
            PrefabWater = null,
            PrefabObstacles = null,
            tile1 = tile1,
        };

        // Generate the path between the starting area and the fortress. You gotta be able to get to one another.
        PathFromZoneToZone(StartingZone, FortressZone);
        

        
    }

    

    void PathFromZoneToZone(Zone startZone, Zone endZone)
    {
        // Determine direction from start to end, which also determines which edge/corner to use.
        // Consequencely, this determines the endzone edge/corner to use.
        HexDirection pathDirection = Direction.GetDirection(startZone.spawnLocation, endZone.spawnLocation);

        // TODO Using a list of surrounding areas, pick one that's on the proper edge for start.
        startZone.GetSurroundingCells();
        // TODO Using a list of surrounding areas, pick one that's on the proper edge for end.
        // TODO Create path starting at start zone, and moving towards end, with some variance happening.



        
        // Pick a surrounding cell that's along the correct bounds, save it as path-start.



        
    }

    

    // Update is called once per frame
    void Update()
    {
        
    }
}
