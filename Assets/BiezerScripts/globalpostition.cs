using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using VesnaSanja;
/*
public class globalpostition : MonoBehaviour {

	public GameObject sphere;
	public GameObject shuriken;
	private const int frameRate = 3;

	private int count = frameRate;
	private List<Vector3> vectors;

	void Start () {
		vectors = new List<Vector3> ();
	}
	
	// Update is called once per frame
	void Update () {
		count --;
		if (count == 0) {
			count = frameRate;
			//vectors.RemoveAt(0);
			vectors.Add(transform.TransformPoint(Vector3.zero));
			testIfMoving();

			if (notMoving == 4){
				notMoving++;
				if(vectors.Count<10)
					vectors.Clear();
				else
				{
					for(int i=0; i<6;i ++)
						vectors.RemoveAt(vectors.Count-1);
					while (vectors.Count>6)
						vectors.RemoveAt(0);
					bool t=true;
					for(int i=(vectors.Count-3>1?vectors.Count-3:1); i<vectors.Count; i++)
						if(vectors[i-1].z>vectors[i].z)
							t=false;

					vectors.Clear ();
				}
			}
		}
	
	}

	private void testIfMoving(){
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
	}

}*/







