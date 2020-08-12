using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WorldCell
{
    public Vector2Int Coordinates = new Vector2Int();
    
    public WorldCell NorthernNeighbor;
    public WorldCell NorthWesternNeighbor;
    public WorldCell NorthEasternNeighbor;
    public WorldCell SouthernNeighbor;
    public WorldCell SouthWesternNeighbor;
    public WorldCell SouthEasternNeighbor;


    public BiomeType biome;
    public TerrainType terrain;
    public SpecialType special;

    public WorldCell GetNeighbor(HexDirection direction)
    {
        switch (direction)
        {
            case HexDirection.N:
                return NorthernNeighbor;
            case HexDirection.NW:
                return NorthWesternNeighbor;
            case HexDirection.NE:
                return NorthEasternNeighbor;
            case HexDirection.S:
                return SouthernNeighbor;
            case HexDirection.SW:
                return SouthWesternNeighbor;
            case HexDirection.SE:
                return SouthEasternNeighbor;
            default:
                return null;
        }
    }

    public List<WorldCell> GetListOfNeighbors()
    {
        List<WorldCell> neighborList = new List<WorldCell>
        {
            NorthernNeighbor,
            NorthWesternNeighbor,
            NorthEasternNeighbor,
            SouthernNeighbor,
            SouthWesternNeighbor,
            SouthEasternNeighbor,
        };

        return neighborList;
    }

    // TODO handle the various layers. Per cell or maybe separate world grids per?
    // Water
    // Land
    // Decoration
    // Obstacles
    // Caves
    // Test
}
