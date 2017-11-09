using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace ParallelLibrary
{
    public class MatrixManager
    {
        public int[,] Multiply(int[,] matrixA, int[,] matrixB)
        {
            if (matrixA.GetLength(1) != matrixB.GetLength(0))
            {
                throw new ArgumentException("MatrixA columns number should be equals to MatrixB rows number!");
            }

            var matrixC = MultiplyWithParallelLooping(matrixA, matrixB);

            return matrixC;
        }

        private static int[,] MultiplyWithParallelLooping(int[,] matrixA, int[,] matrixB)
        {
            int[,] matrixC = new int[matrixA.GetLength(0), matrixB.GetLength(1)];
            var parallelRows = matrixA.GetLength(0);
            Parallel.For(0, parallelRows, i =>
            {
                for (int j = 0; j < matrixB.GetLength(1); j++)
                {
                    for (int k = 0; k < matrixB.GetLength(0); k++)
                    {
                        matrixC[i, j] += matrixA[i, k] * matrixB[k, j];
                    }
                }
            });

            return matrixC;
        }

        public int SumOfElements(int[,] matrix)
        {
            var sum = 0;
            Parallel.For(0, matrix.GetLength(0), i =>
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    sum += matrix[i, j];
                }
            });

            return sum;
        }
    }
}
