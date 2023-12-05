using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection.Metadata;

namespace TAiO
{

    class Program
    {
        private DirectedGraph graph1 = new DirectedGraph(0);
        public DirectedGraph Graph1
        {
            get
            {
                return graph1;
            }
            set
            {
                graph1 = value;
            }
        }
        private DirectedGraph graph2 = new DirectedGraph(0);
        public DirectedGraph Graph2
        {
            get
            {
                if (graph2 == null)
                {
                    graph2 = new DirectedGraph(0);
                }
                return graph2;
            }
            set
            {
                graph2 = value;
            }
        }


        static string GetPath()
        {
            string? path;

            while (true)
            {
                Console.WriteLine("Podaj ścieżkę do pliku z grafem:");
                Console.WriteLine("Bieżący katalog: " + Environment.CurrentDirectory);
                path = Console.ReadLine();

                if (!path.StartsWith('C'))
                    path = Environment.CurrentDirectory + '\\' + path;

                if (File.Exists(path))
                {
                    return path;
                }
                else
                {
                    Console.WriteLine("Plik nie istnieje. Spróbuj jeszcze raz.");
                }
            }
        }
        static void GetGraph(out DirectedGraph graph)
        {
            int size;
            using (StreamReader sr = new StreamReader(GetPath()))
            {
                sr.ReadLine();
                size = int.Parse(sr.ReadLine());
                Console.WriteLine("Liczba wierzchołków: " + size);

                graph = new DirectedGraph(size);
                for (int i = 0; i < size; i++)
                {
                    string[] row = sr.ReadLine().Split(' ');
                    for (int j = 0; j < size; j++)
                    {
                        graph.adjacencyMatrix[i][j] = int.Parse(row[j]);
                    }
                }
            }

            graph.PrintAdjacencyMatrix();
        }
        static int GetOption()
        {
            int option;
            Console.WriteLine("Wybierz opcję, którą chcesz zrobić:");
            Console.WriteLine("1. Ustawić pierwszy graf (klika, podgraf, metryka)");
            Console.WriteLine("2. Ustawić drugi graf (podgraf, metryka)");
            Console.WriteLine("3. Znaleźć metrykę między dwoma grafami");
            Console.WriteLine("4. Znaleźć maksymalny wspólny podgraf dla dwóch grafów");
            Console.WriteLine("5. Znaleźć największą klikę grafu");
            Console.WriteLine("6. Wykonać testy");
            Console.WriteLine("7. Wyczyścić konsolę");

            return ReadUserOption(1, 7);

        }
        static int ReadUserOption(int min, int max)
        {
            int option;
            do
            {
                string input = Console.ReadLine();

                if (int.TryParse(input, out option) && option >= min && option <= max)
                {
                    return option;
                }
                else
                {
                    Console.WriteLine($"Błąd: Wprowadź liczbę od {min} do {max}.");
                }

            } while (true);
        }
        static private void GetOptionClique(DirectedGraph graph)
        {
            Console.WriteLine();
            Console.WriteLine("Wybierz opcję, którą chcesz zrobić:");
            Console.WriteLine("1. Znajdź największą klikę");
            Console.WriteLine("2. Znajdź aproksymację największej kliki");
            int[] clique;
            switch (ReadUserOption(1, 2))
            {
                case 1:
                    clique = CliqueSolver.BiggestClique(graph);
                    Console.WriteLine("Największa klika: " + (clique.Length == 0 ? "No clique found." : "[" + string.Join(", ", clique) + "]"));
                    Console.WriteLine("Rozmiar kliki: " + clique.Length);
                    break;

                case 2:
                    clique = CliqueSolver.AproxClique(graph);
                    Console.WriteLine("Aproksymacja największej kliki: " + (clique.Length == 0 ? "No clique found." : "[" + string.Join(", ", clique) + "]"));
                    Console.WriteLine("Rozmiar kliki: " + clique.Length);
                    break;

                default:

                    break;
            }


        }
        static private void GetOptionSubgraph(DirectedGraph graph1, DirectedGraph graph2)
        {
            Console.WriteLine();
            Console.WriteLine("Wybierz opcję, którą chcesz zrobić:");
            Console.WriteLine("1. Znajdź największy wspólny podgraf");
            Console.WriteLine("2. Znajdź aproksymację największego wspólnego podgrafu");
            int[] clique;
            switch (ReadUserOption(1, 2))
            {
                case 1:
                    MaximalCommonSubgraph.FindPrecisely(graph1, graph2);
                    break;

                case 2:
                    MaximalCommonSubgraph.Approximate(graph1, graph2);
                    break;

                default:
                    break;
            }


        }
        static private void GetOptionMetric(DirectedGraph graph1, DirectedGraph graph2)
        {
            Console.WriteLine();
            Console.WriteLine("Wybierz opcję, którą chcesz zrobić:");
            Console.WriteLine("1. Znajdź wartość metryki");
            Console.WriteLine("2. Znajdź aproksymację wartości metryki");
            switch (ReadUserOption(1, 2))
            {
                case 1:
                    Console.WriteLine($"Dokładna wartość metryki to: {Metric.CalculatePrecisely(graph1, graph2)}");
                    break;

                case 2:
                    Console.WriteLine($"Przybliżona wartość metryki to: {Metric.Approximate(graph1, graph2)}");
                    break;

                default:
                    break;
            }


        }

