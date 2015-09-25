using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using VesnaSanja;

using LockingPolicy = Thalmic.Myo.LockingPolicy;
using Pose = Thalmic.Myo.Pose;
using UnlockType = Thalmic.Myo.UnlockType;
using VibrationType = Thalmic.Myo.VibrationType;

public class RightKinectHand : MonoBehaviour {

	public GameObject myo = null;
	private ThalmicMyo tMyo;
	
	//private static Pose lastPose = Pose.Unknown;
	
	public GameObject sphere;
	public GameObject shuriken;
	public GameObject hand;
	public GameObject foreArm;
	public GameObject upperArm;
	public GameObject pathsColliders;
	
	private const int numOfDots = 6;
	private int frameRate = 4;
	private const float distanceLimit = 0.01f;
	
	private static int count = numOfDots;
	private List<Vector3> vectors;
	private bool thrown = false;
	private float speed=5f;

	void Start () {
		tMyo = myo.GetComponent<ThalmicMyo> ();
		vectors = new List<Vector3> ();
	}
	
	void Update () {
		if (tMyo.pose == Pose.Fist) {
			thrown = true;
			Debug.Log(thrown);
			//pathsColliders.SetActive(false);
		}else{
			if(thrown)
			{
				float dis=Vector3.Distance(hand.transform.TransformPoint(Vector3.zero), foreArm.transform.TransformPoint(Vector3.zero));
				dis+=Vector3.Distance(upperArm.transform.TransformPoint(Vector3.zero), foreArm.transform.TransformPoint(Vector3.zero));
				dis-=Vector3.Distance(hand.transform.TransformPoint(Vector3.zero), upperArm.transform.TransformPoint(Vector3.zero));

				if(dis<distanceLimit) 
				{
					float angle=0f;
					if(vectors.Count>3)
					{
						Vector3 a=vectors[vectors.Count-1]-vectors[vectors.Count-3];
						angle=Mathf.Tan(a.y/a.x)*180/Mathf.PI;
					}
					tangent(shuriken,upperArm.transform.TransformPoint(Vector3.zero), hand.transform.TransformPoint(Vector3.zero), speed, angle);
					thrown=false;

					//pathsColliders.SetActive(true);
				}

				vectors.Clear();
			}
			count --;
			if (count == 0) {
				count = frameRate;
				vectors.Add(hand.transform.TransformPoint(Vector3.zero));
				if (vectors.Count > 10)
					vectors.RemoveAt(0);
				if(!thrown)
					if (vectors.Count > 3)
						Koeficijenti.followPath(vectors.ToArray(), sphere, true, pathsColliders);
			}
		}
	}
	
	public static void tangent(GameObject shuriken, Vector3 pointOne, Vector3 pointTwo, float speed, float angle){
		Debug.Log ("tangent");
		GameObject shur = (GameObject)Instantiate (shuriken, pointTwo, Quaternion.identity);
		shur.GetComponent<Rigidbody>().velocity = (pointTwo - pointOne).normalized * speed;
		shur.transform.LookAt (pointOne);

		//Vector3 dot = pathsColliders.transform.GetChild (10).transform.position;

		shur.transform.Rotate(new Vector3(0, 0, angle));
		shur.GetComponent<Rigidbody> ().AddTorque (shur.transform.up * 600f);

		//shur.GetComponent<Rigidbody> ().velocity -= new Vector3 (0, shur.GetComponent<Rigidbody> ().velocity.y , 0);
		
		Destroy (shur, 5);
	}
}