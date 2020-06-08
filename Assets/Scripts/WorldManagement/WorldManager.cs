using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BoardManager : MonoBehaviour
{
    [Serializable]
    public class Count
    {
        public int minimum;
        public int maximum;
        public Count(int min, int max)
        {
            minimum = min;
            maximum = max;
        }
    }
    public int columns = 8;
    public int rows = 8;
    public Count wallCount = new Count(5, 9);
    public Count foodCount = new Count(1, 5);
    public GameObject exit;
    public GameObject[] floorTiles;
    public GameObject[] wallTiles;
    public GameObject[] enemyTiles;
    public GameObject[] outerWallTiles;
    private System.Random randy = new System.Random();

    private Transform boardHolder;
    private List<Vector3> gridPositions = new List<Vector3>();
    void InitializeList()
    {
        gridPositions.Clear();

        for (int x = 1; x < columns - 1; x++)
        {
            for (int y = 1; y < columns - 1; y++)
            {
                gridPositions.Add(new Vector3(x, y, 0f));
            }
        }
        
    }

    public GameObject PlayerUnitPrefab;
    public GameObject[] Players;
    public Sprite[] PlayerSprites;
    public Vector3[] PlayerSpawnLocations;
        

    // Start is called before the first frame update
    void Start()
    {
        // Pull all hero sprites into the sprites object
        LoadPlayerCharacterSprites();

        // Initialize player spawn locations.
        PlayerSpawnLocations = new Vector3[]
            {
                World.Grid.CellToLocal(new Vector3Int(2,0,0)),
                World.Grid.CellToWorld(new Vector3Int(0,1,0)),
                World.Grid.CellToWorld(new Vector3Int(0,-1,0)),
            };

        // Generate the player characters, randomly assing values.
        PreparePlayerCharacters();
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LoadPlayerCharacterSprites()
    {
        // Todo turn sprite location into a resource item instead of hard string.
        object[] loadedIcons = Resources.LoadAll ("Sprites/xFE PlayerSprites", typeof(Sprite));
        PlayerSprites = new Sprite[loadedIcons.Length];
        
        for(int x = 0; x< loadedIcons.Length;x++){
            PlayerSprites[x] = (Sprite)loadedIcons [x];
        }

        loadedIcons.CopyTo(PlayerSprites, 0);
        // PlayerSprites = Resources.LoadAll<Sprite>("Sprites/xFE PlayerSprites", typeof(Sprite));
    }

    private void PreparePlayerCharacters()
    {
        for(int i = 0; i < Players.Length; i++)
        {
            PlayerUnitPrefab.GetComponent<SpriteRenderer>().sprite = PlayerSprites[randy.Next(PlayerSprites.Length)];
            Players[i] = Instantiate(PlayerUnitPrefab, PlayerSpawnLocations[i], Quaternion.identity);
            // TODO make this variation work.
            // Players[2].GetComponent<SpriteRenderer>().sprite = Hero3;
        }
    }

}