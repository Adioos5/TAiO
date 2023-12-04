using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAiO
{
    public class Graph
    {
        private int[,] adjacencyMatrix;
        public int[,] AdjacencyMatrix
        {
            get => adjacencyMatrix; set => adjacencyMatrix = value;
        }
        private int numVertices;
        public int NumVertices
        {
            get => numVertices; set => numVertices = value;
        }

        public Graph(int numVertices)
        {
            this.numVertices = numVertices;
            this.adjacencyMatrix = new int[numVertices, numVertices];
        }
        public Graph(Graph originalGraph)
        {
            this.numVertices = originalGraph.NumVertices;
            this.adjacencyMatrix = new int[numVertices, numVertices];

            // Copy the adjacency matrix
            for (int i = 0; i < numVertices; i++)
            {
                for (int j = 0; j < numVertices; j++)
                {
                    this.adjacencyMatrix[i, j] = originalGraph.AdjacencyMatrix[i, j];
                }
            }
        }

        public void AddEdge(int source, int destination)
        {
            if (source >= 0 && source < numVertices && destination >= 0 && destination < numVertices)
            {
                adjacencyMatrix[source, destination] = 1;
                adjacencyMatrix[destination, source] = 1;
            }
            else
            {
                //Console.WriteLine("a");
            }
        }
        public void PrintGraph()
        {
            for (int i = 0; i < numVertices; i++)
            {
                for (int j = 0; j < numVertices; j++)
                {
                    Console.Write(adjacencyMatrix[i, j] + " ");
                }
                Console.WriteLine();
            }
        }

        public int[] FindDegrees()
        {
            int[] result = new int[numVertices];
            for (int i = 0; i < numVertices; i++)
            {
                for (int j = i + 1; j < numVertices; j++)
                {
                    result[i] += adjacencyMatrix[i, j];
                    result[j] += adjacencyMatrix[i, j];
                }
            }

            return result;
        }
        public void deleteVer(int ver)
        {
            for (int i = 0; i < numVertices; i++)
            {
                adjacencyMatrix[i, ver] = 0;
                adjacencyMatrix[ver, i] = 0;
            }
        }
        public bool IsClique(int[] vertices)
        {
            foreach (var vertex in vertices)
            {
                if (vertex < 0 || vertex >= NumVertices)
                {
                    throw new ArgumentException("Invalid vertex index");
                }
            }

            // Check if all pairs of vertices in the subgraph are connected
            for (int i = 0; i < vertices.Length - 1; i++)
            {
                for (int j = i + 1; j < vertices.Length; j++)
                {
                    if (adjacencyMatrix[vertices[i], vertices[j]] == 0)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
        public void DeleteVertex(int vertexToDelete)
        {
            if (vertexToDelete < 0 || vertexToDelete >= numVertices)
            {
                throw new ArgumentException("Invalid vertex index");
            }

            Graph newGraph = new Graph(numVertices - 1);

            // Copy values from the old graph to the new graph, excluding the specified vertex
            int newRow = 0;
            for (int i = 0; i < numVertices; i++)
            {
                if (i == vertexToDelete)
                {
                    continue; // Skip the row and column corresponding to the deleted vertex
                }

                int newCol = 0;
                for (int j = 0; j < numVertices; j++)
                {
                    if (j == vertexToDelete)
                    {
                        continue; // Skip the column corresponding to the deleted vertex
                    }

                    newGraph.AdjacencyMatrix[newRow, newCol] = adjacencyMatrix[i, j];
                    newCol++;
                }

                newRow++;
            }

            // Update the current graph with the values of the new graph
            numVertices = newGraph.NumVertices;
            adjacencyMatrix = newGraph.AdjacencyMatrix;
        }
        public void DeleteVertices(int[] verticesToDelete)
        {
            if (verticesToDelete.Length != numVertices)
            {
                throw new ArgumentException("The length of verticesToDelete should be equal to the number of vertices.");
            }

            // Count the number of vertices to be deleted
            int countToDelete = verticesToDelete.Count(v => v == 1);

            // Create a new graph with fewer vertices
            Graph newGraph = new Graph(numVertices - countToDelete);

            // Copy values from the old graph to the new graph, excluding the specified vertices
            int newRowIndex = 0;
            for (int i = 0; i < numVertices; i++)
            {
                if (verticesToDelete[i] == 1)
                {
                    continue; // Skip the row corresponding to the deleted vertex
                }

                int newColIndex = 0;  // Reset newColIndex for each row in the new graph
                for (int j = 0; j < numVertices; j++)
                {
                    if (verticesToDelete[j] == 1)
                    {
                        continue; // Skip the column corresponding to the deleted vertex
                    }

                    newGraph.AdjacencyMatrix[newRowIndex, newColIndex] = adjacencyMatrix[i, j];
                    newColIndex++;
                }

                newRowIndex++;
            }

            // Update the current graph with the values of the new graph
            numVertices = newGraph.NumVertices;
            adjacencyMatrix = newGraph.AdjacencyMatrix;
        }
    }
}
