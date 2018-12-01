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

    private void Start()
    {
        healthText.text = "Health: " + hitPoints;
    }

    public void TakeDamage()
	{
		hitPoints--;
        healthText.text = "Health: " + hitPoints;
		Debug.Log("Player hit! " + hitPoints + " HP left");
		if (hitPoints == 0)
		{
			Instantiate(explosionEffect, transform.position, transform.rotation);
            //Destroy(gameObject);
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
			Destroy(hook);
            StartCoroutine(EndGame());
		}
	}

    IEnumerator EndGame() {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
