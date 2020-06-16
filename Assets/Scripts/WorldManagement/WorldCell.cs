using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WorldCell
{
    public Vector2Int Coordinates = new Vector2Int();
    
    public WorldCell[] Neighbor = new WorldCell[6];

    public BiomeType biome;
    public TerrainType terrain;
    public SpecialType special; 

    // TODO handle the various layers. Per cell or maybe separate world grids per?
    // Water
    // Land
    // Decoration
    // Obstacles
    // Caves
    // Test
}
