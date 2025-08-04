using System.Diagnostics;
using LabsLib.BinrayBalancedTree;

namespace Specialist_Lab_1_5;

internal class Program
{
    static async Task Main(string[] args)
    {
        int treeLevel = 24;
        Console.WriteLine($"Starting tree creation with depth {treeLevel}...");
        Tree tree = new(25);
        Console.WriteLine($"Tree created with total weight: {tree.Total}");

        Stopwatch timer = new();

        timer.Start();
        long weightTree = Tree.WeightTree(tree.RootNode);
        timer.Stop();
        Console.WriteLine($"Single weight: {weightTree} Sync Time {timer.ElapsedMilliseconds}");

        timer.Reset();

        timer.Start();
        weightTree = await Tree.WeightTreeAsync(tree.RootNode, Environment.ProcessorCount);
        timer.Stop();
        Console.WriteLine($"Single weight: {weightTree} Async Time {timer.ElapsedMilliseconds}");
    }
}