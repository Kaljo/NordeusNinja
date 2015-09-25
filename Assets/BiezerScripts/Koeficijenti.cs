using UnityEngine;
using System.Collections;

namespace VesnaSanja{

public class Koeficijenti : MonoBehaviour {
	public static Vector3 p0;
	public static Vector3 p1;
	public static Vector3 p2;

	public static Vector3[] c0, c1, c2;
	public static Matrix C,T,M;
	public static MatrixVector P, N;
	public static Vector3[] Npoints;
	//Vector3[] vectors = new Vector3[6];

/*	Array(Vector4(0,10,2,0),Vector4(.5,5,3,.05),Vector4(1,0,4,.1),Vector4(1.5,2.5,5,.15),Vector4(2,5,6,.2)
	      ,Vector4(2.5,2.5,7,.25),Vector4(3,0,8,.3),Vector4(3.5,1.25,9,.35),Vector4(4,2.5,10,.4)).ToBuiltin(Vector4);*/
	
	/*void Start () {
		vectors[0] = new Vector3(0,10,2);
		vectors[1]= new Vector3(.5f,5,3);
		vectors [2] = new Vector3 (1, 0, 4);
		//vectors[3] = new Vector3(1.5f,2.5f,5);
		//vectors[4]= new Vector3(2,5,6);
		//vectors [5] = new Vector3 (2.5f,2.5f,7);
		vectors [3] = new Vector3 (3, 0, 8);
		vectors [4] = new Vector3 (3.5f, 1.25f, 9);
		vectors [5] = new Vector3(4,2.5f,10);

		Npoints= new Vector3[11];

		for(int x=0; x<vectors.Length; x++)
		{
			Vector3 temp = new Vector3(vectors[x].x,vectors[x].y,vectors[x].z);
			GameObject sphere= GameObject.CreatePrimitive(PrimitiveType.Sphere);
			sphere.GetComponent<Renderer>().material.color = Color.yellow;
			sphere.transform.position = temp;
			
		}
		C = new Matrix (11, 4);
		float k = 0f;
		for(int i=0;i<=10;i++)
			{

				C.setValueAt(i,0,Mathf.Pow(1-k,3));
				C.setValueAt(i,1,3*k*(1-k)*(1-k));
				C.setValueAt(i,2,3*k*k*(1-k));
			 	C.setValueAt(i,3, k*k*k);

			    k+=0.1f;
			}

		T = Matrix.transpose (C);

//		Matrix m = Matrix.multiply (T, C);
		//ispis (m);
//		Matrix n = Matrix.inverse (m);
		//ispis (n);



		M = Matrix.multiply(Matrix.inverse(Matrix.multiply (T,C)),T);


		Debug.Log("*******M*********");
		ispis (M);

		N = new MatrixVector (11,1);
		N.setValueAt (0, 0, new Vector3 (1, 1, 1)); 
		N.setValueAt (1, 0, new Vector3(1,3,5)); 
		N.setValueAt (2, 0, new Vector3 (2, 2, 2));
		N.setValueAt (3, 0, new Vector3 (5, 7, 6));
		N.setValueAt (4, 0, new Vector3 (9, 10, 3));
		N.setValueAt (5, 0, new Vector3(0,10,4));
		N.setValueAt (6, 0, new Vector3 (1, 15, 10));
		N.setValueAt (7, 0, new Vector3 (1, 0, 11));
		N.setValueAt (8, 0, new Vector3 (-2, 0, 10));
		N.setValueAt (9, 0, new Vector3 (5, 7, -3));
		N.setValueAt (10, 0,new Vector3(9,-2,1));

		findNpoints(vectors);
		N = convertToMatrix (Npoints);

		P = MatrixVector.multiplyByMatrix (M, N);

		for(int x=0;x<Npoints.Length; x++)
		{
			//Vector3 temp = new Vector3(vectors[x].x,vectors[x].y,vectors[x].z);
			GameObject sphere= GameObject.CreatePrimitive(PrimitiveType.Sphere);
			sphere.GetComponent<Renderer>().material.color = Color.green;

			sphere.transform.position = Npoints[x];

			
		}

		new Bezier (P.getValueAt (3, 0), P.getValueAt (2, 0), P.getValueAt (1, 0), P.getValueAt (0, 0), 150);



		for (int i=0; i<4; i++)
						Debug.Log (P.getValueAt (i, 0));



	}*/
	
