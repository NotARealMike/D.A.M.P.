using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookshotBehaviour : MonoBehaviour
{
	private enum HookState
	{
		Idle,
		Shooting,
		Pulling
	}

	public GameObject player;
	
	private HookState state = HookState.Idle;
	
	private Vector2 direction = Vector2.right;

	public float Speed;
	public float Force;
	public float OrbitRadius;
	public float OrbitSpeed;
	public float MaxRadius = 30;
	
	
	void Update ()
	{
		switch (state)
		{
			case HookState.Idle:
				direction.Set(OrbitRadius*Mathf.Cos(OrbitSpeed*Time.time), OrbitRadius*Mathf.Sin(OrbitSpeed*Time.time));
				gameObject.transform.position = player.transform.position + (Vector3)direction;
				if (Input.anyKeyDown)
				{
					state = HookState.Shooting;					
				}
				break;
			case HookState.Shooting:
				gameObject.transform.Translate(Time.deltaTime*direction*Speed);
				if ((player.transform.position - transform.position).sqrMagnitude > MaxRadius*MaxRadius)
				{
					state = HookState.Idle;
					transform.SetPositionAndRotation(player.transform.position + (Vector3)direction, Quaternion.identity);
				}
				break;
			case HookState.Pulling:
				player.GetComponent<Rigidbody2D>().AddForceAtPosition(direction*Force, Vector2.zero);
				if ((player.transform.position - gameObject.transform.position).sqrMagnitude < 10)
				{
					direction.Set(OrbitRadius*Mathf.Cos(OrbitSpeed*Time.time), OrbitRadius*Mathf.Sin(OrbitSpeed*Time.time));
					state = HookState.Idle;
					transform.SetPositionAndRotation(player.transform.position + (Vector3)direction*OrbitRadius, Quaternion.identity);
					player.GetComponent<DamageEnemies>().TurnDamageOff();
				}
				break;
		}
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		Debug.Log("Hook hit");
		if (state == HookState.Shooting)
		{
			state = HookState.Pulling;
			player.GetComponent<DamageEnemies>().TurnDamageOn();
		}
	}
}
