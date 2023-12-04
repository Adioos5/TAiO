using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAiO
{
    public class DirectedGraph
    {
        public int Vertices { get; }
        public int[][] adjacencyMatrix;

        public DirectedGraph(int vertices)
        {
            Vertices = vertices;
            adjacencyMatrix = new int[vertices][];
            for (int i = 0; i < vertices; i++)
            {
                adjacencyMatrix[i] = new int[vertices];
            }
        }

        public void AddEdge(int from, int to)
        {
            adjacencyMatrix[from][to] = 1;
        }

        public void PrintAdjacencyMatrix()
        {
            for (int i = 0; i < Vertices; i++)
            {
                for (int j = 0; j < Vertices; j++)
                {
                    Console.Write(adjacencyMatrix[i][j] == 1 ? "1 " : "0 ");
                }
                Console.WriteLine();
            }
        }
    }
}
