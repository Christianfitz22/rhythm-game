using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour, IMover
{
    private Vector3 direction;
    private float speed;
    private Bounds killBounds;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;

        if (killBounds.Contains(transform.position))
        {
            CenterHit();
        }
    }

    public void SetProperties(Vector3 dir, float s, Bounds bounds)
    {
        direction = dir;
        speed = s;
        killBounds = bounds;
    }

    public void CenterHit()
    {
        LevelRunner.AddMiss();
        Destroy(gameObject);
    }

    public void HitPlayer(int accuracy)
    {
        LevelRunner.AddHit(accuracy);
        Destroy(gameObject);
    }
}
