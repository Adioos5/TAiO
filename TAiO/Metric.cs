using Accord.Math.Optimization;
using Accord.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics;

namespace TAiO
{
    public static class Metric
    {
        public static double CalculatePrecisely(DirectedGraph graph1, DirectedGraph graph2)
        {
            int numVertices = Math.Max(graph1.Vertices, graph2.Vertices);
            int[][] matrixA = (int[][])graph1.adjacencyMatrix.Clone();
            int[][] matrixB = (int[][])graph2.adjacencyMatrix.Clone();

            if (graph1.Vertices != graph2.Vertices)
                (matrixA, matrixB) = AdjacencyMatricesOperations.MakeMatricesOfEqualSize(graph1, graph2);

            double result = double.MaxValue;
            foreach (var permutation in Permutating.GeneratePermutations(numVertices))
            {
                double temp = 0.0;
                int[][] permutedGraphAdjacencyMatrix = Permutating.PermuteGraph(matrixB, permutation);

                for (int i = 0; i < numVertices; i++)
                {
                    for (int j = 0; j < numVertices; j++)
                    {
                        temp += Math.Pow(matrixA[i][j] - permutedGraphAdjacencyMatrix[i][j], 2);
                    }
                }

                result = Math.Min(Math.Sqrt(temp), result);
            }
            result = result + Math.Abs(graph1.Vertices - graph2.Vertices);

            return Math.Round(result, 2);
        }

        public static double Approximate(DirectedGraph graph1, DirectedGraph graph2)
        {
            int numVertices = Math.Max(graph1.Vertices, graph2.Vertices);
            int[][] matrixA = (int[][])graph1.adjacencyMatrix.Clone();
            int[][] matrixB = (int[][])graph2.adjacencyMatrix.Clone();

            if (graph1.Vertices != graph2.Vertices)
                (matrixA, matrixB) = AdjacencyMatricesOperations.MakeMatricesOfEqualSize(graph1, graph2);

            double[][] S = new double[numVertices][];
            for (int i = 0; i < numVertices; i++)
            {
                S[i] = new double[numVertices];
            }

            for (int i = 0; i < numVertices; i++)
            {
                for (int j = 0; j < numVertices; j++)
                {
                    S[i][j] = 1;
                }
            }

            for (int k = 1; k <= 10; k++)
            {
                double[][] YSXT = Matrix.Dot(matrixB, Matrix.Dot(S, Matrix.Transpose(matrixA)));
                double[][] YTSX = Matrix.Dot(Matrix.Transpose(matrixB), Matrix.Dot(S, matrixA));
                S = Elementwise.Add(YSXT, YTSX);
            }

            Munkres munkres = new Munkres(S);
            munkres.Maximize();

            double[] assignments = munkres.Solution;

            double result = double.MaxValue;
            double temp = 0.0;
            int[][] permutedGraphAdjacencyMatrix = Permutating.PermuteGraph(matrixB, assignments);

            for (int i = 0; i < numVertices; i++)
            {
                for (int j = 0; j < numVertices; j++)
                {
                    temp += Math.Pow(matrixA[i][j] - permutedGraphAdjacencyMatrix[i][j], 2);
                }
            }

            result = Math.Min(Math.Sqrt(temp), result) + Math.Abs(graph1.Vertices - graph2.Vertices);

            return Math.Round(result, 2);
        }
    }
}
