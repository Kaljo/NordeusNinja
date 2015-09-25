using UnityEngine;
using System.Collections;

public class FruitTriggerDestroy : MonoBehaviour {
	
	private bool collided = false;

	void OnTriggerEnter(Collider other){
		//Debug.Log (other.gameObject.tag);

		if (other.gameObject.tag.Equals("PlayerCollider") && collided == false){
			collided = true;

			GameObject parent = transform.parent.gameObject;


			GameObject child = parent.transform.GetChild(1).gameObject;
			child.transform.position = transform.position;
			child.SetActive(true);
			child.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-50, -500), Random.Range(-50, 350), Random.Range(-100,300)));
			Destroy (child, 3);

			child = parent.transform.GetChild(2).gameObject;
			child.transform.position = transform.position;
			child.SetActive(true);
			child.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(50, 500), Random.Range(-50, 350), Random.Range(-100,300)));
			Destroy (child, 3);
			Destroy(gameObject);
			Destroy (parent, 4);
			/*GameObject one = (GameObject)Instantiate(half, transform.position + new Vector3(1,1,1), Quaternion.identity); 
			one.GetComponent<Rigidbody>().AddForce(new Vector3(300, 0, 300));
			Destroy(one, 3);

			one = (GameObject)Instantiate(half, transform.position, Quaternion.identity); 
			one.GetComponent<Rigidbody>().AddForce(new Vector3(-400, 400, -300));
			Destroy(one, 3);
			*/                                
		}
	}

}
