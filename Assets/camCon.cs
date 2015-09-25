using UnityEngine;
using System.Collections;

public class camCon : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	
	void Update(){
		Debug.Log ("glava "+ transform.TransformPoint (Vector3.zero));
		//transform.position = head.TransformPoint(Vector3.zero);
	}
}
