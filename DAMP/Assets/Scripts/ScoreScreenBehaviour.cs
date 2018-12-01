using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreScreenBehaviour : MonoBehaviour {
    
    public TextMeshProUGUI scoreText;

	// Use this for initialization
	void Start () {
        scoreText.text = PlayerPrefs.GetInt("Score").ToString();
	}
	
	public void Back()
    {
        PlayerPrefs.SetInt("Menu", 0);
    }
}
