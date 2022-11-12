using System;

namespace GaussAlgorithm
{
    public class Solver
    {
        public double[] FindResults(double[][] matrix, double[] freeMembers)
        {
            var result = new double[matrix[0].Length];
            for (int i = 0, n = matrix.Length; i < n; ++i)
            {
                var flag = true;
                for (int j = 0; j < matrix[i].Length; ++j)
                {
                    if (matrix[i][j] != 0)
                    {
                        result[j] = freeMembers[i] / matrix[i][j];
                        flag = false;
                        break;
                    }
                }
                if (flag && freeMembers[i] != 0)
                    throw new ArgumentException("no solution");
            }
            return result;
        }

        public void ReduceMatrix(double[][] matrix, double[] freeMembers)
        {
            var busyRow = new bool[matrix.Length];
            for (int i = 0, n = matrix[0].Length; i < n; ++i)
            {
                var leadingRow = 0;
                var leadingElement = 0.0;
                for (int j = 0, m = matrix.Length; j < m; ++j)
                {
                    if (matrix[j][i] != 0 && !busyRow[j])
                    {
                        leadingElement = matrix[j][i];
                        busyRow[j] = true;
                        leadingRow = j;
                        break;
                    }
                }
                if (leadingElement != 0)
                {
                    for (int j = 0, m = matrix.Length; j < m; ++j)
                    {
                        if (j != leadingRow)
                        {
                            var coef = matrix[j][i] / leadingElement;
                            freeMembers[j] = Math.Round(freeMembers[j] - coef * freeMembers[leadingRow], 9);
                            for (int k = i; k < n; ++k)
                                matrix[j][k] = Math.Round(matrix[j][k] - coef * matrix[leadingRow][k], 9);
                        }
                    }
                }
            }
        }

        public double[] Solve(double[][] matrix, double[] freeMembers)
        {
            ReduceMatrix(matrix, freeMembers);
            return FindResults(matrix, freeMembers);
        }
    }
}
