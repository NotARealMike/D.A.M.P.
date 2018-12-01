using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPillarSpawner : MonoBehaviour {

    public GameObject obj;
    //public GameObject wallPrefab;
    public int objectNumber = 5;
    public float boxSide;
    public float speedReduction;

	// Use this for initialization
	void Start () {
        for (int i = 0; i < objectNumber; i++)
            Spawn();
	}

    void Spawn() {
        Debug.Log("Spawning moving pillar");
        float x = Random.Range(-boxSide / 2, boxSide / 2);
        float y = Random.Range(-boxSide / 2, boxSide / 2);
        Vector3 position = new Vector3(x, y, 0);

        float vx = Random.Range(-boxSide / 2, boxSide / 2) / speedReduction;
        float vy = Random.Range(-boxSide / 2, boxSide / 2) / speedReduction;
        Vector3 vel = new Vector3(vx, vy, 0);

        GameObject instance = Instantiate(obj, position, Quaternion.identity);
        instance.GetComponent<Rigidbody2D>().velocity = vel;
    }
}
