using UnityEngine;
using System.Collections;

public class FruitCanon : MonoBehaviour {

	//public Rigidbody prefab;
	public GameObject[] prefabs;
	public Transform target;
	public float angle = 45f;

	private Vector3 target2;
	void Start () {
		Invoke ("fire", Random.Range(1, 5));
	}

	void Update(){
		//vector3 target2 = target.position- new Vector3 (0, target.position.y, 0);// - new Vector3 ((float)Random.Range (-50, 50) / 30, target.position.y, 0);
		transform.LookAt (target.position- new Vector3 ((float)Random.Range (-50, 50) / 1500, target.position.y, 0));
		angle = Random.Range (30, 55);
		transform.Rotate (Vector3.right, -angle);

	}

	public void fire(){

		float g = -Physics.gravity.y;

		Vector3 start = transform.position - new Vector3 (0, transform.position.y, 0);
		//Debug.Log (start);
		Vector3 end = target.position - new Vector3 (0,target.position.y, 0);
		//Debug.Log (end);
		//end += new Vector3 ((float)Random.Range (-150, 150) / 10, (float)Random.Range (0, 50) / 100, 0);
		float distance = Vector3.Distance (start, end);
		//distance += Random.Range (50, 100) / 100;
		//Debug.Log (distance);
		float radians = angle * 2 * Mathf.PI / 360;
		float speed = Mathf.Sqrt (distance * g / Mathf.Sin (2 * radians));


		GameObject projectile = (GameObject)Instantiate (prefabs[Random.Range(0, prefabs.Length)], transform.position, Quaternion.identity);
		projectile.transform.GetChild(0).GetComponent<Rigidbody> ().AddForce (transform.forward * speed, ForceMode.VelocityChange);
		Destroy (projectile, 10);

		Invoke ("fire", Random.Range(2,8));
	}



}