        static int GetDecisionForGraphs()
        {
            Console.WriteLine("Wybierz opcję, którą chcesz zrobić:");
            Console.WriteLine("1. Wczytać domyślne dwa grafy.");
            Console.WriteLine("2. Ustawić własne dwa grafy.");
            return ReadUserOption(1, 2);
        }
        static void Main()
        {
            var default1 = new int[,]
            {
                { 0, 1, 1, 1, 1, 1},
                { 1, 0, 1, 1, 0, 1},
                { 1, 1, 0, 1, 1, 1},
                { 1, 1, 1, 0, 1, 1},
                { 1, 1, 1, 1, 0, 0},
                { 1, 1, 1, 1, 1, 0},
            };

            var default2 = new int[,]
            {
                { 0, 0, 1, 0, 0},
                { 0, 0, 1, 1, 1},
                { 0, 0, 0, 1, 1},
                { 0, 1, 0, 0, 1},
                { 1, 1, 0, 1, 0},
            };
            DirectedGraph g1 = new DirectedGraph(default1);
            DirectedGraph g2 = new DirectedGraph(default2);

            switch (GetDecisionForGraphs())
            {
                case 1:
                    g1.PrintAdjacencyMatrix();
                    g2.PrintAdjacencyMatrix();
                    break;

                case 2:
                    GetGraph(out g1);
                    GetGraph(out g2);
                    break;

                default:
                    break;
            }

            while (true)
            {
                switch (GetOption())
                {
                    case 1:
                        try
                        {
                            GetGraph(out g1);
                        }
                        catch (Exception ex) { Console.WriteLine("Błąd:" + ex.Message); }
                        break;
                    case 2:
                        try
                        {
                            GetGraph(out g2);
                        }
                        catch (Exception ex) { Console.WriteLine("Błąd:" + ex.Message); }
                        break;
                    case 3:

                        try
                        {
                            GetOptionMetric(g1, g2);
                        }
                        catch (Exception ex) { Console.WriteLine("Błąd:" + ex.Message); }
                        break;

                    case 4:

                        try
                        {
                            GetOptionSubgraph(g1, g2);
                        }
                        catch (Exception ex) { Console.WriteLine("Błąd:" + ex.Message); }
                        break;

                    case 5:
                        try
                        {
                            GetOptionClique(g1);
                        }
                        catch (Exception ex) { Console.WriteLine("Błąd:" + ex.Message); }
                        break;
                    case 6:
                        //run tests
                        break;
                    case 7:
                        Console.Clear();
                        break;

                    default:
                        break;
                }
            }
        }
    }
}
