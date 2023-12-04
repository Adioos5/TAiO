using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAiO
{
    public static class AdjacencyMatricesOperations
    {
        public static (int[][], int[][]) MakeMatricesOfEqualSize(DirectedGraph graph1, DirectedGraph graph2)
        {
            int numVertices = Math.Max(graph1.Vertices, graph2.Vertices);
            int[][] matrixA = new int[numVertices][];
            int[][] matrixB = new int[numVertices][];

            for(int i = 0;i<numVertices;i++)
            {
                matrixA[i] = new int[numVertices];
                matrixB[i] = new int[numVertices];
            }

            if (graph1.Vertices < graph2.Vertices)
            {
                int[][] temp = new int[numVertices][];
                for(int k = 0;k<numVertices;k++)
                    temp[k] = new int[numVertices];
                for (int i = 0; i < graph2.Vertices; i++)
                {
                    for (int j = 0; j < graph2.Vertices; j++)
                    {
                        if (i < graph1.Vertices && j < graph1.Vertices)
                        {
                            temp[i][j] = graph1.adjacencyMatrix[i][j];
                        }
                        else
                        {
                            temp[i][j] = 0;
                        }
                    }
                }

                matrixA = (int[][])temp.Clone();
                matrixB = (int[][])graph2.adjacencyMatrix.Clone();
            }
            else if (graph1.Vertices > graph2.Vertices)
            {
                int[][] temp = new int[numVertices][];
                for (int k = 0; k < numVertices; k++)
                    temp[k] = new int[numVertices];
                for (int i = 0; i < graph1.Vertices; i++)
                {
                    for (int j = 0; j < graph1.Vertices; j++)
                    {
                        if (i < graph2.Vertices && j < graph2.Vertices)
                        {
                            temp[i][j] = graph2.adjacencyMatrix[i][j];
                        }
                        else
                        {
                            temp[i][j] = 0;
                        }
                    }
                }

                matrixA = (int[][])graph1.adjacencyMatrix.Clone();
                matrixB = (int[][])temp.Clone();
            }

            return (matrixA, matrixB);
        }
    }
}
