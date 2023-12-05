using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAiO
{
    public class DirectedGraph
    {
        public int Vertices { get; set; }
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
        public DirectedGraph(DirectedGraph originalGraph)
        {
            this.Vertices = originalGraph.Vertices;
            this.adjacencyMatrix = new int[Vertices][];

            // Copy the adjacency matrix
            for (int i = 0; i < Vertices; i++)
            {
                this.adjacencyMatrix[i] = new int[Vertices];
                for (int j = 0; j < Vertices; j++)
                {
                    this.adjacencyMatrix[i][j] = originalGraph.adjacencyMatrix[i][j];
                }
            }
        }
        public DirectedGraph(int[,] matrix)
        {
            this.Vertices = matrix.GetLength(0);
            this.adjacencyMatrix = new int[Vertices][];

            // Copy the adjacency matrix
            for (int i = 0; i < Vertices; i++)
            {
                this.adjacencyMatrix[i] = new int[Vertices];
                for (int j = 0; j < Vertices; j++)
                {
                    this.adjacencyMatrix[i][j] = matrix[i,j];
                }
            }
        }

        public void Cull()
        {
            for (int i = 1; i < Vertices; ++i)
            {
                for (int j = 0; j < i; ++j)
                {
                    adjacencyMatrix[i][j] = adjacencyMatrix[j][i] = (adjacencyMatrix[i][j] + adjacencyMatrix[j][i]) / 2;
                }
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

        public int[] FindDegrees()
        {
            int[] result = new int[Vertices];
            for (int i = 0; i < Vertices; i++)
            {
                for (int j = i + 1; j < Vertices; j++)
                {
                    result[i] += adjacencyMatrix[i][j];
                    result[j] += adjacencyMatrix[i][j];
                }
            }

            return result;
        }
        public void deleteVer(int ver)
        {
            for (int i = 0; i < Vertices; i++)
            {
                adjacencyMatrix[i][ver] = 0;
                adjacencyMatrix[ver][i] = 0;
            }
        }
        public bool IsClique(int[] vertices)
        {
            foreach (var vertex in vertices)
            {
                if (vertex < 0 || vertex >= Vertices)
                {
                    throw new ArgumentException("Invalid vertex index");
                }
            }

            // Check if all pairs of vertices in the subgraph are connected
            for (int i = 0; i < vertices.Length - 1; i++)
            {
                for (int j = i + 1; j < vertices.Length; j++)
                {
                    if (adjacencyMatrix[vertices[i]][vertices[j]] == 0)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
        public void DeleteVertex(int vertexToDelete)
        {
            if (vertexToDelete < 0 || vertexToDelete >= Vertices)
            {
                throw new ArgumentException("Invalid vertex index");
            }

            DirectedGraph newGraph = new DirectedGraph(Vertices - 1);

            // Copy values from the old graph to the new graph, excluding the specified vertex
            int newRow = 0;
            for (int i = 0; i < Vertices; i++)
            {
                if (i == vertexToDelete)
                {
                    continue; // Skip the row and column corresponding to the deleted vertex
                }

                int newCol = 0;
                for (int j = 0; j < Vertices; j++)
                {
                    if (j == vertexToDelete)
                    {
                        continue; // Skip the column corresponding to the deleted vertex
                    }

                    newGraph.adjacencyMatrix[newRow][newCol] = adjacencyMatrix[i][j];
                    newCol++;
                }

                newRow++;
            }

            // Update the current graph with the values of the new graph
            Vertices = newGraph.Vertices;
            adjacencyMatrix = newGraph.adjacencyMatrix;
        }
        public void DeleteVertices(int[] verticesToDelete)
        {
            if (verticesToDelete.Length != Vertices)
            {
                throw new ArgumentException("The length of verticesToDelete should be equal to the number of vertices.");
            }

            // Count the number of vertices to be deleted
            int countToDelete = verticesToDelete.Count(v => v == 1);

            // Create a new graph with fewer vertices
            DirectedGraph newGraph = new DirectedGraph(Vertices - countToDelete);

            // Copy values from the old graph to the new graph, excluding the specified vertices
            int newRowIndex = 0;
            for (int i = 0; i < Vertices; i++)
            {
                if (verticesToDelete[i] == 1)
                {
                    continue; // Skip the row corresponding to the deleted vertex
                }

                int newColIndex = 0;  // Reset newColIndex for each row in the new graph
                for (int j = 0; j < Vertices; j++)
                {
                    if (verticesToDelete[j] == 1)
                    {
                        continue; // Skip the column corresponding to the deleted vertex
                    }

                    newGraph.adjacencyMatrix[newRowIndex][newColIndex] = adjacencyMatrix[i][j];
                    newColIndex++;
                }

                newRowIndex++;
            }

            // Update the current graph with the values of the new graph
            Vertices = newGraph.Vertices;
            adjacencyMatrix = newGraph.adjacencyMatrix;
        }
    }
}
