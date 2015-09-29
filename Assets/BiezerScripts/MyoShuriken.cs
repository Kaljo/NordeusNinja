using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using VesnaSanja;

using LockingPolicy = Thalmic.Myo.LockingPolicy;
using Pose = Thalmic.Myo.Pose;
using UnlockType = Thalmic.Myo.UnlockType;
using VibrationType = Thalmic.Myo.VibrationType;

public class MyoShuriken : MonoBehaviour {

	//public GameObject aim;

	public GameObject myo = null;

	private ThalmicMyo tMyo;

	private static Pose lastPose = Pose.Unknown;

	public GameObject sphere;
	public GameObject shuriken;
	public GameObject hand;
	public GameObject pathsColliders;
	
	private const int numOfDots = 6;
	private int frameRate = 4;
	private const float distanceLimit = 0.05f;
	
	private static int count = 4;
	private List<Vector3> vectors;
	private static volatile bool thrown=false;
	
	public int br=0;
	// Use this for initialization
	void Start () {
		tMyo = myo.GetComponent<ThalmicMyo> ();
		vectors = new List<Vector3> ();

		//thrown = false;
	}
	
	// Update is called once per frame
	/*
	 * Tamarin update za suriken :D
	 * void Update () {
		count --;
		if (count == 0) {
			count = frameRate;

			if (tMyo.pose == Pose.Fist) {
				pathsColliders.SetActive(false);
				Debug.Log ("STISNUTA");
				vectors.Add (hand.transform.TransformPoint (Vector3.zero));
				thrown = false;
				if (lastPose != Pose.Fist )
					vectors.Clear ();
				//vectors.Clear ();
				//frameRate = 1;
			} else {
				//Debug.Log ("PUSTENA");
				if (lastPose == Pose.Fist && thrown == false) {
					Debug.Log ("BACANJE");
					thrown = true;
					lastPose = tMyo.pose; 
					if (vectors.Count >= 3) {
						while (vectors.Count>40)
							vectors.RemoveAt (0);
						calculateShurikenPath();
						//Koeficijenti.calculatePath (vectors.ToArray (), sphere, shuriken, false, pathsColliders);
						br++;

						Debug.Log ("baca suriken " + br);
					}
					vectors.Clear ();
					//int x = pathsColliders.transform.childCount;
					//tangent(shuriken, pathsColliders.transform.GetChild(x-5).position, pathsColliders.transform.GetChild(x-2).position, 5f);
					//frameRate = 4;
					pathsColliders.SetActive(true);
				}else{
					vectors.Add(hand.transform.TransformPoint(Vector3.zero));
					if (vectors.Count > 6){
						vectors.RemoveAt(0);
					}
					if (vectors.Count > 3){
						Koeficijenti.followPath(vectors.ToArray(), sphere, true, pathsColliders);
					}
				}	
			}
			lastPose = tMyo.pose;
		}
	}*/

	void Update () {
		count --;
		if (count == 0) {
			count = frameRate;

			vectors.Add(hand.transform.TransformPoint(Vector3.zero));
			if (vectors.Count > 10){
				vectors.RemoveAt(0);
			}
			if (vectors.Count > 3){
				Koeficijenti.followPath(vectors.ToArray(), sphere, true, pathsColliders);
			}

			if (tMyo.pose == Pose.Fist) {
				//pathsColliders.SetActive(false);
				count=1;
				Debug.Log ("STISNUTA");
				//vectors.Add (hand.transform.TransformPoint (Vector3.zero));
				thrown = false;
			} else {
				//Debug.Log ("PUSTENA");
				if (lastPose == Pose.Fist && thrown == false) {
					Debug.Log ("BACANJE");
					thrown = true;
					lastPose = tMyo.pose;
					if (vectors.Count >= 3) {
						while (vectors.Count>40)
							vectors.RemoveAt (0);
						calculateShurikenPathRed();
						//Koeficijenti.calculatePath (vectors.ToArray (), sphere, shuriken, false, pathsColliders);
						br++;
						
						Debug.Log ("baca suriken " + br);
					}
					vectors.Clear ();
					//pathsColliders.SetActive(true);
				}else{
					/*vectors.Add(hand.transform.TransformPoint(Vector3.zero));
					if (vectors.Count > 10){
						vectors.RemoveAt(0);
					}
					if (vectors.Count > 3){
						Koeficijenti.followPath(vectors.ToArray(), sphere, true, pathsColliders);
					}*/
				}	
			}
			lastPose = tMyo.pose;
		}
	}

	public static void tangent(GameObject shuriken, Vector3 pointOne, Vector3 pointTwo, float speed){
		Debug.Log ("tangent");
		GameObject shur = (GameObject)Instantiate (shuriken, pointOne, Quaternion.identity);
		shur.GetComponent<Rigidbody>().velocity = (pointTwo - pointOne).normalized * speed;
		//shur.GetComponent<Rigidbody> ().velocity -= new Vector3 (0, shur.GetComponent<Rigidbody> ().velocity.y , 0);
		
		Destroy (shur, 5);
	}

	private void calculateShurikenPath(){
		Vector3 pointOne = new Vector3(0,0,0);
		Vector3 pointTwo = new Vector3(0,0,0);
		Vector3 pointSp = new Vector3 (0, 0, 0);
		Debug.Log (vectors.Count);
		for (int i=0; i<vectors.Count/2; i++)
			pointOne += vectors [i];
		for (int i=vectors.Count/2; i<vectors.Count; i++)
			pointTwo += vectors [i];
		pointOne /= vectors.Count / 2;
		pointTwo /= vectors.Count - vectors.Count / 2;
		//pointSp += 2 * pointTwo - pointOne;
		tangent (shuriken, pointOne, pointTwo, 5f);

	}

	private void calculateShurikenPathRed(){

		int x = pathsColliders.transform.childCount;

		Vector3 pointOne = Vector3.zero;
		for (int i = 0; i < 5; i++){
			pointOne += pathsColliders.transform.GetChild(x-i-6).position;
		}
		pointOne /= 5;

		Vector3 pointt = Vector3.zero;
		for (int i = 0; i < 5; i++){
			pointt += pathsColliders.transform.GetChild(x-i-11).position;
		}
		pointt /= 5;

		tangent (shuriken, pointOne, pointt, 5f);

	}

	/*private void testIfMoving(){
		if (vectors.Count < 2) {
			notMoving = 0;
			return;
		}
		float distance = Vector3.Distance (vectors [vectors.Count - 1], vectors [vectors.Count - 2]);
		if (distance < distanceLimit) {
			notMoving++;
		} else {
			notMoving = 0;
		}
	}
	
	void OnGUI(){
		GUI.Box (new Rect (100, 100, 100, 30), "" + notMoving);
	}*/
}
