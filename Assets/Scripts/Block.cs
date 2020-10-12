using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    // Configuration Parameters
    [SerializeField] Sprite[] hitSprites = null;
    [SerializeField] AudioClip destroyBlockSound = null;
    [SerializeField] int blockDamageDefaultPoints = 10;
    [SerializeField] int blockDamagePointAdderPerHit = 20;
    [SerializeField] int blockHealth = 2;
    [SerializeField] GameObject blockSparklesVFX = null;
    [SerializeField] float sparklesDuration = 2.0f;
    [SerializeField] bool isRotating = false;
    [SerializeField] float rotatingSpeed = 10.0f;

    // Cached Reference
    GameStatus gameStatus;
    Level level;

    // State
    [SerializeField] int timesHit = 0; //Serialized for debug

    // Start is called before the first frame update
    void Start()
    {
        level = FindObjectOfType<Level>();
        if (CompareTag("Breakable"))
        {
            level.IncrementBreakableBlocks();
            UpdateBlockSprite();
        }
        gameStatus = FindObjectOfType<GameStatus>();
    }

    private void FixedUpdate()
    {
        if (isRotating)
        {
            transform.Rotate(0.0f, 0.0f, rotatingSpeed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (CompareTag("Breakable"))
        {
            timesHit++;
            UpdateBlockSprite();
            HandleHit();
        }
    }

    private void UpdateBlockSprite()
    {
        int blockStatus = (int)Mathf.Clamp(blockHealth - timesHit, 0, 2);
        if (hitSprites[blockStatus] != null)
        {
            GetComponent<SpriteRenderer>().sprite = hitSprites[blockStatus];
        }
    }

    private void HandleHit()
    {
        gameStatus.IncrementScore(blockDamageDefaultPoints + (timesHit - 1) * blockDamagePointAdderPerHit);
        if (timesHit > blockHealth)
        {
            DestroyBlock();
        }
    }

    private void DestroyBlock()
    {
        AudioSource.PlayClipAtPoint(destroyBlockSound, Camera.main.transform.position);
        level.DecrementBreakableBlocks();
        Destroy(gameObject);
        TriggerSparklesVFX();
    }

    private void TriggerSparklesVFX()
    {
        GameObject sparkles = Instantiate(blockSparklesVFX, transform.position, transform.rotation);
        Destroy(sparkles, sparklesDuration);
    }
}
