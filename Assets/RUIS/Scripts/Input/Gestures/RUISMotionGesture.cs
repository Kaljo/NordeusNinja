using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;



public class RUISMotionGesture : RUISGestureRecognizer 
{
	public int _maxSlope = 3;

	private readonly string configFolderPath = "C:\\Users\\Nordeus\\Downloads\\krug.txt"; // todo set it where your txt is;

	private int _minimumLength;
	

	private List<Vector3> gesture = new List<Vector3> ();

	double length;

	private bool gestureEnabled = false;

	private bool gestureState = false;

	//Input from kinect;
	RUISPointTracker points;

	public override bool GestureIsTriggered()
	{
		return gestureState;
	}
	public override bool GestureWasTriggered()
	{
		//TODO ignore - maybe
		return false;
	}
	
	public override float GetGestureProgress()
	{
		return gestureState ? 1 : 0;
	}
	public override void ResetProgress()
	{
		gestureState = false;
	}
	
	public override void EnableGesture()
	{
		gestureEnabled = true;
	}
	public override void DisableGesture()
	{
		gestureEnabled = false;
	}
	
	public override bool IsBinaryGesture()
	{
		//TODO dafuq is this ? Implement or just leave return false wont use anyway

		return false;
	}

	void Start()
	{
		points = GetComponent<RUISPointTracker> ();
	}

	public double Dtw(List<PointData> seq, List<Vector3> seq2R)
	{
		var seq1R = new List<Vector3> ();
		foreach (var iter in seq) {
			seq1R.Add (iter.position);
		}

		seq1R.Reverse ();
		seq2R.Reverse ();
		var tab = new double[seq1R.Count + 1, seq2R.Count + 1];
		var slopeI = new int[seq1R.Count + 1, seq2R.Count + 1];
		var slopeJ = new int[seq1R.Count + 1, seq2R.Count + 1];
		
		for (int i = 0; i < seq1R.Count + 1; i++) {
			for (int j = 0; j < seq2R.Count + 1; j++) {
				tab [i, j] = double.PositiveInfinity;
				slopeI [i, j] = 0;
				slopeJ [i, j] = 0;
			}
		}
		
		tab [0, 0] = 0;
		
		// Dynamic computation of the DTW matrix.
		for (int i = 1; i < seq1R.Count + 1; i++) {
			for (int j = 1; j < seq2R.Count + 1; j++) {
				if (tab [i, j - 1] < tab [i - 1, j - 1] && tab [i, j - 1] < tab [i - 1, j] &&
					slopeI [i, j - 1] < _maxSlope) {
					tab [i, j] = Dist2 (seq1R [i - 1], seq2R [j - 1]) + tab [i, j - 1];
					slopeI [i, j] = slopeJ [i, j - 1] + 1;
					slopeJ [i, j] = 0;
				} else if (tab [i - 1, j] < tab [i - 1, j - 1] && tab [i - 1, j] < tab [i, j - 1] &&
					slopeJ [i - 1, j] < _maxSlope) {
					tab [i, j] = Dist2 (seq1R [i - 1], seq2R [j - 1]) + tab [i - 1, j];
					slopeI [i, j] = 0;
					slopeJ [i, j] = slopeJ [i - 1, j] + 1;
				} else {
					tab [i, j] = Dist2 (seq1R [i - 1], seq2R [j - 1]) + tab [i - 1, j - 1];
					slopeI [i, j] = 0;
					slopeJ [i, j] = 0;
				}
			}
		}

		// Find best between seq2 and an ending (postfix) of seq1.
		double bestMatch = double.PositiveInfinity;
		for (int i = 1; i < (seq1R.Count + 1) - _minimumLength; i++)
		{
			if (tab[i, seq2R.Count] < bestMatch)
			{
				bestMatch = tab[i, seq2R.Count];
			}
		}
		
		return bestMatch;
	}

	private double Dist2(Vector3 first, Vector3 second)
	{
		//We dont use Y Cord since we only recognize 2d gestures
		//TODO find coordinate system on which we rely.
		first.y = 0;
		second.y = 0;

		return Vector3.Distance(first, second);

	}
	
	//ON AWAKE
	//load gesture from config file
	//calculate gesutre circumfence and put in length
	//maybe something more
	void Awake()
	{
		LoadModel (configFolderPath);

	}


	void Update () 
	{
		gestureState = false;
		//NOT SURE IF WE NEED TO check in each iteration. maybe once in some number of times


		//calculate how many points we need
		int numOfPoints = (int)((double)(length) / points.averageSpeed / Time.deltaTime);

		_minimumLength = (int)(0.9*numOfPoints);

		double similarity = Dtw (points.normalizedPoints, gesture);

		//TODO public variable treshold instead of 0.5
		if (similarity < 0.5) {
			gestureState = true;
			GestureEvent ();
		} else {
			if(!double.IsInfinity(similarity))
				Debug.LogWarning (similarity);
		}
	}

	void GestureEvent()
	{
		Debug.LogWarning ("Recognized motion");
	}


	/// <summary>
	/// Loads gesture model from file given path. File must be formated like x1 z1 x2 z2 ...
	/// </summary>
	/// <param name="path">Path.</param>
	private void LoadModel(string path)
	{
		string file = File.ReadAllText (path);
		string [] entries = file.Split (' ');

		for(int i=0; i<entries.Length-1; i+=2)
		{
			Vector3 toAdd = new Vector3(Convert.ToSingle(entries[i]), 0, Convert.ToSingle(entries[i+1]));

			gesture.Add(toAdd);
		}
	}

	private void calculateLength()
	{
		length = 0;
		for (int i=1; i<gesture.Count; i++) {
			length += Vector3.Distance(gesture[i], gesture[i-1]);
		}

	}

}