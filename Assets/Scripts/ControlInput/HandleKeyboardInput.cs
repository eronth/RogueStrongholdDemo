﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleKeyboardInput : MonoBehaviour
{
    [SerializeField]
    private LayerMask clickablesLayer;
    public GameObject selectedCharacter = null;
    public GameObject[] players;

    // Start is called before the first frame update
    void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        HandleMouseClicks();
        HandleKeyInputs();
    }

    public Sprite spriteHolder;
    private bool HandleMouseClicks()
    {
        bool didClick = false;
        // On M1
        if(Input.GetMouseButtonDown(0))
        {
            didClick = true;
            RaycastHit2D hit = Physics2D.Raycast(
                Camera.main.ScreenToWorldPoint(Input.mousePosition),
                Vector2.zero
            );

            // Fires if you actually clicked on something.
            if(hit)
            {
                var rayName = hit.transform.name;
                Debug.Log(rayName);
                
                DebuggerOnScreen.Mouse = $"Clicked at {Input.mousePosition.x},{Input.mousePosition.y}"
                    + $" Clicked on {rayName}, Object Type: {hit.collider.gameObject}";
                
                // Action to take if you've clicked on a player's character.
                if (hit.collider.gameObject.CompareTag("Player"))
                {
                    SpriteRenderer sr = hit.collider.GetComponent<SpriteRenderer>();
                    sr.sprite = spriteHolder;
                    // TODO Fire off special function that will set character to selected AND color them.
                    if (selectedCharacter != hit.collider)
                    {
                        if (selectedCharacter != null) 
                        { selectedCharacter.GetComponent<PlayerMovement>().Unselect(); }
                        selectedCharacter = hit.collider.gameObject;
                        hit.collider.GetComponent<PlayerMovement>().Select();
                    }
                }
            }
            // Action to take if you did not click on something (e.g. You clicked on the ground.)
            else
            {

                if (selectedCharacter != null)
                {
                    // Collect the coordinates and convert to grid coordinates.
                    Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    Vector3Int clickedGridCell = World.Grid.WorldToCell(mouseWorldPos);

                    var debugMessage = $"Mouse click at {mouseWorldPos} mapping to {clickedGridCell}. Moving {selectedCharacter.name}.";
                    Debug.Log(debugMessage);
                    DebuggerOnScreen.Mouse = debugMessage;

                    selectedCharacter.GetComponent<PlayerMovement>()
                        .MoveToCellCoordinates(clickedGridCell);
                }
                else
                {
                    Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    Vector3Int clickedGridCell = World.Grid.WorldToCell(mouseWorldPos);
                    DebuggerOnScreen.Mouse 
                        = $"Clicked at Input: {Input.mousePosition}; ScreentToWorldPoint: {mouseWorldPos}; WorldToCell: {clickedGridCell};";
                }
            }
        }
        // On M2
        else if (Input.GetMouseButtonDown(1))
        {
            didClick = true;
            if (selectedCharacter != null)
            {
                selectedCharacter.GetComponent<PlayerMovement>().Unselect();
                selectedCharacter = null;
            }
        }

        return didClick;
    }
    private bool HandleKeyInputs()
    {
        bool didPress = false;

        return didPress;
    }
    private void OnDisable()
    {
    }
}