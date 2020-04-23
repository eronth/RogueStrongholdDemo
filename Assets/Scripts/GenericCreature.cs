using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GenericCreature : MonoBehaviour
{
    public float moveTime = 0.1f; // Todo, swap this out for a property that gets/sets inverseMoveTime
    private float inverseMoveTime;
    public LayerMask blockingLayer;

    private BoxCollider2D boxCollider;
    private Rigidbody2D rb2D;

    // // Start is called before the first frame update
    protected virtual void Start()
    {
    //     // Get references to objects components.
        boxCollider = GetComponent<BoxCollider2D>();
        rb2D = GetComponent<Rigidbody2D>();

    //     // By storing the reciprocal of the move time we can use it by multiplying instead of dividing, this is more efficient.
        inverseMoveTime = 1f / moveTime;
    }

    protected virtual void AttemptMove<T>(int xDir, int yDir)
        where T : Component
    {
        RaycastHit2D hit;
        bool canMove = Move(xDir, yDir, out hit);

    //     if (hit.transform == null)
    //     {
    //         return;
    //     }

    //     T hitComponent = hit.transform.GetComponent<T>();

    //     if (!canMove && hitComponent != null)
    //     {
    //         OnCantMove(hitComponent);
    //     }
    }

    protected bool Move (int xDir, int yDir, out RaycastHit2D hit)
    {
    // //     Vector3 end = new Vector3(-3, 3, 0);
    // //     rb2D.MovePosition(end);
        Vector2 start = transform.position;
        Vector2 end = start + new Vector2(xDir, yDir);
        boxCollider.enabled = false;
        hit = Physics2D.Linecast(start, end, blockingLayer);
        boxCollider.enabled = true;

        if (hit.transform == null)
        {
            StartCoroutine(SmoothMovement(end));
            return true; // Did move.
        }
        else
        {
            return false; // Did not move.
        } 
    }

    protected IEnumerator SmoothMovement (Vector3 end)
    {
    //     end = new Vector3(-3, 3, 0);
        float sqrRemainingDistance = (transform.position - end).sqrMagnitude;

        while (sqrRemainingDistance > float.Epsilon)
        {
            Vector3 newPosition = Vector3.MoveTowards(rb2D.position, end, inverseMoveTime * Time.deltaTime);
            rb2D.MovePosition(newPosition);//newPosition);
            sqrRemainingDistance = (transform.position - end).sqrMagnitude;

            yield return null;
        }
    }


    protected abstract void OnCantMove<T>(T component)
        where T : Component;
}