	// Update is called once per frame
	void Update () {

	
		Debug.DrawRay(P.getValueAt (3, 0), P.getValueAt (2, 0) - P.getValueAt (3, 0), Color.blue);
		
		Debug.DrawRay (P.getValueAt (0, 0), P.getValueAt (1, 0) - P.getValueAt (0, 0), Color.blue);
	}

	public static void ispis(Matrix m){

		/*for (int i=0; i<m.getNrows(); i++)
				for (int j=0; j<m.getNcols(); j++)
					Debug.Log (m.getValueAt (i, j));*/
	}

	
	
	public static void findNpoints(Vector3[] points)
	{
		float[] duzineSegmenata= new float[points.Length-1];
		float length=0f;
        int		i = 0;
		Vector3  oldPoint = points [i];
		
		for (i=1; i<points.Length; i++) 
		{
			Vector3 newPoint = points[i];
			duzineSegmenata[i-1]=Vector3.Distance(oldPoint,newPoint); 
			int s=i-1;
			//Debug.Log ("Duzina segmenta [ "+s+ "]"+ duzineSegmenata[s]);
			length+=duzineSegmenata[i-1];
			oldPoint=newPoint;
		}


	//	Debug.Log ("Duzina cele linije  je: " + length);
		
		float segment = length / 10; //duzina desetog dela linije
		
		Npoints [0] = points [0];
		Npoints [10] = points [points.Length - 1];
		
		
		int k = 0;
		
		for (int j=1; j< 10; j++) 
		{
			float seg=segment;
			
			while (seg > duzineSegmenata[k]) 
			{
				seg-=duzineSegmenata[k];
				k++;
			}
			
			float odnos=seg/duzineSegmenata[k];

		//	Debug.Log("odnos je:" +odnos);
			Vector3 A=points[k];
			Vector3 B=points[k+1];
			Vector3 AB = B - A;
			//AB.Normalize();

			Npoints[j]=A + odnos * AB;

			duzineSegmenata[k]-=seg;
			points[k]=Npoints[j];
			
			if( odnos == 1) k++;
			
			
		}
		
	}

	public static MatrixVector convertToMatrix(Vector3[] points){

		MatrixVector m = new MatrixVector (points.Length, 1);
		for (int i=0; i<points.Length; i++)
						m.setValueAt (i, 0, points [i]);
		return m;
	}

		public static Vector3 calculatePath(Vector3[] vectors, GameObject prefab, GameObject sh, bool mode, GameObject pathsColls){

		Npoints= new Vector3[11];
		
		for(int x=0; x<vectors.Length; x++)
		{
			Vector3 temp = new Vector3(vectors[x].x,vectors[x].y,vectors[x].z);
			//GameObject sphere= GameObject.CreatePrimitive(PrimitiveType.Sphere);
			/*GameObject sphere = Instantiate(prefab);
			sphere.GetComponent<Renderer>().material.color = Color.yellow;
			sphere.transform.position = temp;*/
			
		}
		C = new Matrix (11, 4);
		float k = 0f;
		for(int i=0;i<=10;i++)
		{
			
			C.setValueAt(i,0,Mathf.Pow(1-k,3));
			C.setValueAt(i,1,3*k*(1-k)*(1-k));
			C.setValueAt(i,2,3*k*k*(1-k));
			C.setValueAt(i,3, k*k*k);
			
			k+=0.1f;
		}
		
		T = Matrix.transpose (C);
		
		//		Matrix m = Matrix.multiply (T, C);
		//ispis (m);
		//		Matrix n = Matrix.inverse (m);
		//ispis (n);
		
		
		
		M = Matrix.multiply(Matrix.inverse(Matrix.multiply (T,C)),T);
		
		
		//Debug.Log("*******M*********");
		ispis (M);
		
		/*N = new MatrixVector (11,1);
		N.setValueAt (0, 0, new Vector3 (1, 1, 1)); 
		N.setValueAt (1, 0, new Vector3(1,3,5)); 
		N.setValueAt (2, 0, new Vector3 (2, 2, 2));
		N.setValueAt (3, 0, new Vector3 (5, 7, 6));
		N.setValueAt (4, 0, new Vector3 (9, 10, 3));
		N.setValueAt (5, 0, new Vector3(0,10,4));
		N.setValueAt (6, 0, new Vector3 (1, 15, 10));
		N.setValueAt (7, 0, new Vector3 (1, 0, 11));
		N.setValueAt (8, 0, new Vector3 (-2, 0, 10));
		N.setValueAt (9, 0, new Vector3 (5, 7, -3));
		N.setValueAt (10, 0,new Vector3(9,-2,1));*/
		
		findNpoints(vectors);
		N = convertToMatrix (Npoints);
		
		P = MatrixVector.multiplyByMatrix (M, N);
		
		for(int x=0;x<Npoints.Length; x++)
		{
			//Vector3 temp = new Vector3(vectors[x].x,vectors[x].y,vectors[x].z);
			//GameObject sphere= GameObject.CreatePrimitive(PrimitiveType.Sphere);
			/*GameObject sphere = Instantiate(prefab);
			sphere.GetComponent<Renderer>().material.color = Color.green;
			
			sphere.transform.position = Npoints[x];
			*/
			
		}
		
		new Bezier (P.getValueAt (3, 0), P.getValueAt (2, 0), P.getValueAt (1, 0), P.getValueAt (0, 0), prefab, sh, mode, pathsColls, 100);

		//for (int i=0; i<4; i++)
		//	Debug.Log (P.getValueAt (i, 0));

		return P.getValueAt (2, 0);
	}

