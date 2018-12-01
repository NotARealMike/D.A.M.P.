using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class HomingPlayer : MonoBehaviour {

    public Transform target;
    private Rigidbody2D rb;
    public float speed = 5f;
    public float rotateSpeed = 200f;
    public GameObject explosionEffect;
   

    // Use this for initialization
    void Start () {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();


    }
	
	// Update is called once per frame
	void FixedUpdate () {
        Vector2 direction = (Vector2)target.position - rb.position;
        direction.Normalize();

        float rotateAngle = Vector3.Cross(direction, transform.up).z;
        rb.angularVelocity = -rotateAngle * rotateSpeed;


        rb.velocity = transform.up * speed;
	}

    void OnTriggerEnter2D()
    {
        Explode();
    }

    void Explode()
    {
        Instantiate(explosionEffect, transform.position, transform.rotation);

        Destroy(gameObject);
    }
}
