using System;
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
    public Transform WaterLayer;
    public Transform LandLayer;
    public Transform ObstacleLayer;
    
    #region Key Locations
    // Starting Zone Related Objects
    public Tilemap StartingZonePrefab;
    Zone StartingZone;

    // Fortress Related Objects
    public Tilemap FortressPrefab;
    Zone FortressZone;

    // Test Related Objects
    public Tilemap TestPrefab;
    Zone TestZone;

    public Tilemap Test2Prefab;
    Zone Test2Zone;

    public Tilemap Test3Prefab;
    Zone Test3Zone;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        // todo remove this and replace with actual application.
        ObstacleLayer = LandLayer;
        WaterLayer = LandLayer;
         
        // Initialize some locations.
        Vector2Int startingZoneSpawnLocation = new Vector2Int(0, world.Center.y);
        Vector2Int fortressSpawnLocation = new Vector2Int(world.Height/3, world.Center.y);
        Vector2Int testingSpawnLocation = new Vector2Int(world.Center.x, world.Center.y);
        Vector2Int testing2SpawnLocation = new Vector2Int(world.Center.x+1, world.Center.y);
        Vector2Int testing3SpawnLocation = new Vector2Int(world.Center.x, world.Center.y+1); // green, should be above

        // Render the location visuals.
        // -- Make Zones from Prefabs -- //
        // StartingZone = new Zone(world, SpecialType.Starter, LandLayer, WaterLayer, ObstacleLayer)
        // {
        //     spawnLocation = startingZoneSpawnLocation,
        //     PrefabLand = StartingZonePrefab,
        //     PrefabWater = null,
        //     PrefabObstacles = null,
        //     tile1 = tile1,
        //     debugName = "StartZone",
        // };
        //StartingZone.Spawn();

        FortressZone = new Zone(world, SpecialType.HomeFortress, LandLayer, WaterLayer, ObstacleLayer)
        {
            spawnLocation = fortressSpawnLocation,
            PrefabLand = FortressPrefab,
            PrefabWater = null,
            PrefabObstacles = null,
            tile1 = tile1,
            debugName = "FortressZone",
        };
        FortressZone.Spawn();

        TestZone = new Zone(world, SpecialType.Debug, LandLayer, WaterLayer, ObstacleLayer)
        {
            spawnLocation = testingSpawnLocation,
            PrefabLand = TestPrefab,
            PrefabWater = null,
            PrefabObstacles = null,
            tile1 = tile1,
            debugName = "TestZone",
        };
        TestZone.Spawn();

        // Test2Zone = new Zone(world, SpecialType.Debug, LandLayer, WaterLayer, ObstacleLayer)
        // {
        //     spawnLocation = testing2SpawnLocation,
        //     PrefabLand = Test2Prefab,
        //     PrefabWater = null,
        //     PrefabObstacles = null,
        //     tile1 = tile1,
        //     debugName = "TestZone",
        // };
        // Test2Zone.Spawn();

        // Test3Zone = new Zone(world, SpecialType.Debug, LandLayer, WaterLayer, ObstacleLayer)
        // {
        //     spawnLocation = testing3SpawnLocation,
        //     PrefabLand = Test3Prefab,
        //     PrefabWater = null,
        //     PrefabObstacles = null,
        //     tile1 = tile1,
        //     debugName = "TestZone",
        // };
        // Test3Zone.Spawn();



        // // Generate the path between the starting area and the fortress. You gotta be able to get to one another.
        // PathFromZoneToZone(StartingZone, FortressZone, tile2);
    }

    void PathFromZoneToZone(Zone startZone, Zone endZone, TileBase groundTile)
    {
        System.Random randy = new System.Random();

        // Determine direction from start to end, which also determines which edge/corner to use.
        // Consequencely, this determines the endzone edge/corner to use.
        HexDirection pathDirection = Direction.GetDirection(startZone.spawnLocation, endZone.spawnLocation);
        
        // Get all the cells along the edge closest to our target.
        List<Vector2Int> startEdge = SelectDirectionalEdge(startZone, pathDirection);
        List<Vector2Int> endEdge = SelectDirectionalEdge(endZone, Direction.Opposite(pathDirection));

        // Choose a random cell along the appropriate edge, these will be the end points of the path.
        Vector2Int startCoordinates = startEdge[randy.Next(startEdge.Count)];
        Vector2Int endCoordinates = endEdge[randy.Next(endEdge.Count)];

        world.GetCell(startCoordinates).special = SpecialType.Path;
        world.GetCell(endCoordinates).special = SpecialType.Path;

        HexDirection nextDirection = pathDirection;
        WorldCell currentCell = world.GetCell(startCoordinates).GetNeighbor(pathDirection);
        currentCell.special = SpecialType.Path;

        // cycle through the cells, starting from startCell, and heading towards pathDirection.
        bool NeighborIsEndCoordinates = false;
        while (!NeighborIsEndCoordinates)
        {
            foreach(WorldCell neighbor in currentCell.GetListOfNeighbors())
            {
                if(endCoordinates == neighbor.Coordinates)
                {
                    NeighborIsEndCoordinates = true;
                }
            }

            if (!NeighborIsEndCoordinates)
            {
                // TODO handle out of bounds properly.
                // TODO don't select a cell we've already done in this loop.
                bool validNextCell = false;
                WorldCell nextCell = null;
                while (!validNextCell)
                {
                    // TODO randomly determine which direction we will be going, offset from the nextDirection (or exactly equal to nextDirection)
                    nextDirection = GetNextDirection(Direction.GetDirection(currentCell.Coordinates, endCoordinates));
                    nextCell = currentCell.GetNeighbor(nextDirection);
                    validNextCell = true;
                }
                currentCell = nextCell;

                // Build the path with what we found.
                currentCell.special = SpecialType.Path;
                #region Debug
                if (DebugSettings.DebugPath)
                {
                    tilemap.SetTile(new Vector3Int(currentCell.Coordinates.x, currentCell.Coordinates.y, 0), DebugSettings.DebugPathTile);
                    // tm.SetTile(new Vector3Int(coords.x, coords.y, 0), DebugSettings.DebugPathTile);
                }
                #endregion
                // todo chance for "extra" things to appear on the side.
            }

        }
    }

    private HexDirection GetNextDirection(HexDirection direction)
    {
        System.Random randy = new System.Random();
        HexDirection retVal = direction;
        int decision = randy.Next(100);

        // Chances of each occurance (in %).
        int slightlyBackwardsPercentChance = 5;
        int slightlyTurnedPercentChance = 35;

        if (decision < slightlyBackwardsPercentChance)
        {
            if(randy.Next(1) == 0)
            {
                retVal = Direction.Clockwise(direction, 2);
            }
            else
            {
                retVal = Direction.CounterClockwise(direction, 2);
            }
        }
        else if (decision < (slightlyTurnedPercentChance + slightlyBackwardsPercentChance))
        {
            if(randy.Next(1) == 0)
            {
                retVal = Direction.Clockwise(direction);
            }
            else
            {
                retVal = Direction.CounterClockwise(direction);
            }
        }
        // Otherwise we head straight ahead.
        return retVal;
    }

    public List<Vector2Int> SelectDirectionalEdge(Zone zone, HexDirection direction)
    {
        Vector2Int zoneCenter = zone.GetCenter();
        List<Vector2Int> boundryCells = zone.GetSurroundingCells();
        List<Vector2Int> returnCells = new List<Vector2Int>();
        
        foreach(Vector2Int cell in boundryCells)
        {
            bool NorthOrSouthValid = false;
            bool EastOrWestValid = false;
            if ((direction == HexDirection.N || direction == HexDirection.NW || direction == HexDirection.NE)
                && cell.y > zoneCenter.y)
            {
                NorthOrSouthValid = true;
            } else if ((direction == HexDirection.S || direction == HexDirection.SW || direction == HexDirection.SE)
                        && cell.y < zoneCenter.y)
            {
                NorthOrSouthValid = true;
            }

            if ((direction == HexDirection.NW || direction == HexDirection.SW) && cell.x > zoneCenter.x)
            {
                EastOrWestValid = true;
            }
            else if ((direction == HexDirection.NE || direction == HexDirection.SE) && cell.x < zoneCenter.x)
            {
                EastOrWestValid = true;
            }
            // The east/west valid check is always true if the direction is cardinal north or cardinal south.
            else if ((direction == HexDirection.N || direction == HexDirection.S))
            {
                EastOrWestValid = true;
            }
            

            if (NorthOrSouthValid && EastOrWestValid)
                returnCells.Add(cell);
        }

        return returnCells;
    }

    

    // Update is called once per frame
    void Update()
    {
        
    }
}
