using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    enum Directions
    {
        Left,
        Right,
        None
    }

    public float fixedTravelTime;
    private Vector3 targetPosition, velocity;
    private Directions direction;
    private bool isMoving, hasQueuedMove, isFalling, isRight;
    List<GameObject> activeCollisions = new List<GameObject>();
    private Animator animator;

    void Start()
    {
        direction = Directions.None;
        isMoving = hasQueuedMove = false;
        isFalling = isRight = true;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) && !isFalling)
        {
            MoveHorizontally(Directions.Left);

            if (isRight)
            {
                isRight = false;
                gameObject.GetComponent<SpriteRenderer>().flipX = true;
            }
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && !isFalling)
        {
            MoveHorizontally(Directions.Right);
            if (!isRight)
            {
                isRight = true;
                gameObject.GetComponent<SpriteRenderer>().flipX = false;
            }
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            animator.SetBool("isDigging", true);
            foreach (GameObject collisionObj in activeCollisions)
            {
                if (collisionObj.transform.position.x == Mathf.Round(transform.position.x) &&
                    collisionObj.transform.position.y == Mathf.Round(transform.position.y) - 1)
                {
                    Destroy(collisionObj);
                    break;
                }
            }
        }
        else if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            animator.SetBool("isDigging", false);
        }

        transform.position += velocity * Time.deltaTime * Time.timeScale;
        if (isMoving && 
            (direction == Directions.Left && targetPosition.x >= transform.position.x) ||
            (direction == Directions.Right && targetPosition.x <= transform.position.x))
        {
            StopMoving();
        }
        if (!isFalling && isMoving && transform.position.y < MathF.Round(transform.position.y))
        {
            transform.position = new Vector3(transform.position.x, MathF.Round(transform.position.y), transform.position.z);
        }
    }

    void MoveHorizontally(Directions newDirection)
    {
        if (hasQueuedMove)
        {
            return;
        }

        float xVelocity = 0;
        switch (newDirection)
        {
            case Directions.Left:
                xVelocity = -1;
                break;
            case Directions.Right:
                xVelocity = 1;
                break;
            case Directions.None:
            default:
                return;
        }
        direction = newDirection;

        Vector3 currentPosition;
        if (targetPosition == Vector3.zero)
        {
            currentPosition = transform.position;
        }
        else
        {
            currentPosition = targetPosition;
            hasQueuedMove = true;
        }
        targetPosition = currentPosition;
        targetPosition.x = Mathf.Round(targetPosition.x);
        targetPosition.x += xVelocity;
        velocity = new Vector3(xVelocity, 0, 0);
        velocity /= fixedTravelTime;
        isMoving = true;
        animator.SetBool("isMoving", isMoving);
    }

    void StopMoving()
    {
        velocity = targetPosition = Vector3.zero;
        isMoving = hasQueuedMove = hasQueuedMove = false;
        animator.SetBool("isMoving", isMoving);
        direction = Directions.None;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Prize"))
        {
            return;
        }
        else if (collision.gameObject.CompareTag("Trap"))
        {
            //game over
            //open menu
        }

        activeCollisions.Add(collision.gameObject);
        if (isFalling)
        {
            isFalling = false;
        }

        if (collision.gameObject.CompareTag("Wall") && isMoving)
        {
            StopMoving();
        }
        else if ((collision.gameObject.CompareTag("Block") || collision.gameObject.CompareTag("GravelBlock"))
            && Mathf.Round(collision.gameObject.transform.position.y) == Mathf.Round(targetPosition.y)
            && isMoving)
        {
            Destroy(collision.gameObject);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        activeCollisions.Remove(collision.gameObject);

        if (collision.gameObject.CompareTag("GravelBlock") && activeCollisions.Count == 0)
        {
            isFalling = true;
        }
    }
}
