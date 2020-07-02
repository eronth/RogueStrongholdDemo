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
        // TODO TODO connect starting zone to fortress via pathway.
        
        // todo determine the closest borders.

        // todo get surrounding cells
        // todo pick a surrounding cell on the correct border.


        startZone.GetSurroundingCells();
        // Pick a surrounding cell that's along the correct bounds, save it as path-start.

        // TODO repeat for ending location.



        // TODO get ending location bounds.
        // TODO have the path start at the starting location edge NEAREST to the ending location.
        // TODO have the starting path kinda zig and zag, trending TOWARDS the end location.


        // TODO create spidering biomes.
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
