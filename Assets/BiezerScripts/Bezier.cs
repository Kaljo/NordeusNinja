using UnityEngine;

namespace VesnaSanja{

	[System.Serializable]
	public class Bezier
	{
		
		public Vector3 p0;
		public Vector3 p1;
		public Vector3 p2;
		public Vector3 p3;
		
		public float length=0;
		
		public Vector3[] points;
		private GameObject sphere;
		private GameObject shuriken;
		private float speed = 5f;
		private bool mode;

		private Transform[] colliders;
		
		// Init function v0 = 1st point, v1 = handle of the 1st point , v2 = handle of the 2nd point, v3 = 2nd point
		// handle1 = v0 + v1
		// handle2 = v3 + v2
		public Bezier( Vector3 v0, Vector3 v1, Vector3 v2, Vector3 v3, GameObject prefab, GameObject sh, bool mode, GameObject pathsColls, int _calculatePoints=0)
		{
			this.p0 = v0;
			this.p1 = v1;
			this.p2 = v2;
			this.p3 = v3;
			this.mode = mode;
			if (pathsColls != null) {
				colliders = new Transform[pathsColls.transform.childCount];
				for (int i = 0; i < colliders.Length; i++) {
					colliders [i] = pathsColls.transform.GetChild (i);
				}
			}
			sphere = prefab;
			shuriken = sh;

			if(_calculatePoints>0) CalculatePoints(_calculatePoints);
		}
		
		// 0.0 >= t <= 1.0 her be magic and dragons
		public Vector3 GetPointAtTime( float t )
		{
			float u = 1f - t;
			float tt = t * t;
			float uu = u * u;
			float uuu = uu * u;
			float ttt = tt * t;
			
			Vector3 p = uuu * p0; //first term
			p += 3 * uu * t * p1; //second term
			p += 3 * u * tt * p2; //third term
			p += ttt * p3; //fourth term
			
			return p;
			
		}
		
		//where _num is the desired output of points and _precision is how good we want matching to be
		public void CalculatePoints(int _num, int _precision=200)
		{
			if(_num>_precision) Debug.LogError("_num must be less than _precision");
			
			//calculate the length using _precision to give a rough estimate, save lengths in array
			length=0;
			//store the lengths between PointsAtTime in an array
			float[] arcLengths = new float[_precision];
			
			Vector3 oldPoint = GetPointAtTime(0);
			
			for(int p=1;p<arcLengths.Length;p++)
			{
				Vector3 newPoint = GetPointAtTime((float) p/_precision); //get next point
				arcLengths[p] = Vector3.Distance(oldPoint,newPoint); //find distance to old point
				length += arcLengths[p]; //add it to the bezier's length
				oldPoint = newPoint; //new is old for next loop
			}
			
			//create our points array
			points = new Vector3[_num];
			//target length for spacing
			float segmentLength = length/_num;
			
			//arc index is where we got up to in the array to avoid the Shlemiel error http://www.joelonsoftware.com/articles/fog0000000319.html
			int arcIndex = 0;
			
			float walkLength=0; //how far along the path we've walked
			oldPoint = GetPointAtTime(0);
			GameObject sphere;
			//iterate through points and set them

				//following hand, with no fist
				int j = 0;
				for (int i=0; i<points.Length; i++) {
					float iSegLength = i * segmentLength; //what the total length of the walkLength must equal to be valid
					//run through the arcLengths until past it
					while (walkLength<iSegLength) {
						walkLength += arcLengths [arcIndex]; //add the next arcLength to the walk
						arcIndex++; //go to next arcLength
					}
					//walkLength has exceeded target, so lets find where between 0 and 1 it is
					points [i] = GetPointAtTime ((float)arcIndex / arcLengths.Length);
					if (i % 5 == 0) {
						//GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
						//sphere = Instantiate (this.sphere);
						//sphere.GetComponent<Renderer> ().material.color = Color.red;
						//sphere.transform.localScale /= 3;

						//sphere.transform.position = points [i];

						colliders[j].gameObject.GetComponent<Renderer> ().material.color = Color.red;

						colliders[j].position = points[i];
						j++;
					}
				}
			if (mode == false){
				/*
				sphere = Instantiate (this.sphere);
				sphere.GetComponent<Renderer>().material.color = Color.blue;
				sphere.transform.position = p0;

				sphere = Instantiate(this.sphere);
				sphere.GetComponent<Renderer>().material.color = Color.red;
				sphere.transform.position = p1;

				sphere = Instantiate(this.sphere);
				sphere.GetComponent<Renderer>().material.color = Color.green;
				sphere.transform.position = p2;


				sphere = Instantiate(this.sphere);
				sphere.GetComponent<Renderer>().material.color = Color.black;
				sphere.transform.position = p3;*/
					
				//ovo radi solidno, poslednje 2
				//tangent(p0, p1);
				int x = colliders.Length;
				MyoShuriken.tangent(shuriken, colliders[x - 5].position, colliders[ x - 2].position, speed);
				//tangent (p1, p2);
			}


		}

		/*private void tangent(Vector3 pointOne, Vector3 pointTwo){
			Debug.Log ("tangent");

			//Debug.DrawLine (pointOne, pointTwo);
			GameObject shur = (GameObject)Instantiate (shuriken, pointOne, Quaternion.identity);
			shur.GetComponent<Rigidbody>().velocity = (pointOne - pointTwo).normalized * speed;
			//shur.GetComponent<Rigidbody> ().velocity -= new Vector3 (0, shur.GetComponent<Rigidbody> ().velocity.y , 0);

			Destroy (shur, 5);

		}*/
		
	}
}