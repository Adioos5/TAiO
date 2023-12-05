using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TAiO;

namespace PerformanceTests
{
    public class SubgraphAndMetricTests
    {
        public Stopwatch sw = new Stopwatch();
        string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        public void RunTests()
        {
            TestConstVertFullGraph();
            TestConstVertNotFullGraph();
        }
        public DirectedGraph GenerateFullGraph(int vertices)
        {
            var Graph = new DirectedGraph(vertices);
            for (int i = 0; i < vertices; i++)
            {
                for (int j = 0; j < vertices; j++)
                {
                    if (i != j)
                    {
                        Graph.AddEdge(i, j);
                    }
                }
            }

            return Graph;
        }

        public void TestConstVertFullGraph()
        {
            int vertices = 10, edges = 0, graphsToCheck = 10;
            double[] timesExactAlg = new double[graphsToCheck], timesApprAlg = new double[graphsToCheck];
            double[] timesExactAlgM = new double[graphsToCheck], timesApprAlgM = new double[graphsToCheck];

            var graphUnchanging = GenerateFullGraph(vertices);
            var graphGeneratorConstVert = new GraphGeneratorConstVert(vertices, edges);
            var graphChanging = graphGeneratorConstVert.Generate();

            for (int i = 0; i < graphsToCheck; i++)
            {
                double sum = 0;
                for (int j = 0; j < 3; j++)
                {
                    sw.Start();
                    _ = MaximalCommonSubgraph.FindPrecisely(graphUnchanging, graphChanging);
                    sw.Stop();
                    sum += sw.Elapsed.TotalSeconds;
                    sw.Reset();
                }
                timesExactAlg[i] = sum / 3;

                sum = 0;
                for (int j = 0; j < 3; j++)
                {
                    sw.Restart();
                    _ = MaximalCommonSubgraph.Approximate(graphUnchanging, graphChanging);
                    sw.Stop();
                    sum += sw.Elapsed.TotalSeconds;
                }
                timesApprAlg[i] = sum / 3;

                sum = 0;
                for (int j = 0; j < 3; j++)
                {
                    sw.Restart();
                    _ = Metric.CalculatePrecisely(graphUnchanging, graphChanging);
                    sw.Stop();
                    sum += sw.Elapsed.TotalSeconds;
                }
                timesExactAlgM[i] = sum / 3;

                sum = 0;
                for (int j = 0; j < 3; j++)
                {
                    sw.Restart();
                    _ = Metric.Approximate(graphUnchanging, graphChanging);
                    sw.Stop();
                    sum += sw.Elapsed.TotalSeconds;
                }
                timesApprAlgM[i] = sum / 3;
                sw.Reset();
                graphChanging = graphGeneratorConstVert.GenerateNext(5);
            }

            File.WriteAllLines(path + "\\SubgraphExactAlgorithmTimes_1.txt", timesExactAlg.Select(tb => tb.ToString()));
            File.WriteAllLines(path + "\\MetricExactAlgorithmTimes_1.txt", timesExactAlgM.Select(tb => tb.ToString()));
            File.WriteAllLines(path + "\\SubgraphApproximationTimes_1.txt", timesApprAlg.Select(tb => tb.ToString()));
            File.WriteAllLines(path + "\\MetricApproximationTimes_1.txt", timesApprAlgM.Select(tb => tb.ToString()));
        }

        public void TestConstVertNotFullGraph()
        {
            int vertices = 10, edges = 0, graphsToCheck = 10;
            double[] timesExactAlg = new double[graphsToCheck], timesApprAlg = new double[graphsToCheck];
            double[] timesExactAlgM = new double[graphsToCheck], timesApprAlgM = new double[graphsToCheck];

            var graphGeneratorConstVert = new GraphGeneratorConstVert(vertices, 24);
            var graphUnchanging = graphGeneratorConstVert.Generate();
            graphGeneratorConstVert.edges = edges;
            var graphChanging = graphGeneratorConstVert.Generate();
            for (int i = 0; i < graphsToCheck; i++)
            {
                double sum = 0;
                for(int j=0; j<3; j++)
                {
                    sw.Start();
                    _ = MaximalCommonSubgraph.FindPrecisely(graphUnchanging, graphChanging);
                    sw.Stop();
                    sum += sw.Elapsed.TotalSeconds;
                    sw.Reset();
                }
                timesExactAlg[i] = sum/3;

                sum = 0;
                for (int j = 0; j < 3; j++)
                {
                    sw.Restart();
                    _ = MaximalCommonSubgraph.Approximate(graphUnchanging, graphChanging);
                    sw.Stop();
                    sum += sw.Elapsed.TotalSeconds;
                }
                timesApprAlg[i] = sum/3;

                sum = 0;
                for (int j = 0; j < 3; j++)
                {
                    sw.Restart();
                    _ = Metric.CalculatePrecisely(graphUnchanging, graphChanging);
                    sw.Stop();
                    sum += sw.Elapsed.TotalSeconds;
                }
                timesExactAlgM[i] = sum/3;

                sum = 0;
                for (int j = 0; j < 3; j++)
                {
                    sw.Restart();
                    _ = Metric.Approximate(graphUnchanging, graphChanging);
                    sw.Stop();
                    sum += sw.Elapsed.TotalSeconds;
                }
                timesApprAlgM[i] = sum/3;
                sw.Reset();
                graphChanging = graphGeneratorConstVert.GenerateNext(5);
            }
            File.WriteAllLines(path + "\\SubgraphExactAlgorithmTimes_2.txt", timesExactAlg.Select(tb => tb.ToString()));
            File.WriteAllLines(path + "\\MetricExactAlgorithmTimes_2.txt", timesExactAlgM.Select(tb => tb.ToString()));
            File.WriteAllLines(path + "\\SubgraphApproximationTimes_2.txt", timesApprAlg.Select(tb => tb.ToString()));
            File.WriteAllLines(path + "\\MetricApproximationTimes_2.txt", timesApprAlgM.Select(tb => tb.ToString()));
        }
    }
}
