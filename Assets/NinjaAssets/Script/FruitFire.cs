using UnityEngine;
using System.Collections;

public class FruitFire : MonoBehaviour {

	public GameObject player;
	public float speed = 50f;
	public float ySpeed = 3f;


	public GameObject[] fruits;

	// Use this for initialization
	void Start () {

		Invoke ("fire", 3);
	}
	
	// Update is called once per frame
	void Update () {
		//lookAt (player.transform);
	}

	private void lookAt(Transform player){
		transform.LookAt (player);
		transform.Rotate (new Vector3 (45, 0, 0));
	}

	private GameObject chooseFruit(){
		int x = Random.Range (0, fruits.Length);
		return fruits[x];
	}

	private void fire(){
		lookAt (player.transform);
		GameObject clone = (GameObject)Instantiate (chooseFruit(), transform.position , Quaternion.identity);
		
		clone.GetComponent<Rigidbody>().velocity = (player.transform.position - transform.position).normalized * speed ; 
		clone.GetComponent<Rigidbody>().velocity = new Vector3(clone.GetComponent<Rigidbody>().velocity.x, ySpeed, clone.GetComponent<Rigidbody>().velocity.z);
		Invoke ("fire", Random.Range(2,5));
	}
}
