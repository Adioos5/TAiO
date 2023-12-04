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

                if (File.Exists(Environment.CurrentDirectory + '\\'+ path))
                {
                    return Environment.CurrentDirectory + '\\' + path;
                }
                else
                {
                    Console.WriteLine("Plik nie istnieje. Spróbuj jeszcze raz.");
                }
            }
        }
        static void GetGraph(StreamReader sr, out DirectedGraph graph)
        {
            int size;
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

            Console.WriteLine("Macierz sąsiedztwa:");
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Console.Write(graph.adjacencyMatrix[i][j] + " ");
                }
                Console.WriteLine();
            }
        }
        static DirectedGraph GetGraph(string path)
        {
            int nrOfGraphs;
            DirectedGraph directedGraph = new DirectedGraph(0); // shhh nikt tej linijki nie widzi
            //List<int[,]> graphs = new List<int[,]>();
            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    //nrOfGraphs = int.Parse(sr.ReadLine());
                    if (int.Parse(sr.ReadLine()) != 1) throw new Exception("inna ilość grafów niż 1");
                    //Console.WriteLine("Liczba grafów: " + nrOfGraphs);
                    /*
                    for (int i = 0; i < nrOfGraphs; i++)
                    {
                        var graph = GetGraph(sr);
                        if (graph != null)
                            graphs.Add(graph);

                        //To trzeba zmienić
                        Console.WriteLine(sr.ReadLine());
                        sr.ReadLine();
                    }
                    */
                    int verticesNum = int.Parse(sr.ReadLine());
                    directedGraph = new DirectedGraph(verticesNum);
                    for (int i = 0; i < verticesNum; i++)
                    {
                        string s = sr.ReadLine();
                        var items = s.Split(' ');
                        for (int j = 0; j < verticesNum; j++)
                        {
                            directedGraph.adjacencyMatrix[i][j] = int.Parse(items[j]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Błąd podczas odczytu pliku: " + ex.Message);
            }

            return directedGraph;
        }
        static int GetOption()
        {
            int option;
            Console.WriteLine("Wybierz opcję, którą chcesz zrobić:");
            Console.WriteLine("1. Wyczyścić konsolę");
            Console.WriteLine("2. Znaleźć metrykę między dwoma grafami");
            Console.WriteLine("3. Znaleźć maksymalny wspólny podgraf dla dwóch grafów");
            Console.WriteLine("4. Znaleźć największą klikę grafu");
            Console.WriteLine("5. Ustaw pierwszy graf (klika i podgraf)");
            Console.WriteLine("6. Ustaw drugi graf (podgraf)");
            Console.WriteLine("7. Testy");

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
        static void Main()
        {
            DirectedGraph g1 = new DirectedGraph(0);
            DirectedGraph g2 = new DirectedGraph(0);
            while (true)
            {
                switch (GetOption())
                {
                    case 1:

                        try
                        {
                            GetOptionMetric(g1, g2);
                        }
                        catch (Exception ex) { Console.WriteLine("Błąd:" + ex.Message); }
                        break;

                    case 2:

                        try
                        {
                            GetOptionSubgraph(g1, g2);
                        }
                        catch (Exception ex) { Console.WriteLine("Błąd:" + ex.Message); }
                        break;

                    case 3:
                        try
                        {
                            GetOptionClique(g1);
                        }
                        catch (Exception ex) { Console.WriteLine("Błąd:" + ex.Message); }
                        break;

                    case 4:
                        using (StreamReader sr = new StreamReader(GetPath()))
                            try
                            {
                                GetGraph(sr, out g1);
                            }
                            catch (Exception ex) { Console.WriteLine("Błąd:" + ex.Message); }
                        break;

                    case 5:
                        using (StreamReader sr = new StreamReader(GetPath()))
                            try
                            {
                                GetGraph(sr, out g2);
                            }
                            catch (Exception ex) { Console.WriteLine("Błąd:" + ex.Message); }
                        break;
                    case 6:
                        //run tests
                        break;
                    case 0:
                        Console.Clear();
                        break;

                    default:
                        break;
                }
            }
        }
    }
}
