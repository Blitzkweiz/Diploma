using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    public GameObject objectToSpawn;
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
        if (lastSpawn + nextSpawn < Time.time && spawnTime < Time.time)
        {
            position = transform.position;
            Invoke("Spawn", 0.1f);
            lastSpawn = Time.time;
            nextSpawn = 9999f;

            if(spawnAwait > 60.0f)
            {
                spawnTime = float.MaxValue;
            }
        }
    }

    private void Spawn()
    {
        if (PhotonNetwork.InRoom)
        {
            MasterManager.NetworkInstantiate(objectToSpawn, position, Quaternion.identity);
        }
    }
}
