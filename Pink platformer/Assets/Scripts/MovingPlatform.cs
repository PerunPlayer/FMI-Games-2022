using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public int speed;
    public float rightPos, leftPos;
    bool moveRight = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < leftPos)
        {
            moveRight = true;
        }
        if (transform.position.x > rightPos)
        {
            moveRight = false;
        }

        if (moveRight)
        {
            transform.position = new Vector2(transform.position.x + (speed * Time.deltaTime), transform.position.y);
        }
        else
        {
            transform.position = new Vector2(transform.position.x - (speed * Time.deltaTime), transform.position.y);
        }
    }
}
