using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAiO
{
    public static class CliqueSolver
    {
        public static int[] AproxClique(DirectedGraph G)
        {
            DirectedGraph graph = new DirectedGraph(G);
            graph.Cull();
            int[] result = new int[graph.Vertices];
            int[] degrees;
            while (true)
            {
                degrees = graph.FindDegrees();


                var list = degrees
                     .Select((value, index) => new { Value = value, Index = index })
                     .Where(a => result[a.Index] == 0 && a.Value != 0);

                int indexMax = !list.Any() ? -1 :
                list.Aggregate((a, b) => (a.Value > b.Value) ? a : b)
                    .Index;


                if (indexMax >= 0)
                {
                    result[indexMax] = 1;
                    for (int i = 0; i < graph.Vertices; i++)
                    {
                        if (graph.adjacencyMatrix[i][indexMax] == 0 && i != indexMax)
                        {
                            graph.deleteVer(i);
                        }
                    }
                }
                else
                {
                    break;
                }
            }

            return result.Select((value, index) => new { value, index })
                    .Where(pair => pair.value == 1)
                    .Select(pair => pair.index)
                    .ToArray();
        }

        public static int[] BiggestClique(DirectedGraph G)
        {
            DirectedGraph graph = new DirectedGraph(G);
            graph.Cull();
            int[]? result;
            if (graph == null) return new int[1];
            if (graph.Vertices == 1) return new int[] { 1 };

            //can be better starting point
            for (int i = graph.Vertices; i > 0; i--)
            {
                if ((result = LookForClique(graph, i)) != null)
                {
                    return result;
                }
            }
            return new int[] { 1 };

        }
        public static int[]? LookForClique(DirectedGraph graph, int size)
        {
            int[] vertices = graph.FindDegrees();
            vertices = vertices.Select((value, index) => new { value, index })
                .Where(pair => pair.value >= size - 1)
                .Select(pair => pair.index)
                .ToArray();

            if (vertices.Length < size) return null;
            foreach (var combination in GetPermutations(vertices, size))
            {
                if (graph.IsClique(combination.ToArray())) return combination.ToArray();
            }
            return null;
        }


        public static IEnumerable<IEnumerable<T>>
        GetPermutations<T>(IEnumerable<T> list, int length)
        {
            if (length == 1) return list.Select(t => new T[] { t });
            return GetPermutations(list, length - 1)
                .SelectMany(t => list.Where(o => !t.Contains(o)),
                    (t1, t2) => t1.Concat(new T[] { t2 }));
        }

        public static int[] DeleteElements(int[] array, int[] indexesToDelete)
        {
            if (array.Length != indexesToDelete.Length)
            {
                throw new ArgumentException("Array lengths do not match");
            }

            // Use LINQ to create a new array excluding elements at specified indexes
            int[] newArray = array.Where((value, index) => indexesToDelete[index] == 0).ToArray();

            return newArray;
        }
    }
}
