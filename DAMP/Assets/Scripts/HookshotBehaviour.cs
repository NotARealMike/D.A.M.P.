using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

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
	public float AutoaimDegrees = 1800;
	public float PullTimeout = 1;

	private float pullStartTime;
	
	
	void Update ()
	{
		switch (state)
		{
			case HookState.Idle:
				direction.Set(OrbitRadius*Mathf.Cos(OrbitSpeed*Time.time), OrbitRadius*Mathf.Sin(OrbitSpeed*Time.time));
				gameObject.transform.position = player.transform.position + (Vector3)direction;
				if (Input.anyKeyDown)
				{
					autoaimDirection();
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
				if ((player.transform.position - gameObject.transform.position).sqrMagnitude < 10
				    || Time.time > pullStartTime + PullTimeout)
				{
					direction.Set(OrbitRadius*Mathf.Cos(OrbitSpeed*Time.time), OrbitRadius*Mathf.Sin(OrbitSpeed*Time.time));
					state = HookState.Idle;
					transform.SetPositionAndRotation(player.transform.position + (Vector3)direction*OrbitRadius, Quaternion.identity);
					player.GetComponent<DamageEnemies>().TurnDamageOff();
				}
				break;
		}
	}

	private void autoaimDirection()
	{
		float closestDist2 = float.PositiveInfinity;
		Collider2D match = null;
		foreach (var col in Physics2D.OverlapCircleAll(transform.position, MaxRadius))
		{
			Vector2 relPos = col.transform.position - transform.position;
			if (Vector2.Angle(direction, relPos) <= AutoaimDegrees / 2
			    && relPos.sqrMagnitude < closestDist2
			    && (col.gameObject.layer == LayerMask.NameToLayer("Enemy") 
			        || col.gameObject.layer == LayerMask.NameToLayer("Obstacle")))
			{
				closestDist2 = relPos.sqrMagnitude;
				match = col;
			}
		}

		if (match != null)
		{
			direction = (match.transform.position-transform.position).normalized;
		}
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (state == HookState.Shooting)
		{
			state = HookState.Pulling;
			pullStartTime = Time.time;
			player.GetComponent<DamageEnemies>().TurnDamageOn();
		}
	}
}
