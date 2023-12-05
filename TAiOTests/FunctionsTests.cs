using TAiO;

namespace TAiOTests
{
    public class FunctionsTests
    {
        Random _random = new Random();

        [Theory]
        [InlineData(5)]
        [InlineData(10)]
        public void DoesCullWorkProperly(int vertices)
        {
            var graph = new DirectedGraph(vertices);

            var edges = _random.Next(0, (vertices - 1) * vertices);

            for(int i=0; i<edges; i++)
            {
                int source = 0, destination = 0;
                while (source == destination || graph.adjacencyMatrix[source][destination] == 1)
                {
                    source = _random.Next(0, vertices);
                    destination = _random.Next(0, vertices);
                }
                graph.AddEdge(source,destination);
            }

            graph.Cull();

            int nrOfOffEdges = 0;

            for(int i=0; i<vertices; i++)
            {
                for(int j=0; j<vertices; j++)
                {
                    if(i!=0 && graph.adjacencyMatrix[i][j] != graph.adjacencyMatrix[j][i])
                    {
                        nrOfOffEdges++;
                    }
                }
            }

            Assert.Equal(0, nrOfOffEdges);
        }

        [Fact]
        public void IsCliqueRecognizedProperly()
        {
            var graph = new DirectedGraph(5);

            graph.AddEdge(0, 1);
            graph.AddEdge(0, 2);
            graph.AddEdge(0, 3);
            graph.AddEdge(0, 4);
            graph.AddEdge(1, 0);
            graph.AddEdge(1, 4);
            graph.AddEdge(2, 0);
            graph.AddEdge(2, 3);
            graph.AddEdge(3, 0);
            graph.AddEdge(3, 2);
            graph.AddEdge(4, 1);

            var clique1 = new int[] { 0, 1 };
            var clique2 = new int[] { 1, 4 };
            var clique3 = new int[] { 0, 2, 3 };

            graph.Cull();

            var IsClique1 = graph.IsClique(clique1);
            var IsClique2 = graph.IsClique(clique2);
            var IsClique3 = graph.IsClique(clique3);

            Assert.True(IsClique1);
            Assert.True(IsClique2);
            Assert.True(IsClique3);
        }

        [Fact]
        public void IsNONCliqueRecognizedProperly()
        {
            var graph = new DirectedGraph(5);

            graph.AddEdge(0, 1);
            graph.AddEdge(0, 4);
            graph.AddEdge(1, 0);
            graph.AddEdge(1, 4);
            graph.AddEdge(2, 0);
            graph.AddEdge(2, 3);
            graph.AddEdge(3, 0);
            graph.AddEdge(3, 2);

            graph.Cull();

            var clique1 = new int[] { 0, 2 };
            var clique2 = new int[] { 0, 1, 4 };
            var clique3 = new int[] { 0, 4 };

            var IsClique1 = graph.IsClique(clique1);
            var IsClique2 = graph.IsClique(clique2);
            var IsClique3 = graph.IsClique(clique3);

            Assert.False(IsClique1);
            Assert.False(IsClique2);
            Assert.False(IsClique3);
        }

        [Theory]
        [InlineData(10)]
        public void IsSizeCalculatedCorrectly(int vertices)
        {
            var graph = new DirectedGraph(vertices);

            var edges = _random.Next(0, (vertices - 1) * vertices);

            for (int i = 0; i < edges; i++)
            {
                int source = 0, destination = 0;
                while (source == destination || graph.adjacencyMatrix[source][destination] == 1)
                {
                    source = _random.Next(0, vertices);
                    destination = _random.Next(0, vertices);
                }
                graph.AddEdge(source, destination);
            }

            int size = Size.Calculate(graph);

            Assert.Equal(vertices+edges,size);
        }
    }
}