namespace LabsLib.BinrayBalancedTree;

public class Tree
{
    private int level;
    private readonly Random rnd = new();
    public int Level
    {
        get => this.level;
        private set => this.level = value <= 0 ? 1 : value;
    }
    public long Total { get; private set; }
    public TreeNode RootNode { get; private set; }

    public Tree(int level)
    {
        this.Level = level;
        this.RootNode = new TreeNode();
        this.CreateRandomTree(this.RootNode, this.Level);
    }

    public static long WeightTree(TreeNode rootNode)
    {
        return
            rootNode.Weight +
            (rootNode.Left is null ? 0 : WeightTree(rootNode.Left)) +
            (rootNode.Right is null ? 0 : WeightTree(rootNode.Right));
    }

    public static async Task<long> WeightTreeAsync(TreeNode rootNode, int asyncLevel)
    {
        if (asyncLevel <= 0) return WeightTree(rootNode);
        Task<long>? leftNode = rootNode.Left is null ? null : Task.Run(() => WeightTree(rootNode.Left));
        Task<long>? rightNode = rootNode.Right is null ? null : Task.Run(() => WeightTree(rootNode.Right));
        return
            rootNode.Weight +
            (leftNode is null ? 0 : await leftNode) +
            (rightNode is null ? 0 : await rightNode);
    }

    private void CreateRandomTree(TreeNode node, int level)
    {
        node.Left = new TreeNode();
        node.Right = new TreeNode();
        node.Weight = rnd.Next(100);
        this.Total += node.Weight;
        level--;
        if (level == 0)
        {
            node.Left.Weight = rnd.Next(100);
            node.Right.Weight = rnd.Next(100);
            this.Total += node.Left.Weight;
            this.Total += node.Right.Weight;
            return;
        }
        CreateRandomTree(node.Left, level);
        CreateRandomTree(node.Right, level);
    }
}