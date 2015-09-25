using UnityEngine;
using System.Collections;

public class test : MonoBehaviour {

	void OnTriggerEnter(Collider other){
		Debug.Log ("dsadas");
	}

	void OnCollisionEnter(Collision collision){
		Debug.Log ("aaaaaaaaaaaaa");
	}
}
