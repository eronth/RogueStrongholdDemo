using UnityEngine;

public class WorldCell
{
    public Vector2Int Coordinates;
    
    public WorldCell[] Neighbor = new WorldCell[6];

    // TODO handle the various layers. Per cell or maybe separate world grids per?
    // Water
    // Land
    // Decoration
    // Obstacles
    // Caves
    // Test
}
