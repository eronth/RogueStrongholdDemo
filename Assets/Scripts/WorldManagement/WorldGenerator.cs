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
    private Tilemap theStartingZone;
    private Vector3Int startingZoneSpawnLocation;

    // Fortress Related Objects
    public Tilemap FortressPrefab;
    private Tilemap theFortress;
    private Vector3Int fortressSpawnLocation;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        // Initialize some locations.
        startingZoneSpawnLocation = new Vector3Int(0, 0, 0);
        fortressSpawnLocation = new Vector3Int(0, world.Height/3, 0);

        // Render the location visuals.
        // -- Prefab Zones -- //
        theStartingZone = InitializePrefabLocation(StartingZonePrefab, startingZoneSpawnLocation, LandLayer, SpecialType.Starter);
        theFortress = InitializePrefabLocation(FortressPrefab, fortressSpawnLocation, LandLayer, SpecialType.HomeFortress);


        
    }

    Tilemap InitializePrefabLocation(Tilemap prefab, Vector3Int spawnLocation, Transform gridLayer, SpecialType st)
    {
        Tilemap tm = Instantiate(prefab, spawnLocation, Quaternion.identity, gridLayer);

        // Get an enumerator for all the cells within this area.
        BoundsInt.PositionEnumerator pe = theStartingZone.cellBounds.allPositionsWithin;
        do
        {
            if (theStartingZone.GetSprite(pe.Current) != null)
            {
                world.GetCell(pe.Current).special = st;
                tilemap.SetTile(pe.Current, tile2);
            }   
        } while (pe.MoveNext());

        return tm;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
