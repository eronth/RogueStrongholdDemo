using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Click : MonoBehaviour
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
    }

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
                    var debugMessage = $"Moving {selectedCharacter.name} to {Input.mousePosition.x}, {Input.mousePosition.y}";
                    Debug.Log(debugMessage);
                    DebuggerOnScreen.Extra = debugMessage;

                    Vector3 mousePositionToWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    DebuggerOnScreen.Mouse = $"Mouse clicking at {mousePositionToWorldPosition.x}, {mousePositionToWorldPosition.y}";
                    mousePositionToWorldPosition.z = 0;
                    selectedCharacter.GetComponent<PlayerMovement>().MoveToGridLocation(
                        mousePositionToWorldPosition.x,
                        mousePositionToWorldPosition.y
                    );
                }
                else
                {
                    DebuggerOnScreen.Mouse 
                        = $"Clicked at {Input.mousePosition.x}, {Input.mousePosition.y}";

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
            }
        }

        return didClick;
    }

    private void OnDisable()
    {
        // GameManager.instance.playerFoodPoints = food;
    }
}
