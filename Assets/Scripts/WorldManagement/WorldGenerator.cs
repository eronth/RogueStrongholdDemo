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
    public GameObject FortressPrefab;
    public Transform WorldGriddd;
    private GameObject theFortress;


    // Start is called before the first frame update
    void Start()
    {

        //tilemap.SetTile(new Vector3Int(0,0,0), tile1);
        //tilemap.SetTile(new Vector3Int(1,0,0), tile2);
        //tilemap.SetTile(new Vector3Int(0,1,0), tile3);
        
        
        theFortress = Instantiate(FortressPrefab, new Vector3Int(0,world.Height/3,0), Quaternion.identity);
        theFortress.transform.parent = WorldGriddd;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
