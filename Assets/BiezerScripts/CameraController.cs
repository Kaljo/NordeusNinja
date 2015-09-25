using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public Transform head;

	void Update(){
		Debug.Log (head.position);
		transform.position = head.position;
	}
}
