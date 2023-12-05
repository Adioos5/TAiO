using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAiO;

namespace TAiOTests
{
    public class ExactAlgorithmsTests
    {
        [Fact]
        public void IsMaximumCliqueCorrect()
        {
            var graph = new DirectedGraph(10);

            for(int i=0; i<10; i++)
            {
                for(int j=0; j<10; j++)
                {
                    if (i != j) graph.AddEdge(i, j);
                }
            }

            graph.adjacencyMatrix[8][6] = 0;
            graph.adjacencyMatrix[0][8] = 0;
            graph.adjacencyMatrix[0][7] = 0;
            graph.adjacencyMatrix[0][5] = 0;

            var expectedMaximumClique = new int[] { 1, 2, 3, 4, 5, 6, 7, 9 };

            var maximumClique = CliqueSolver.BiggestClique(graph);

            Assert.Equal(8,maximumClique.Length);
            Assert.Equal(expectedMaximumClique, maximumClique);
        }

        [Fact]
        public void IsMaximumCliqueCorrectNoEdges()
        {
            var graph = new DirectedGraph(10);

            var maximumClique = CliqueSolver.BiggestClique(graph);

            Assert.Single(maximumClique);
        }

        [Fact]
        public void IsMaximumCliqueCorrectK2()
        {
            var graph = new DirectedGraph(5);

            graph.AddEdge(0, 1);
            graph.AddEdge(0, 2);
            graph.AddEdge(0, 3);
            graph.AddEdge(0, 4);
            graph.AddEdge(1, 2);
            graph.AddEdge(1, 3);
            graph.AddEdge(1, 4);
            graph.AddEdge(2, 3);
            graph.AddEdge(2, 4);
            graph.AddEdge(4, 0);

            var expectedClique = new int[] {0, 4};

            var MaximumClique = CliqueSolver.BiggestClique(graph);

            Assert.Equal(2, MaximumClique.Length);
            Assert.Equal(expectedClique, MaximumClique);
        }

        [Fact]
        public void IsMetricCorrectForIsomorphicGraphs()
        {
            var graph1 = new DirectedGraph(10);
            var graph2 = new DirectedGraph(10);

            graph1.AddEdge(0, 1);
            graph1.AddEdge(0, 3);
            graph1.AddEdge(1, 2);
            graph1.AddEdge(2, 4);
            graph1.AddEdge(4, 1);
            graph1.AddEdge(4, 3);

            graph2.AddEdge(4, 5);
            graph2.AddEdge(4, 7);
            graph2.AddEdge(7, 6);
            graph2.AddEdge(6, 0);
            graph2.AddEdge(0, 5);
            graph2.AddEdge(0, 7);

            var metricResult = Metric.CalculatePrecisely(graph1, graph2);

            Assert.Equal(0, metricResult);
        }

        [Fact]
        public void IsMetricCorrectEqualVert()
        {
            var graph1 = new DirectedGraph(5);
            var graph2 = new DirectedGraph(5);

            graph1.AddEdge(0, 1);
            graph1.AddEdge(0, 3);
            graph1.AddEdge(1, 2);
            graph1.AddEdge(2, 4);
            graph1.AddEdge(4, 1);
            graph1.AddEdge(4, 3);

            /*var metricResult = Metric.CalculatePrecisely(graph1, graph2);

            Assert.Equal(Math.Sqrt(6), metricResult, 0.01);*/

            graph1.AddEdge(1, 0);
            graph1.AddEdge(3, 4);

            var metricResult = Metric.CalculatePrecisely(graph1, graph2);

            Assert.Equal(Math.Sqrt(8), metricResult, 0.01);
        }

        [Fact]
        public void IsMetricCorrectDiffVert()
        {
            var graph1 = new DirectedGraph(4);
            var graph2 = new DirectedGraph(7);

            graph1.AddEdge(0, 1);
            graph1.AddEdge(0, 2);
            graph1.AddEdge(2, 3);

            graph2.AddEdge(1, 3);
            graph2.AddEdge(1, 6);
            graph2.AddEdge(6, 1);
            graph2.AddEdge(6, 2);
            graph2.AddEdge(6, 5);

            var metricResult = Metric.CalculatePrecisely(graph1, graph2);

            Assert.Equal(Math.Sqrt(2)+3, metricResult, 0.01);
        }

        [Fact]
        public void MaximumSubgraphIsomporphicGraphs()
        {
            var graph1 = new DirectedGraph(5);
            var graph2 = new DirectedGraph(5);

            graph1.AddEdge(0, 1);
            graph1.AddEdge(0, 3);
            graph1.AddEdge(1, 2);

            graph2.AddEdge(0, 1);
            graph2.AddEdge(0, 3);
            graph2.AddEdge(1, 2);

            var subgraph = MaximalCommonSubgraph.FindPrecisely(graph1, graph2);

            var exactSubgraph = new int[][] { new int[] { 0, 1, 0, 1, 0 }, new int[] { 0, 0, 1, 0, 0}, new int[5], new int[5], new int[5] };
            
            Assert.Equal(exactSubgraph, subgraph);
        }

        [Fact]
        public void MaximumSubgraph()
        {
            var graph1 = new DirectedGraph(5);
            var graph2 = new DirectedGraph(3);

            graph1.AddEdge(0, 1);
            graph1.AddEdge(0, 2);
            graph1.AddEdge(0, 3);
            graph1.AddEdge(0, 4);

            graph2.AddEdge(0, 1);
            graph2.AddEdge(0, 2);
            graph2.AddEdge(1, 0);
            graph2.AddEdge(2, 0);

            var subgraph = MaximalCommonSubgraph.FindPrecisely(graph1, graph2);

            var exactSubgraph = new int[][] { new int[] { 0, 1, 1}, new int[3], new int[3] };

            Assert.Equal(exactSubgraph,subgraph);
        }
    }
}
