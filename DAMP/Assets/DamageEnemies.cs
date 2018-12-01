using UnityEngine;

public class DamageEnemies : MonoBehaviour
{
	private bool active = false;
    public ScoreTracker score;
    public SpriteRenderer damageAura;
	
	public void TurnDamageOn()
	{
		active = true;
        damageAura.enabled = true;
		foreach (var col in Physics2D.OverlapCircleAll(gameObject.transform.position, 10f))
		{
			if (col.gameObject.layer == LayerMask.NameToLayer("Enemy"))
			{
				col.gameObject.GetComponent<HomingPlayer>().Explode();
                score.Increment();
			}
		}
	}

	public void TurnDamageOff()
	{
		active = false;
        damageAura.enabled = false;
	}
	
	void OnTriggerEnter2D(Collider2D col)
	{
		if (active && col.gameObject.layer == LayerMask.NameToLayer("Enemy"))
		{
			col.gameObject.GetComponent<HomingPlayer>().Explode();
            score.Increment();
		}
	}
}
