using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAiO
{
    public static class Size
    {
        public static int Calculate(DirectedGraph graph)
        {
            int result = graph.Vertices;
            for(int i = 0; i < graph.adjacencyMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < graph.adjacencyMatrix.GetLength(0); j++)
                {
                    result += graph.adjacencyMatrix[i][j];
                }
            }
            return result;
        }
    }
}
