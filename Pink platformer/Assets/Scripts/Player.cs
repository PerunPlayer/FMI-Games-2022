using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int speed;
    public float jumpVelocity;
    private Rigidbody2D rigidbody2D;
    private BoxCollider2D boxCollider2D;
    [SerializeField] private LayerMask platformsLayerMask;
    private bool isLeft = false;
    private bool isRight;
    bool canDoubleJump = false;
    int collectedKeys = 0;
    public int lifes;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = transform.GetComponent<Rigidbody2D>();
        boxCollider2D = transform.GetComponent<BoxCollider2D>();
        isRight = true;
        lifes = 3;
    }

    private bool IsGrounded()
    {
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0f, Vector2.down, 0.1f, platformsLayerMask);
        return (raycastHit2D.collider != null);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(-speed * Time.deltaTime, 0, 0);
            if (isRight)
            {
                isRight = false;
                isLeft = true;
                gameObject.GetComponent<SpriteRenderer>().flipX = true;
            }
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(speed * Time.deltaTime, 0, 0);
            if (isLeft)
            {
                isRight = true;
                isLeft = false;
                gameObject.GetComponent<SpriteRenderer>().flipX = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (IsGrounded())
            {
                rigidbody2D.velocity = Vector2.up * jumpVelocity;
                canDoubleJump = true;
            }
            else if (canDoubleJump)
            {
                rigidbody2D.velocity = Vector2.up * jumpVelocity;
                canDoubleJump = false;
            }
            
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Equals("MovingPlatform"))
        {
            this.transform.parent = collision.transform;
        }
        if (collision.gameObject.name.Equals("Spikes"))
        {
            lifes--;
            Debug.Log("Lifes: " + lifes);
            if (lifes > 0)
            {
                transform.position = new Vector2(0, -1.5f); //respawn
            }
        }
        if (collision.gameObject.tag.Equals("Key"))
        {
            Destroy(collision.gameObject);
            collectedKeys += 1;
            Debug.Log("Collected keys: " + collectedKeys);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            this.transform.parent = null;
        }
    }
}
