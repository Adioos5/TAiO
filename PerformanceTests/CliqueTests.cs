using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAiO;

namespace PerformanceTests
{
    public class CliqueTests
    {
        public Stopwatch sw = new Stopwatch();
        string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        public void RunTests()
        {
            TestConstVert();
            TestConstEdg();
        }
        public void TestConstVert()
        {
            int vertices = 20, edges = 0, graphsToCheck = 80;
            double[] timesExactAlg = new double[graphsToCheck], timesApprAlg = new double[graphsToCheck];

            var graphGeneratorConstVert = new GraphGeneratorConstVert(vertices, edges);
            var graph = graphGeneratorConstVert.Generate();
            for (int i = 0; i < graphsToCheck; i++)
            {
                double sum = 0;
                for(int j= 0; j<3; j++)
                {
                    sw.Start();
                    _ = CliqueSolver.BiggestClique(graph);
                    sw.Stop();
                    sum += sw.Elapsed.TotalSeconds;
                    sw.Reset();
                }
                timesExactAlg[i] = sum/3;

                sum = 0;
                for(int j=0; j<3; j++)
                {
                    sw.Restart();
                    _ = CliqueSolver.AproxClique(graph);
                    sw.Stop();
                    sum += sw.Elapsed.TotalSeconds;
                }
                timesApprAlg[i] = sum/3;
                sw.Reset();
                graph = graphGeneratorConstVert.GenerateNext(1);
            }
            File.WriteAllLines(path + "\\CliqueAlgorithmTimes_1.txt", timesExactAlg.Select(tb => tb.ToString()));
            File.WriteAllLines(path + "\\CliqueApproximationTimes_1.txt", timesApprAlg.Select(tb => tb.ToString()));
        }
        public void TestConstEdg()
        {
            int vertices = 5, edges = 15, graphsToCheck = 80;
            double[] timesExactAlg = new double[graphsToCheck], timesApprAlg = new double[graphsToCheck];

            var graphGeneratorConstEdg = new GraphGeneratorConstEdg(vertices, edges);
            var graph = graphGeneratorConstEdg.Generate();
            for (int i = 0; i < graphsToCheck; i++)
            {
                double sum = 0;
                for (int j = 0; j < 3; j++)
                {
                    sw.Start();
                    _ = CliqueSolver.BiggestClique(graph);
                    sw.Stop();
                    sum += sw.Elapsed.TotalSeconds;
                    sw.Reset();
                }
                timesExactAlg[i] = sum / 3;

                sum = 0;
                for (int j = 0; j < 3; j++)
                {
                    sw.Restart();
                    _ = CliqueSolver.AproxClique(graph);
                    sw.Stop();
                    sum += sw.Elapsed.TotalSeconds;
                }
                timesApprAlg[i] = sum / 3;
                sw.Reset();
                graph = graphGeneratorConstEdg.GenerateNext(1);
            }
            File.WriteAllLines(path + "\\CliqueAlgorithmTimes_2.txt", timesExactAlg.Select(tb => tb.ToString()));
            File.WriteAllLines(path + "\\CliqueApproximationTimes_2.txt", timesApprAlg.Select(tb => tb.ToString()));
        }
    }
}
