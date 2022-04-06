using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Move : MonoBehaviour
{
    public int speed;
    private bool isLeft = false;
    private bool isRight;
    public Animator animator;

    void Start()
    {
        isRight = true;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            animator.SetFloat("Speed", speed);
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
            animator.SetFloat("Speed", speed);
            transform.Translate(speed * Time.deltaTime, 0, 0);
            if (isLeft)
            {
                isRight = true;
                isLeft = false;
                gameObject.GetComponent<SpriteRenderer>().flipX = false;
            }
        }

        if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            animator.SetFloat("Speed", 0);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Equals("MovingPlatform"))
        {
            this.transform.parent = collision.transform;
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
