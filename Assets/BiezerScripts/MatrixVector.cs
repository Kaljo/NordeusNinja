using UnityEngine;
using System.Collections;

namespace VesnaSanja{

public class MatrixVector 
{

	private int nrows;
	private int ncols;
	private Vector3[,] data;
	

	public MatrixVector(int nrow, int ncol) {
		this.nrows = nrow;
		this.ncols = ncol;
		data = new Vector3[nrow, ncol];
	}
	
	public int getNrows()
	{
		return nrows;
	}
	
	public int getNcols()
	{
		return ncols;
	}
	
	public void setValueAt(int i, int j, Vector3 data1)
	{
		data [i,j] = data1;
	}
	
	public Vector3 getValueAt(int i, int j)
	{
		return data [i,j];
	}
	
	public int changeSign(int i){
		return (int)Mathf.Pow (-1, i);
		
		
	}
	public MatrixVector multiplyByConstant(double alpha){
		
		
		for (int i=0; i<nrows; i++)
						for (int j=0; j<ncols; j++) {
			Vector3 v=new Vector3((float)(data[i,j].x * alpha), (float)(data[i,j].y*alpha),(float)(data[i,j].z*alpha));
			this.setValueAt (i, j, v);
						}
		return this;
		
	}

	public static MatrixVector multiplyByMatrix(Matrix m, MatrixVector n)
	{

				MatrixVector res = new MatrixVector (m.getNrows(), 1);

				for (int i=0; i<m.getNrows(); i++) 
				{
						Vector3 sum = new Vector3 (0, 0, 0);
						for (int j=0; j<m.getNcols(); j++)
				sum += new Vector3 ((float)(m.getValueAt (i, j) * n.getValueAt(j,0).x), (float)(m.getValueAt (i, j) * n.getValueAt(j,0).y), (float)(m.getValueAt (i, j) * n.getValueAt(j,0).z));
			res.setValueAt(i,0,sum);

				}
		return res;

	}

}

}
