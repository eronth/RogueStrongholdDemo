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

    public WorldGrid world = new WorldGrid();
    public Transform WorldGriddd;

    public GameObject tgottm;
    public Tilemap ttm;
    
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
        // -- Starting Zone -- //
        theStartingZone = Instantiate(StartingZonePrefab, startingZoneSpawnLocation, Quaternion.identity, WorldGriddd);
        // Get an enumerator for all the cells within this area.
        BoundsInt.PositionEnumerator a = theStartingZone.cellBounds.allPositionsWithin;
        do
        {
            tilemap.SetTile(a.Current, tile1);
            if (theStartingZone.GetSprite(a.Current) == null)
                tilemap.SetTile(a.Current, tile2);
        } while (a.MoveNext());
        
        theFortress = Instantiate(FortressPrefab, fortressSpawnLocation, Quaternion.identity, WorldGriddd);
        BoundsInt i = ttm.cellBounds;

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
