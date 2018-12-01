using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionBehaviour : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine(e());
	}
	
	IEnumerator e()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }
}
