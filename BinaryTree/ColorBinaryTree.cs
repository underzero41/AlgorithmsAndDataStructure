namespace BalancedBinaryTreeLib;

/// <summary>
/// ColorBinaryTree
/// </summary>
/// <typeparam name="T">T as IComparable<T></typeparam>
public class ColorBinaryTree<T> where T : IComparable<T>
{
    /// <summary> Root node </summary>
    private Node<T>? root;

    /// <summary> Create tree  </summary>
    public ColorBinaryTree()
    {
        root = null;
    }

    /// <summary> Add new value in tree </summary>
    /// <param name="item">value</param>
    /// <returns>Is successfully</returns>
    public bool Add(T item)
    {
        if (Contains(item))
        {
            return false; // Item already exists in the tree
        }

        root = Insert(root, item);
        root.Color = Color.Black; // Ensure the root is black
        return true;
    }

    /// <summary> insert new item </summary>
    /// <param name="node">node for search</param>
    /// <param name="item">value</param>
    /// <returns>inserted node</returns>
    private Node<T> Insert(Node<T>? node, T item)
    {
        if (node == null)
        {
            return new Node<T>(item, null);
        }

        //if less
        if (item.CompareTo(node.Value) < 0)
        {
            node.Left = Insert(node.Left, item);
            node.Left.Parent = node;
        }
        else
        {
            node.Right = Insert(node.Right, item);
            node.Right.Parent = node;
        }

        // Fix violations and perform rotations if needed
        if (node.Left?.Color == Color.Red && node.Left.Left?.Color == Color.Red)
        {
            node = RotateRight(node);
        }
        else if (node.Right?.Color == Color.Red && node.Left?.Color == Color.Red)
        {
            node = RotateLeft(node);
        }
        else if (node.Left?.Color == Color.Red && node.Right?.Color == Color.Red)
        {
            FlipColors(node);
        }

        return node;
    }

    /// <summary>
    /// will remove a value if that value is contained in the tree, 
    /// if the value is not found, it will return false
    /// </summary>
    /// <param name="item">value</param>
    /// <returns>is successfully</returns>
    public bool Remove(T item)
    {
        if (!Contains(item))
        {
            return false; // Item not found in the tree
        }
        
        root = Delete(root!, item);
        if (root != null)
        {
            root.Color = Color.Black; // Ensure the root is black
        }
        return true;
    }

    /// <summary>
    /// will remove a value if that value is contained in the tree, 
    /// if the value is not found, it will return null
    /// </summary>
    /// <param name="node">node for search</param>
    /// <param name="item">value</param>
    /// <returns>founded node</returns>
    private Node<T> Delete(Node<T> node, T item)
    {
        if (item.CompareTo(node.Value) < 0 && node.Left != null)
        {
            // Continue searching in the left subtree
            node.Left = Delete(node.Left, item);
        }
        else if (item.CompareTo(node.Value) > 0 && node.Right != null)
        {
            // Continue searching in the right subtree
            node.Right = Delete(node.Right, item);
        }
        else
        {
            // Node to be deleted found
            if (node.Left == null)
            {
                return node.Right!;
            }
            else if (node.Right == null)
            {
                return node.Left!;
            }
            else // Node to be deleted has two children
            {                
                Node<T> successor = MinValueNode(node.Right);
                node.Value = successor.Value;
                node.Right = Delete(node.Right, successor.Value);
            }
        }

        // Fix violations and perform rotations if needed
        if (node.Left?.Color == Color.Red && node.Left.Left?.Color == Color.Red)
        {
            node = RotateRight(node);
        }
        if (node.Right?.Color == Color.Red && node.Left?.Color == Color.Red)
        {
            node = RotateLeft(node);
        }
        if (node.Left?.Color == Color.Red && node.Right?.Color == Color.Red)
        {
            FlipColors(node);
        }

        return node;
    }

    /// <summary> Is contains value in tree </summary>
    /// <param name="item">value</param>
    /// <returns>Is contains</returns>
    public bool Contains(T item)
    {
        return Find(item) != null;
    }

    /// <summary> searching value in the tree </summary>
    /// <param name="item">value</param>
    /// <returns>founded node or null</returns>
    public Node<T>? Find(T item)
    {
        Node<T>? current = root;
        while (current != null)
        {
            if (item.CompareTo(current.Value) == 0)
            {
                return current; // Item found
            }
            else if (item.CompareTo(current.Value) < 0)
            {
                current = current.Left;
            }
            else
            {
                current = current.Right;
            }
        }
        return null; // Item not found
    }

    /// <summary> left rotation </summary>
    /// <param name="node">root node for rotation</param>
    /// <returns>balanced node</returns>
    private Node<T> RotateLeft(Node<T> node)
    {
        Node<T> newRoot = node.Right!;
        node.Right = newRoot.Left;
        newRoot.Left = node;
        newRoot.Color = node.Color;
        node.Color = Color.Red;
        return newRoot;
    }

    /// <summary> right rotation </summary>
    /// <param name="node">root node for rotation</param>
    /// <returns>balanced node</returns>
    private Node<T> RotateRight(Node<T> node)
    {
        Node<T> newRoot = node.Left!;
        node.Left = newRoot.Right;
        newRoot.Right = node;
        newRoot.Color = node.Color;
        node.Color = Color.Red;
        return newRoot;
    }

    /// <summary> will flip colors in the node </summary>
    /// <param name="node">node for flip</param>
    private void FlipColors(Node<T> node)
    {
        node.Color = Color.Red;
        node.Left.Color = Color.Black;
        node.Right.Color = Color.Black;
    }

    /// <summary> will found node with less value </summary>
    /// <param name="node"></param>
    /// <returns></returns>
    private Node<T> MinValueNode(Node<T> node)
    {
        while (node.Left != null)
        {
            node = node.Left;
        }
        return node;
    }

    /// <summary> Print tree in console </summary>
    public void PrintTree()
    {
        PrintTree(root, "", true);
    }

    /// <summary> Print tree in console  </summary>
    /// <param name="node">current node</param>
    /// <param name="prefix">prefix for print string</param>
    /// <param name="isLeft">set flag is node left or right</param>
    private void PrintTree(Node<T>? node, string prefix, bool isLeft)
    {
        if (node != null)
        {
            if(ReferenceEquals(node,root)) Console.WriteLine("Root: " + node.Value + "(" + node.Color + ")");
            else Console.WriteLine(prefix + (isLeft ? "L-- " : "R-- ") + node.Value + "(" + node.Color + ")");
            PrintTree(node.Left, prefix + (isLeft ? "|\t" : "\t"), true);
            PrintTree(node.Right, prefix + (isLeft ? "|\t" : "\t"), false);
        }
    }
}