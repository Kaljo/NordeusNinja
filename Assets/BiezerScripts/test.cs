using UnityEngine;
using System.Collections;

public class test : MonoBehaviour {

	public GameObject shuriken;

	void Update(){

		if (Input.GetMouseButtonDown(0)){
			GameObject s = (GameObject)Instantiate(shuriken, new Vector3(0, Random.Range(1, 4), 0), Quaternion.identity);
			s.GetComponent<Rigidbody> ().AddTorque (s.transform.up * 600f);
			s.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-500, 500),0, 1000));
		}

	}


}
