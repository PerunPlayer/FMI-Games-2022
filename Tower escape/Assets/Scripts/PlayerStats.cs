using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    private int depth, prizes;

    void Start()
    {
        depth = 0;
        prizes = 0;
    }

    void Update()
    {
        depth = (int)Mathf.Round(transform.position.y) * -1;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Prize"))
        {
            prizes++;
            Destroy(collision.gameObject);
        }
    }
}
