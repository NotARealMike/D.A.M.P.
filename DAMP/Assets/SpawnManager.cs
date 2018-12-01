using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {

    public GameObject enemy;                // The enemy prefab to be spawned.
    public float spawnTime = 2f;            // How long between each spawn.
    public static int side = 120;
    public int distance = 5;
    private Rect border = new Rect(-side / 2, -side / 2, side, side);


    void Start() {
        // Call the Spawn function after a delay of the spawnTime and then continue to call after the same amount of time.
        InvokeRepeating("Spawn", spawnTime, spawnTime);
    }


    void Spawn() {
        // Find a random point on the perimeter
        int spawnPointIndex = Random.Range(0, 4*side/distance);
        int sideIndex = spawnPointIndex * distance / side;
        int sidePosition = spawnPointIndex * distance % side;
        Vector3 spawnPosition;

        switch(sideIndex) {
            case 0:
                spawnPosition = new Vector3(sidePosition - side / 2, side / 2);
                break;
            case 1:
                spawnPosition = new Vector3(side / 2, sidePosition - side / 2);
                break;
            case 2:
                spawnPosition = new Vector3(sidePosition - side / 2, - side / 2);
                break;
            case 3:
                spawnPosition = new Vector3(- side / 2, sidePosition - side / 2);
                break;
            default:
                spawnPosition = new Vector3(0, 0);
                break;
        }

        // Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
        Instantiate(enemy, spawnPosition, Quaternion.identity);
    }
}
