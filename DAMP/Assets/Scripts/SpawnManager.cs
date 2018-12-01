using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    public GameObject enemy;                // The enemy prefab to be spawned.
    public float spawnInterval = 5f;            // How long between each spawn.
    public float spawnAcceleration = 5e-3f;     // By how much per second the interval decreases
    public float minSpawnInterval = 0.2f;
    public static int side = 120;
    public int distance = 5;
    private Rect border = new Rect(-side / 2, -side / 2, side, side);
    private float lastSpawnTime;

    void Start()
    {
        lastSpawnTime = Time.time;
        // Call the Spawn function after a delay of the spawnTime and then continue to call after the same amount of time.
        //InvokeRepeating("Spawn", spawnTime, spawnTime);
    }

    void Update()
    {
        float timeSinceSpawn = Time.time - lastSpawnTime;
        if (timeSinceSpawn >= spawnInterval)
        {
            if(spawnInterval > minSpawnInterval)
                spawnInterval -= spawnAcceleration * timeSinceSpawn;
            lastSpawnTime = Time.time;
            Spawn();
        }
    }

    void Spawn()
    {
        // Find a random point on the perimeter
        int spawnPointIndex = Random.Range(0, 4 * side / distance);
        int sideIndex = spawnPointIndex * distance / side;
        int sidePosition = spawnPointIndex * distance % side;
        Vector3 spawnPosition;

        switch (sideIndex)
        {
            case 0:
                spawnPosition = new Vector3(sidePosition - side / 2, side / 2);
                break;
            case 1:
                spawnPosition = new Vector3(side / 2, sidePosition - side / 2);
                break;
            case 2:
                spawnPosition = new Vector3(sidePosition - side / 2, -side / 2);
                break;
            case 3:
                spawnPosition = new Vector3(-side / 2, sidePosition - side / 2);
                break;
            default:
                spawnPosition = new Vector3(0, 0);
                break;
        }

        // Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
        Instantiate(enemy, spawnPosition, Quaternion.identity);
    }
}
