using UnityEngine;
using System.Collections;

namespace VesnaSanja{

	public class Matrix {
		
		private int nrows;
		private int ncols;
		private double[,] data;
		
		public Matrix(int nrow, int ncol) {
			this.nrows = nrow;
			this.ncols = ncol;
			data = new double [nrow , ncol];
		}

		public int getNrows()
		{
				return nrows;
		}

		public int getNcols()
		{
			return ncols;
		}

		public void setValueAt(int i, int j, double data1)
		{
				data [i, j] = data1;
		}

		public double getValueAt(int i, int j)
		{
				return data [i, j];
		}

	public bool isSquare() {
		return nrows == ncols;
	}
	
	public int size() {
		if (isSquare())
			return nrows;
		return -1;
	}
	    
	public static int changeSign(int i){
		if (i % 2==1)
						return -1;
				else
						return 1;

		}

	public Matrix multiplyByConstant(double constant) {
		Matrix mat = new Matrix(nrows, ncols);
		for (int i = 0; i < nrows; i++) {
			for (int j = 0; j < ncols; j++) {
				mat.setValueAt(i, j, data[i,j] * constant);
			}
		}
		return mat;
	}

	public static Matrix multiply(Matrix matrix1, Matrix matrix2)  {
		Matrix multipliedMatrix = new Matrix(matrix1.getNrows(), matrix2.getNcols());
		
		for (int i=0;i<multipliedMatrix.getNrows();i++) {
			for (int j=0;j<multipliedMatrix.getNcols();j++) {
				double sum = 0.0;
				for (int k=0;k<matrix1.getNcols();k++) {
					sum += matrix1.getValueAt(i, k) * matrix2.getValueAt(k, j);
				}
				multipliedMatrix.setValueAt(i, j, sum);
			}
		}
		return multipliedMatrix;
	}

		public static Matrix transpose(Matrix matrix) 
		{
			Matrix transposedMatrix = new Matrix(matrix.getNcols(), matrix.getNrows());
			for (int i=0;i<matrix.getNrows();i++) {
				for (int j=0;j<matrix.getNcols();j++) {
					transposedMatrix.setValueAt(j, i, matrix.getValueAt(i, j));
				}
			}
			return transposedMatrix;
		}	

		
	public static double determinant(Matrix matrix) {

		if (matrix.size() == 1){
			return matrix.getValueAt(0, 0);
		}
		
		if (matrix.size()==2) {
			return (matrix.getValueAt(0, 0) * matrix.getValueAt(1, 1)) - ( matrix.getValueAt(0, 1) * matrix.getValueAt(1, 0));
		}
		double sum = 0.0;
		for (int i=0; i<matrix.getNcols(); i++) {
			sum += changeSign(i) * matrix.getValueAt(0, i) * determinant(createSubMatrix(matrix, 0, i));
		}
		return sum;
	}

		public static Matrix createSubMatrix(Matrix matrix, int excluding_row, int excluding_col) {
			Matrix mat = new Matrix(matrix.getNrows()-1, matrix.getNcols()-1);
			int r = -1;
			for (int i=0;i<matrix.getNrows();i++) {
				if (i==excluding_row)
					continue;
				r++;
				int c = -1;
				for (int j=0;j<matrix.getNcols();j++) {
					if (j==excluding_col)
						continue;
					mat.setValueAt(r, ++c, matrix.getValueAt(i, j));
				}
			}
			return mat;
		} 

		public static Matrix cofactor(Matrix matrix) {
			Matrix mat = new Matrix(matrix.getNrows(), matrix.getNcols());
			for (int i=0;i<matrix.getNrows();i++) {
				for (int j=0; j<matrix.getNcols();j++) {
					mat.setValueAt(i, j, changeSign(i) * changeSign(j) * determinant(createSubMatrix(matrix, i, j)));
				}
			}
			
			return mat;
		}

		public static Matrix inverse(Matrix matrix)  {
			return (transpose(cofactor(matrix)).multiplyByConstant(1.0/determinant(matrix)));
		}

}

}