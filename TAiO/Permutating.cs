using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAiO
{
    public static class Permutating
    {
        
        static IEnumerable<IEnumerable<T>>GetPermutations<T>(IEnumerable<T> list, int length)
        {
            if (length == 1) return list.Select(t => new T[] { t });

            return GetPermutations(list, length - 1)
                .SelectMany(t => list.Where(e => !t.Contains(e)),
                    (t1, t2) => t1.Concat(new T[] { t2 }));
        }

        public static IEnumerable<IEnumerable<double>> GeneratePermutations(int n)
        {
            double[] vertices = new double[n];
            for(int i = 0; i < n; i++)
            {
                vertices[i] = i;
            }

            return GetPermutations(vertices, n);
        }

        public static int[][] PermuteGraph(int[][] originalGraphAdjacencyMatrix, IEnumerable<double> permutation)
        {
            DirectedGraph permutedGraph = new DirectedGraph(originalGraphAdjacencyMatrix.GetLength(0));

            for (int i = 0; i < originalGraphAdjacencyMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < originalGraphAdjacencyMatrix.GetLength(0); j++)
                {
                    if (originalGraphAdjacencyMatrix[i][j] == 1)
                    {
                        permutedGraph.AddEdge((int)permutation.ElementAt(i), (int)permutation.ElementAt(j));
                    }
                }
            }

            return permutedGraph.adjacencyMatrix;
        }
    }
}
