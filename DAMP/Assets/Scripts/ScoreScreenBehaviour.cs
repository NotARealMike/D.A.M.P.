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
	
	// Update is called once per frame
	void Update () {
		
	}
}
