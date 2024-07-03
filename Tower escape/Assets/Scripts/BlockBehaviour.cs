using System.Collections;
using UnityEngine;

public class BlockBehaviour : MonoBehaviour
{
    public enum BlockType
    {
        Stone,
        Dirt,
        Gravel
    }

    public BlockType blockType;
    public float destroyingTimer;
    private bool startedTimer;

    void Start()
    {
        destroyingTimer = 2;
        startedTimer = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && blockType == BlockType.Gravel && !startedTimer)
        {
            startedTimer = true;
            StartCoroutine(BreakBlockDelayed(destroyingTimer));
        }
    }

    IEnumerator BreakBlockDelayed(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
