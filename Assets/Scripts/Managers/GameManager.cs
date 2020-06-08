using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private bool doingSetup;
    public static GameManager instance = null;
    private WorldManager boardScript;
    [HideInInspector] public bool playersTurn = true;

    //Awake is always called before any Start functions
    void Awake()
    {
        // Check if instance already exists. if not, set instance to 'this'
        // else, if instance already exists and it's not this, destroy it.
        if (instance == null)
        { instance = this; }
        else if (instance != this)
        { Destroy(gameObject); }

        // Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);

        // Get a component reference to the attached BoardManager script
        boardScript = GetComponent<WorldManager>();

        // Call the InitGame function to initialize the first level 
        InitGame();
    }

    private void OnLevelWasLoaded(int index)
    {
        InitGame();
    }

    // Initializes the game for each level.
    void InitGame()
    {
        doingSetup = true;
        // Call the SetupScene function of the BoardManager script, pass it current level number.
        // boardScript.SetupScene(level);
        doingSetup = false;
    }

    // Update is called every frame.
    void Update()
    {

    }

    public void GameOver()
    {
        enabled = false;
    }
}
