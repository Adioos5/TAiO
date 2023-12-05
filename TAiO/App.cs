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
        static string GetPath()
        {
            string? path;

            while (true)
            {
                Console.WriteLine("Podaj sciezke do pliku z grafem:");
                Console.WriteLine("Biezacy katalog: " + Environment.CurrentDirectory);
                path = Console.ReadLine();
                if (path == null)
                    path = "";

                if (!path.StartsWith('C'))
                    path = Environment.CurrentDirectory + '\\' + path;

                if (File.Exists(path))
                {
                    return path;
                }
                else
                {
                    Console.WriteLine("Plik nie istnieje. Sprobuj jeszcze raz.");
                }
            }
        }
        static void GetGraph(out DirectedGraph graph)
        {
            int size;
            using (StreamReader sr = new StreamReader(GetPath()))
            {
                sr.ReadLine();
                size = int.Parse(sr.ReadLine() ?? "0");
                Console.WriteLine("Liczba wierzchołkow: " + size);

                graph = new DirectedGraph(size);
                for (int i = 0; i < size; i++)
                {
                    string? input = sr.ReadLine();
                    if(input == null)
                        input = "";
                    string[] row = input.Split(' ');
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
            Console.WriteLine("Wybierz opcje, ktora chcesz zrobic:");
            Console.WriteLine("1. Ustawic pierwszy graf (klika, podgraf, metryka).");
            Console.WriteLine("2. Ustawic drugi graf (podgraf, metryka).");
            Console.WriteLine("3. Znalezc metrykę miedzy dwoma grafami.");
            Console.WriteLine("4. Znalezc maksymalny wspolny podgraf dla dwoch grafow.");
            Console.WriteLine("5. Znalezc najwieksza klike grafu.");
            Console.WriteLine("6. Wyczyscic konsole.");

            return ReadUserOption(1, 6);

        }
        static int ReadUserOption(int min, int max)
        {
            int option;
            do
            {
                string? input = Console.ReadLine();

                if (int.TryParse(input, out option) && option >= min && option <= max)
                {
                    return option;
                }
                else
                {
                    Console.WriteLine($"Bład: Wprowadz liczbe od {min} do {max}.");
                }

            } while (true);
        }
        static private void GetOptionClique(DirectedGraph graph)
        {
            Console.WriteLine();
            Console.WriteLine("Wybierz opcje, ktora chcesz zrobic:");
            Console.WriteLine("1. Znajdz najwieksza klike.");
            Console.WriteLine("2. Znajdz aproksymacje najwiekszej kliki.");
            int[] clique;
            switch (ReadUserOption(1, 2))
            {
                case 1:
                    clique = CliqueSolver.BiggestClique(graph);
                    Console.WriteLine("Najwieksza klika: " + (clique.Length == 0 ? "Klika nieznaleziona." : "[" + string.Join(", ", clique) + "]"));
                    Console.WriteLine("Rozmiar kliki: " + clique.Length);
                    Console.WriteLine();
                    break;

                case 2:
                    clique = CliqueSolver.AproxClique(graph);
                    Console.WriteLine("Aproksymacja najwiekszej kliki: " + (clique.Length == 0 ? "Klika nieznaleziona." : "[" + string.Join(", ", clique) + "]"));
                    Console.WriteLine("Rozmiar kliki: " + clique.Length);
                    Console.WriteLine();
                    break;

                default:

                    break;
            }


        }
        static private void GetOptionSubgraph(DirectedGraph graph1, DirectedGraph graph2)
        {
            Console.WriteLine();
            Console.WriteLine("Wybierz opcje, ktora chcesz zrobic:");
            Console.WriteLine("1. Znajdz maksymalny wspolny podgraf.");
            Console.WriteLine("2. Znajdz aproksymacje maksymalnego wspolnego podgrafu.");
            switch (ReadUserOption(1, 2))
            {
                case 1:
                    MaximalCommonSubgraph.FindPrecisely(graph1, graph2);
                    Console.WriteLine();
                    break;

                case 2:
                    MaximalCommonSubgraph.Approximate(graph1, graph2);
                    Console.WriteLine();
                    break;

                default:
                    break;
            }


        }
        static private void GetOptionMetric(DirectedGraph graph1, DirectedGraph graph2)
        {
            Console.WriteLine();
            Console.WriteLine("Wybierz opcje, ktora chcesz zrobic:");
            Console.WriteLine("1. Znajdz wartosc metryki.");
            Console.WriteLine("2. Znajdz aproksymacje wartosci metryki.");
            switch (ReadUserOption(1, 2))
            {
                case 1:
                    Console.WriteLine($"Dokladna wartosc metryki to: {Metric.CalculatePrecisely(graph1, graph2)}");
                    Console.WriteLine();
                    break;

                case 2:
                    Console.WriteLine($"Przyblizona wartosc metryki to: {Metric.Approximate(graph1, graph2)}");
                    Console.WriteLine();
                    break;

                default:
                    break;
            }


        }

        static int GetDecisionForGraphs()
        {
            Console.WriteLine("Wybierz opcje, ktora chcesz zrobic:");
            Console.WriteLine("1. Wczytac domyslne dwa grafy.");
            Console.WriteLine("2. Ustawic własne dwa grafy.");
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
                        catch (Exception ex) { Console.WriteLine("Error:" + ex.Message); }
                        break;
                    case 2:
                        try
                        {
                            GetGraph(out g2);
                        }
                        catch (Exception ex) { Console.WriteLine("Error:" + ex.Message); }
                        break;
                    case 3:

                        try
                        {
                            GetOptionMetric(g1, g2);
                        }
                        catch (Exception ex) { Console.WriteLine("Error:" + ex.Message); }
                        break;

                    case 4:

                        try
                        {
                            GetOptionSubgraph(g1, g2);
                        }
                        catch (Exception ex) { Console.WriteLine("Error:" + ex.Message); }
                        break;

                    case 5:
                        try
                        {
                            GetOptionClique(g1);
                        }
                        catch (Exception ex) { Console.WriteLine("Error:" + ex.Message); }
                        break;
                    case 6:
                        Console.Clear();
                        break;

                    default:
                        break;
                }
            }
        }
    }
}
