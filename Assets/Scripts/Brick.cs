using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour {
    public static int breakableCount = 0;

    public AudioClip crack;
    public Sprite[] hitSprites;
    public GameObject smoke;

    private int timesHit;
    private int maxHits;
    private bool isBreakable;
    private LevelManager levelManager;

    void Start () {
        isBreakable = this.tag == "Breakable";
        if (isBreakable)
        {
            breakableCount++;
        }
        timesHit = 0;
        maxHits = hitSprites.Length + 1;
        levelManager = GameObject.FindObjectOfType<LevelManager>();
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isBreakable)
        {
            HandleHits();
        }
    }

    private void HandleHits ()
    {
        timesHit++;
        if (timesHit >= maxHits)
        {
            GameObject smokePuff = Instantiate(smoke, gameObject.transform.position, Quaternion.identity);

            AudioSource.PlayClipAtPoint(crack, transform.position);
            breakableCount--;
            levelManager.BrickDestroyed();
            Destroy(gameObject);
            smokePuff.GetComponent<ParticleSystem>().startColor = this.GetComponent<SpriteRenderer>().color;
        }
        else
        {
            LoadSprites();
        }
    }

    private void LoadSprites ()
    {
        int spriteIndex = timesHit - 1;
        Sprite newSprite = hitSprites[spriteIndex];

        if (newSprite)
        {
            this.GetComponent<SpriteRenderer>().sprite = newSprite;
        }
    }
}
