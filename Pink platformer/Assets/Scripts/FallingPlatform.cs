using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    int fallingSpeed = 2;
    bool isFalling = false;
    Vector2 pos;
    // Start is called before the first frame update
    void Start()
    {
        pos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (isFalling)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y - (fallingSpeed * Time.deltaTime));
        }
        if (!isFalling && !gameObject.GetComponent<Renderer>().enabled)
        {
            gameObject.GetComponent<Renderer>().enabled = true;
            transform.position = pos;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isFalling = true;
            Vector2 pos = transform.position;
            StartCoroutine(ResetPlatform(2f));
        }
    }

    IEnumerator ResetPlatform(float time)
    {
        yield return new WaitForSeconds(time);
        gameObject.GetComponent<Renderer>().enabled = false;
        yield return new WaitForSeconds(time);
        isFalling = false;
    }
}
