using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    private int depth, prizes;
    public PrizesBar prizesBar;

    void Start()
    {
        depth = 0;
        prizes = 0;
        prizesBar.SetCollectedPrizes(prizes);
    }

    void Update()
    {
        depth = (int)Mathf.Round(transform.position.y) * -1;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Prize"))
        {
            CollectPrize();
            Destroy(collision.gameObject);
        }
    }

    void CollectPrize() // so I can add different amount of prizes at once later
    {
        prizes++;
        prizesBar.SetCollectedPrizes(prizes);
    }
}
