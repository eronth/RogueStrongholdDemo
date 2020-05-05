using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : GenericCreature
{
    private Animator animator;
    private SpriteRenderer spriteRend;
    Vector2 movement;
    private bool isSelected = false;

    protected override void Start()
    {
        animator = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();

        // food = GameManager.instance.playerFoodPoints;
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void CheckIfGameOver()
    {
        // if (food <= 0)
        // {
        //     GameManager.instance.GameOver();
        //  }
    }

    public void MoveToCellCoordinates(Vector3Int gridCell)
    {
        AttemptMoveToCellCoords(gridCell);
    }
    
    // public void MoveToGridLocation(float xPos, float yPos)
    // {
    //     xPos = (float)Math.Round(xPos);
    //     yPos = (float)Math.Round(yPos);
    //     AttemptMove(xPos, yPos);
    // }

    // public void MoveToLocation(float xPos, float yPos)
    // {
    //     AttemptMove(xPos, yPos);
    // }

    // protected override void AttemptMove(float xPos, float yPos)
    // {
    //     base.AttemptMove(xPos, yPos);
    // }

    protected override void AttemptMoveToCellCoords(Vector3Int cellCoordinates)
    {
        base.AttemptMoveToCellCoords(cellCoordinates);
    }

    protected override void OnCantMove<T>(T component)
    {
        throw new NotImplementedException();
    }

    public void Select()
    {
        isSelected = true;
        spriteRend.color = ColorManager.PlayerCharacterSelected;
    }

    public void Unselect()
    {
        isSelected = false;
        spriteRend.color = ColorManager.Unselect;
    }
}
