using BalancedBinaryTreeLib;

ColorBinaryTree<int> tree = new ColorBinaryTree<int>();

tree.Add(10);
tree.Add(20);
tree.Add(5);
tree.Add(4);
tree.Add(4);
tree.Add(3);
tree.Add(18);
tree.Add(24);
tree.Add(35);

Console.WriteLine("Red-Black Tree:");
tree.PrintTree();

Console.WriteLine("Contains 18: " + tree.Contains(18));
Console.WriteLine("Contains 30: " + tree.Contains(30));

tree.Remove(24);

Console.WriteLine("After removing 20:");
tree.PrintTree();