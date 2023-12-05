// See https://aka.ms/new-console-template for more information
using PerformanceTests;

var cliqueTests = new CliqueTests();
var subgraphmetricTests = new SubgraphAndMetricTests();
Console.WriteLine("Tests are in progress, it may take a while");
cliqueTests.RunTests();
subgraphmetricTests.RunTests();
