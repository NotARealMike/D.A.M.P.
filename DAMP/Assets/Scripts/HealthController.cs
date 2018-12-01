using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{

	public int hitPoints = 10;
	public GameObject explosionEffect;
	public GameObject hook;
	
	public void TakeDamage()
	{
		hitPoints--;
		Debug.Log("Player hit! " + hitPoints + " HP left");
		if (hitPoints == 0)
		{
			Instantiate(explosionEffect, transform.position, transform.rotation);
			Destroy(gameObject);
			Destroy(hook);
		}
	}
}
