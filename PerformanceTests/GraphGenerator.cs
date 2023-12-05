using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAiO;

namespace PerformanceTests
{
    public class GraphGenerator
    {
        public int vertices, edges;
        public Random random = new();
        public GraphGenerator(int vert, int edg)
        {
            vertices = vert;
            edges = edg;
        }
        public DirectedGraph Generate()
        {
            var Graph = new DirectedGraph(vertices);

            for (int e = 0; e < edges; e++)
            {
                int vert1 = 0, vert2 = 0;
                while (vert1 == vert2 || Graph.adjacencyMatrix[vert1][vert2] == 1)
                {
                    vert1 = random.Next(vertices);
                    vert2 = random.Next(vertices);
                }

                Graph.AddEdge(vert1, vert2);
            }

            return Graph;
        }
    }

    public class GraphGeneratorConstVert : GraphGenerator
    {
        public GraphGeneratorConstVert(int vert, int edg) : base(vert, edg)
        {
        }

        public DirectedGraph GenerateNext()
        {
            edges++;
            return Generate();
        }
    }

    public class GraphGeneratorConstEdg : GraphGenerator
    {
        public GraphGeneratorConstEdg(int vert, int edg) : base(vert, edg)
        {
        }

        public DirectedGraph GenerateNext()
        {
            vertices++;
            return Generate();
        }
    }

    public class GraphGeneratorConstDensity : GraphGenerator
    {
        double density { get; set; }
        public GraphGeneratorConstDensity(int vert, int edg, double rate) : base(vert, edg)
        {
            density = rate;
            var maxEdge = vert * (vert - 1);
            edges = (int)(vert * density);
        }
        public DirectedGraph GenerateNext()
        {
            vertices++;
            edges = (int)(vertices * (vertices - 1) * density);
            return Generate();
        }
    }
}
