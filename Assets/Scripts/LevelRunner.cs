using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelRunner : MonoBehaviour
{
    private static int hits;
    private static int misses;

    public static TMP_Text hitLabel;
    public static TMP_Text missLabel;

    public TMP_Text hitLabelHolder;
    public TMP_Text missLabelHolder;

    public GameObject bulletPrefab;
    public GameObject enemyPrefab;

    public float bulletSpeed = 3f;

    public int maxSpawnCount = 100;
    private int spawnCount = 0;

    private float timeInterval = 0.5f;
    private float timer;

    // the distance between the edge of the square frame and the center
    // of the gameplay screen, in units
    private float edgeDistance;
    private Bounds killBounds;
    private Bounds centerBounds;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0f;

        edgeDistance = Camera.main.orthographicSize;
        killBounds = new Bounds(Vector3.zero, Vector3.one * (edgeDistance * 2f + 2f));
        centerBounds = new Bounds(Vector3.zero, Vector3.one);

        if (hitLabelHolder != null)
        {
            hitLabel = hitLabelHolder;
        }
        if (missLabelHolder != null)
        {
            missLabel = missLabelHolder;
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= timeInterval && spawnCount < maxSpawnCount)
        {
            timer = 0f;
            SpawnRandomBullet();
            spawnCount += 1;
        }
    }

    void SpawnRandomBullet()
    {
        int randDir = Random.Range(0, 4);

        Vector3 spawnPosition = Vector3.zero;
        Vector3 spawnDirection = Vector3.zero;

        if (randDir == 0)
        {
            spawnPosition = Vector3.up * (edgeDistance + 1f);
            spawnDirection = Vector3.down;
        }
        else if (randDir == 1)
        {
            spawnPosition = Vector3.down * (edgeDistance + 1f);
            spawnDirection = Vector3.up;
        }
        else if (randDir == 2)
        {
            spawnPosition = Vector3.left * (edgeDistance + 1f);
            spawnDirection = Vector3.right;
        }
        else
        {
            spawnPosition = Vector3.right * (edgeDistance + 1f);
            spawnDirection = Vector3.left;
        }

        int randPrefab = Random.Range(0, 1);

        if (randPrefab == 0)
        {
            GameObject nextBullet = Instantiate(bulletPrefab, spawnPosition, Quaternion.identity);
            nextBullet.GetComponent<BulletMover>().SetProperties(spawnDirection, bulletSpeed, killBounds);
        }
        else
        {
            GameObject nextEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
            nextEnemy.GetComponent<EnemyMover>().SetProperties(spawnDirection, bulletSpeed, centerBounds);
        }
    }

    void SpawnUpEnemy()
    {

    }

    void SpawnDownEnemy()
    {

    }

    void SpawnLeftEnemy()
    {

    }

    void SpawnRightEnemy()
    {

    }

    public static void AddHit(int points)
    {
        // TODO
        hits += 1;
        hitLabel.SetText("Hits: " + hits);
    }

    public static void AddHit()
    {
        AddHit(1);
    }

    public static void AddMiss()
    {
        misses += 1;
        missLabel.SetText("Miss: " + misses);
    }
}
