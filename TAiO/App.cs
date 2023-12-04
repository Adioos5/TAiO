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
                Console.WriteLine("Podaj ścieżkę do pliku z grafem:");
                path = Console.ReadLine();

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

        static int[,] GetGraph(StreamReader sr)
        {
            int size;
            int[,] graph = null;

            size = int.Parse(sr.ReadLine());
            Console.WriteLine("Liczba wierzchołków: " + size);

            graph = new int[size, size];
            for (int i = 0; i < size; i++)
            {
                string[] row = sr.ReadLine().Split(' ');
                for (int j = 0; j < size; j++)
                {
                    graph[i, j] = int.Parse(row[j]);
                }
            }

            Console.WriteLine("Macierz sąsiedztwa:");
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Console.Write(graph[i, j] + " ");
                }
                Console.WriteLine();
            }

            return graph;
        }

        static List<int[,]> GetGraphs(string path)
        {
            int nrOfGraphs;
            List<int[,]> graphs = new List<int[,]>();

            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    nrOfGraphs = int.Parse(sr.ReadLine());
                    Console.WriteLine("Liczba grafów: " + nrOfGraphs);

                    for (int i = 0; i < nrOfGraphs; i++)
                    {
                        var graph = GetGraph(sr);
                        if (graph != null)
                            graphs.Add(graph);

                        //To trzeba zmienić
                        Console.WriteLine(sr.ReadLine());
                        sr.ReadLine();
                    }

 
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Błąd podczas odczytu pliku: " + ex.Message);
            }

            return graphs;
        }
        static int GetOption()
        {
            int option;
            Console.WriteLine("Wybierz opcję, którą chcesz zrobić:");
            Console.WriteLine("0. Wyczyścić konsolę");
            Console.WriteLine("1. Znaleźć metrykę między dwoma grafami");
            Console.WriteLine("2. Znaleźć maksymalny wspólny podgraf dla dwóch grafów");
            Console.WriteLine("3. Znaleźć największą klikę grafu");

            do
            {
                string input = Console.ReadLine();

                if (int.TryParse(input, out option) && option >= 0 && option <= 3)
                {
                    return option;
                }
                else
                {
                    Console.WriteLine("Błąd: Wprowadź liczbę od 0 do 6.");
                }

            } while (true);

        }

        static void Main1()
        {
            List<int[,]> graphs1, graphs2;

            while (true)
            {
                switch (GetOption())
                {
                    case 1:
                        

                        break;
                    case 2:
                        
                        //MacCommonSubgraph.Find(graph1, graph2);
                        break;
                    case 3:
                        
                        break;
                    case 0:
                        Console.Clear();
                        break;
                    default:
                        break;
                }
            } 
            
            return;

        }
    }
}
