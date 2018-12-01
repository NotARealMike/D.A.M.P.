using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthController : MonoBehaviour
{

	public int hitPoints = 10;
	public GameObject explosionEffect;
	public GameObject hook;
    public TextMeshProUGUI healthText;
    public ScoreTracker scoreTracker;

    private void Start()
    {
        healthText.text = "Health: " + hitPoints;
    }

    public void TakeDamage()
	{
		hitPoints--;
        if (hitPoints < 0) hitPoints = 0;
        healthText.text = "Health: " + hitPoints;
		Debug.Log("Player hit! " + hitPoints + " HP left");
		if (hitPoints == 0)
		{
			Instantiate(explosionEffect, transform.position, transform.rotation);
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
			Destroy(hook);
            PlayerPrefs.SetInt("Score", scoreTracker.score);
            StartCoroutine(EndGame());
		}
	}

    IEnumerator EndGame() {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
