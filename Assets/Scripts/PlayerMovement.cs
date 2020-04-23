using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : GenericCreature
{
    // // Tutorial shiz
    // public int wallDamage = 1;
    // public int pointsPerFood = 10;
    // public int pointsPerSoda = 20;
    // public float restartLevelDelay = 1f;

    // public float moveSpeed = 5f;
    // public Rigidbody2D rb;
    private Vector2 previousInput;
    private Animator animator;
    private SpriteRenderer spriteRend;
    Vector2 movement;
    // private int food = 50;
    private bool isSelected = false;

    protected override void Start()
    {
        animator = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();

        // food = GameManager.instance.playerFoodPoints;
        base.Start();
    }

    // private void CheckIfGameOver()
    // {
    //     if (food <= 0)
    //     {
    //         GameManager.instance.GameOver();
    //     }
    // }

    // // Update is called once per frame
    void Update()
    {
        //HandleMouseClicks();
        // Handle Input
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // Only move in one direction at a time.
        if (movement.x != 0)
        { movement.y = 0; }

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        
        //animator.SetFloat("Speed", movement.sqrMagnitude);

        if (movement.x != 0 || movement.y != 0)
        {
            if (previousInput.x != movement.x || previousInput.y != movement.y)
            {
                DebuggerOnScreen.Position = $"Position {movement.x},{movement.y}.";
                AttemptMove<Wall>((int)movement.x, (int)movement.y);
            }
        }
        
        previousInput.x = movement.x;
        previousInput.y = movement.y;
    }

    public void MoveToLocation(int xPos, int yPos)
    {
        AttemptMove<Wall>(xPos, yPos);
    }

    protected override void AttemptMove<T>(int xDir, int yDir)
    {
        base.AttemptMove<T>(xDir, yDir);
        RaycastHit2D hit;
    //     CheckIfGameOver();
    //     //todo GameManager.instance.playersTurn = false;
    }

    protected override void OnCantMove<T>(T component)
    {
        //throw new NotImplementedException();
    }

    public void Select()
    {
        isSelected = true;
        spriteRend.color = new Color(255, 230, 0, 255);
    }

    public void Unselect()
    {
        isSelected = false;
        spriteRend.color = new Color(255, 255, 255, 255);
    }

    // void FixedUpdate()
    // {
    //     // Movement
    //     rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    // }

}
