using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using VesnaSanja;

public class LeftKinectHand : MonoBehaviour {

	public GameObject sphere;
	public GameObject leftHand;
	public GameObject pathsColliders;
	
	private int frameRate = 2;
	
	private static int count = 4;
	private List<Vector3> vectors;
	
	public int br=0;
	// Use this for initialization
	void Start () {
		vectors = new List<Vector3> ();

	}
	
	void Update () {
		count --;
		if (count == 0) {
			count = frameRate;
			
			vectors.Add (leftHand.transform.TransformPoint (Vector3.zero));
			if (vectors.Count > 10) {
				vectors.RemoveAt (0);
			}
			if (vectors.Count > 3) {
				Koeficijenti.followPath (vectors.ToArray (), sphere, true, pathsColliders);
			}
		}
	}
	
}