	public static Vector3 followPath(Vector3[] vectors, GameObject prefab, bool mode, GameObject pathsColls){
		
		Npoints= new Vector3[11];
		
		for(int x=0; x<vectors.Length; x++)
		{
		Vector3 temp = new Vector3(vectors[x].x,vectors[x].y,vectors[x].z);
			//GameObject sphere= GameObject.CreatePrimitive(PrimitiveType.Sphere);
			/*GameObject sphere = Instantiate(prefab);
		sphere.GetComponent<Renderer>().material.color = Color.yellow;
		sphere.transform.position = temp;*/
			
		}
		C = new Matrix (11, 4);
		float k = 0f;
		for(int i=0;i<=10;i++)
		{
			
			C.setValueAt(i,0,Mathf.Pow(1-k,3));
			C.setValueAt(i,1,3*k*(1-k)*(1-k));
			C.setValueAt(i,2,3*k*k*(1-k));
			C.setValueAt(i,3, k*k*k);
			
			k+=0.1f;
		}
		
		T = Matrix.transpose (C);
		
		//		Matrix m = Matrix.multiply (T, C);
		//ispis (m);
		//		Matrix n = Matrix.inverse (m);
		//ispis (n);
		
		
		
		M = Matrix.multiply(Matrix.inverse(Matrix.multiply (T,C)),T);
		
		
		//Debug.Log("*******M*********");
		ispis (M);
		
		/*N = new MatrixVector (11,1);
	N.setValueAt (0, 0, new Vector3 (1, 1, 1)); 
	N.setValueAt (1, 0, new Vector3(1,3,5)); 
	N.setValueAt (2, 0, new Vector3 (2, 2, 2));
	N.setValueAt (3, 0, new Vector3 (5, 7, 6));
	N.setValueAt (4, 0, new Vector3 (9, 10, 3));
	N.setValueAt (5, 0, new Vector3(0,10,4));
	N.setValueAt (6, 0, new Vector3 (1, 15, 10));
	N.setValueAt (7, 0, new Vector3 (1, 0, 11));
	N.setValueAt (8, 0, new Vector3 (-2, 0, 10));
	N.setValueAt (9, 0, new Vector3 (5, 7, -3));
	N.setValueAt (10, 0,new Vector3(9,-2,1));*/
		
		findNpoints(vectors);
		N = convertToMatrix (Npoints);
		
		P = MatrixVector.multiplyByMatrix (M, N);
		
		for(int x=0;x<Npoints.Length; x++)
		{
			//Vector3 temp = new Vector3(vectors[x].x,vectors[x].y,vectors[x].z);
			//GameObject sphere= GameObject.CreatePrimitive(PrimitiveType.Sphere);
			/*GameObject sphere = Instantiate(prefab);
		sphere.GetComponent<Renderer>().material.color = Color.green;
		
		sphere.transform.position = Npoints[x];
		*/
			
		}
		
		new Bezier (P.getValueAt (3, 0), P.getValueAt (2, 0), P.getValueAt (1, 0), P.getValueAt (0, 0), prefab, null, mode, pathsColls, 200);
		
		//for (int i=0; i<4; i++)
		//	Debug.Log (P.getValueAt (i, 0));
		
		return P.getValueAt (2, 0);
	}


}

}