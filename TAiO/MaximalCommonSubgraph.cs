using Accord.Math.Optimization;
using Accord.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAiO
{
    public static class MaximalCommonSubgraph
    {
        public static int[][] FindPrecisely(DirectedGraph graph1, DirectedGraph graph2)
        {
            int numVertices = Math.Max(graph1.Vertices, graph2.Vertices);
            int[][] matrixA = (int[][])graph1.adjacencyMatrix.Clone();
            int[][] matrixB = (int[][])graph2.adjacencyMatrix.Clone();

            int[][] mcs = new int[numVertices][];
            int[][] resultMcs = new int[numVertices][];
            for (int i = 0; i < numVertices; i++)
            {
                mcs[i] = new int[numVertices];
                resultMcs[i] = new int[numVertices];
            }

            if (graph1.Vertices != graph2.Vertices)
                (matrixA, matrixB) = AdjacencyMatricesOperations.MakeMatricesOfEqualSize(graph1, graph2);

            double result2 = 0;
            foreach (var permutation in Permutating.GeneratePermutations(numVertices))
            {
                double temp = 0.0;
                for (int o = 0; o < numVertices; o++)
                {
                    for (int p = 0; p < numVertices; p++)
                    {
                        mcs[o][p] = 0;
                    }
                }
                int[][] permutedGraphAdjacencyMatrix = Permutating.PermuteGraph(matrixB, permutation);

                for (int i = 0; i < numVertices; i++)
                {
                    for (int j = 0; j < numVertices; j++)
                    {
                        if (matrixA[i][j] == 1 && permutedGraphAdjacencyMatrix[i][j] == 1)
                        {
                            temp += 1;
                            mcs[i][j] = 1;
                        }
                    }
                }

                if (temp > result2)
                {
                    result2 = temp;
                    resultMcs = (int[][])mcs.Clone();
                    mcs = new int[numVertices][];
                    for (int i = 0; i < numVertices; i++)
                    {
                        mcs[i] = new int[numVertices];
                    }
                }
            }
            DisplayGraph(GetRidOfIsolatedVertices(resultMcs, graph1, graph2));
            return GetRidOfIsolatedVertices(resultMcs, graph1, graph2);
        }
        public static int[][] Approximate(DirectedGraph graph1, DirectedGraph graph2)
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
            int[][] permutedGraphAdjacencyMatrix = Permutating.PermuteGraph(matrixB, assignments);

            int[][] mcs = new int[numVertices][];
            for (int i = 0; i < numVertices; i++)
            {
                mcs[i] = new int[numVertices];
            }
            for (int i = 0; i < numVertices; i++)
            {
                for (int j = 0; j < numVertices; j++)
                {
                    if (matrixA[i][j] == 1 && permutedGraphAdjacencyMatrix[i][j] == 1)
                    {
                        mcs[i][j] = 1;
                    }
                }
            }
            DisplayGraph(GetRidOfIsolatedVertices(mcs, graph1, graph2));
            return GetRidOfIsolatedVertices(mcs, graph1, graph2);
        }

        private static int[][] GetRidOfIsolatedVertices(int[][] matrix, DirectedGraph graph1, DirectedGraph graph2)
        {
            int[][] result = (int[][])matrix.Clone();
            int max = Math.Max(graph1.Vertices, graph2.Vertices);
            int limit = Math.Min(graph1.Vertices, graph2.Vertices);
            int currentVerticesAmount = max;

            for(int v = 0; v < max; v++)
            {
                if (currentVerticesAmount <= limit)
                    break;
                bool isIsolated = true;
                for (int i = 0; i < max; i++)
                {
                    if (matrix[i][v] == 1)
                    {
                        isIsolated = false;
                        break;
                    }
                }
                for (int j = 0; j < max; j++)
                {
                    if (matrix[v][j] == 1)
                    {
                        isIsolated = false;
                        break;
                    }
                }

                if (isIsolated)
                {
                    (int row, int column) = (v, v);
                    currentVerticesAmount -= 1;
                    int[][] temp = new int[currentVerticesAmount][];
                    for (int s = 0; s < currentVerticesAmount; s++)
                        temp[s] = new int[currentVerticesAmount];
                    int modifierRow = 0, modifierColumn = 0;
                    for(int i = 0; i < currentVerticesAmount; i++)
                    {
                        if (i == row)
                        {
                            modifierRow = 1;
                        }
                        for(int j = 0; j < currentVerticesAmount; j++)
                        {
                            if(j == column)
                            {
                                modifierColumn = 1;
                            }
                            temp[i][j] = result[i + modifierRow][j + modifierColumn];
                        }
                        modifierColumn = 0;
                    }
                    result = (int[][])temp.Clone();
                }
            }

            return result;
        }
        private static void DisplayGraph(int[][] m)
        {
            for (int i = 0; i < m.GetLength(0); i++)
            {
                for (int j = 0; j < m.GetLength(0); j++)
                {
                    Console.Write(m[i][j] + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }

}
