using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    public Object objectToSpawn;
    public float spawnAwait;

    private float spawnTime;

    private float lastSpawn = 0.0f;
    private float nextSpawn = 0.0f;
    private Vector3 position;

    void Start()
    {
        spawnTime = Time.time + spawnAwait;
    }

    void Update()
    {
        //if (GameEvents.current.theBattleBegins && lastSpawn + nextSpawn < Time.time && spawnTime < Time.time)
        if (lastSpawn + nextSpawn < Time.time && spawnTime < Time.time)
        {
            position = transform.position;
            Invoke("Spawn", 0.1f);
            lastSpawn = Time.time;
            nextSpawn = Random.Range(3.0f, 8.0f);

            if(spawnAwait > 60.0f)
            {
                spawnTime = float.MaxValue;
            }
        }
    }

    private void Spawn()
    {
        Instantiate(objectToSpawn, position, Quaternion.identity);
    }
}
