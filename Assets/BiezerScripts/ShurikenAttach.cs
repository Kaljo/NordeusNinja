using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class ShurikenAttach : MonoBehaviour {
	
	private Rigidbody rigidbody;

	void Start(){
		rigidbody = GetComponent<Rigidbody> ();
	}
	
	void OnTriggerEnter(Collider col){

		rigidbody.velocity = Vector3.zero;
		rigidbody.isKinematic = true;

	}

}
