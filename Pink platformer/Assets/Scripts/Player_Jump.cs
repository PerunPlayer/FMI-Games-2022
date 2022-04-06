using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Jump : MonoBehaviour
{
    bool canDoubleJump = false;
    public float jumpVelocity;
    private Rigidbody2D rigidbody2D;
    private BoxCollider2D boxCollider2D;
    [SerializeField] private LayerMask platformsLayerMask;
    public Animator animator;

    void Start()
    {
        rigidbody2D = transform.GetComponent<Rigidbody2D>();
        boxCollider2D = transform.GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
    }

    private bool IsGrounded()
    {
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0f, Vector2.down, 0.1f, platformsLayerMask);
        return (raycastHit2D.collider != null);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (IsGrounded())
            {
                animator.SetFloat("Speed", 0);
                animator.SetBool("canJump", false);
                rigidbody2D.velocity = Vector2.up * jumpVelocity;
                canDoubleJump = true;
            }
            else if (canDoubleJump)
            {
                animator.SetFloat("Speed", 0);
                animator.SetBool("canJump", false);
                rigidbody2D.velocity = Vector2.up * jumpVelocity;
                canDoubleJump = false;
            }

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            animator.SetBool("canJump", true);
        }
        if (collision.gameObject.name.Equals("Spikes"))
        {
            animator.SetBool("canJump", false);
        }
    }
}
