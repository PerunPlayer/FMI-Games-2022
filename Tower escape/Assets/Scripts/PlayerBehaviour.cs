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
    private bool isMoving, hasQueuedMove;

    void Start()
    {
        direction = Directions.None;
        isMoving = false;
        hasQueuedMove = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MoveHorizontally(Directions.Left);
            
            //if (isRight)
            //{
            //    isRight = false;
            //    isLeft = true;
            //    gameObject.GetComponent<SpriteRenderer>().flipX = true;
            //}
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveHorizontally(Directions.Right);
            //if (isLeft)
            //{
            //    isRight = true;
            //    isLeft = false;
            //    gameObject.GetComponent<SpriteRenderer>().flipX = false;
            //}
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {

        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {

        }

        transform.position += velocity * Time.deltaTime * Time.timeScale;
        if (isMoving && 
            (direction == Directions.Left && targetPosition.x >= transform.position.x) ||
            (direction == Directions.Right && targetPosition.x <= transform.position.x))
        {
            StopMoving();
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
        velocity = new Vector3(xVelocity, currentPosition.y, currentPosition.z);
        velocity /= fixedTravelTime;
        isMoving = true;
    }

    void MoveVertically(float value)
    {

    }

    void StopMoving()
    {
        velocity = Vector3.zero;
        targetPosition = Vector3.zero;
        isMoving = false;
        hasQueuedMove = false;
        direction = Directions.None;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isMoving)
        {
            float deltaY = Mathf.Round(transform.position.y) - transform.position.y;
            MoveVertically(deltaY);
        }

        if (collision.gameObject.CompareTag("Wall") && isMoving)
        {
            StopMoving();
        }
        else if (collision.gameObject.CompareTag("Block")
            && Mathf.Round(collision.gameObject.transform.position.y) == Mathf.Round(targetPosition.y)
            && isMoving)
        {
            Destroy(collision.gameObject);
        }
    }
}
