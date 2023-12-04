using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAiO
{
    public static class Examples
    {


        public static void LaunchMetric()
        {
            int numVertices = 5;
            DirectedGraph graph1 = new DirectedGraph(numVertices);
            DirectedGraph graph2 = new DirectedGraph(numVertices);

            graph1.AddEdge(0, 1);
            graph1.AddEdge(0, 2);
            graph1.AddEdge(0, 3);
            graph1.AddEdge(0, 4);

            graph2.AddEdge(2, 0);
            graph2.AddEdge(2, 1);
            graph2.AddEdge(2, 3);
            graph2.AddEdge(2, 4);

            Console.WriteLine("[ METRYKA ] ");
            Console.WriteLine("Wynik dokładny: " + Metric.CalculatePrecisely(graph1, graph2));
            Console.WriteLine("Wynik aproksymacji: " + Metric.Approximate(graph1, graph2));
            Console.WriteLine();
        }

        public static void LaunchSize()
        {
            int numVertices = 5;
            DirectedGraph graph = new DirectedGraph(numVertices);

            graph.AddEdge(0, 1);
            graph.AddEdge(0, 2);
            graph.AddEdge(0, 3);
            graph.AddEdge(0, 4);

            Console.WriteLine("[ ROZMIAR GRAFU ] ");
            Console.WriteLine("Wynik: " + Size.Calculate(graph));
            Console.WriteLine();
        }

        public static void LaunchMaximalCommonSubgraph()
        {
            int numVertices = 5;
            DirectedGraph graph1 = new DirectedGraph(numVertices);
            DirectedGraph graph2 = new DirectedGraph(numVertices);

            graph1.AddEdge(0, 1);
            graph1.AddEdge(0, 2);
            graph1.AddEdge(0, 3);
            graph1.AddEdge(0, 4);

            graph2.AddEdge(2, 0);
            graph2.AddEdge(2, 1);
            graph2.AddEdge(2, 3);
            graph2.AddEdge(2, 4);

            Console.WriteLine("[ MAKSYMALNY WSPÓLNY PODGRAF ] ");
            Console.WriteLine("Wynik dokładny: ");
            MaximalCommonSubgraph.FindPrecisely(graph1, graph2);
            Console.WriteLine("Wynik aproksymacji: ");
            MaximalCommonSubgraph.Approximate(graph1, graph2);
            Console.WriteLine();
        }

        public static void LaunchCliqueSolver()
        {
            Console.WriteLine("[ MAKSYMALNA KLIKA ] ");

            int numVertices = 10;
            Graph graph = new Graph(numVertices);

            graph.AddEdge(0, 1);
            graph.AddEdge(0, 4);
            graph.AddEdge(1, 2);
            graph.AddEdge(1, 3);
            graph.AddEdge(1, 4);
            graph.AddEdge(2, 3);
            graph.AddEdge(3, 4);
            for (int i = 0; i < numVertices; i++)
            {
                var ran = new Random();
                for (int j = 0; j < numVertices * 0.9; j++)
                {
                    int destination = ran.Next(numVertices);
                    if (i != destination)
                        graph.AddEdge(i, destination);
                }

            }

            Console.WriteLine("Adjacency Matrix of the graph:");
            graph.PrintGraph();

            var bClique = CliqueSolver.BiggestClique(graph);
            Console.WriteLine("Największa klika:");
            Console.WriteLine("Biggest Clique: " + (bClique.Length == 0 ? "No clique found." : "[" + string.Join(", ", bClique) + "]"));
            Console.WriteLine("Rozmiar kliki: " + bClique.Length);

            var aClique = CliqueSolver.AproxClique(graph);
            Console.WriteLine("Aproksymacja kliki:");
            Console.WriteLine("Approximate Clique: " + (aClique.Length == 0 ? "No clique found." : "[" + string.Join(", ", aClique) + "]"));
            Console.WriteLine("Rozmiar kliki: " + aClique.Length);
            Console.WriteLine();
        }
    }
}
