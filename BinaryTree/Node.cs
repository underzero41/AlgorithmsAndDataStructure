namespace BalancedBinaryTreeLib;

public enum Color { Red, Black }

/// <summary> class Node </summary>
/// <typeparam name="T">Type of value must implement IComparable<T> interface</typeparam>
public class Node<T> where T : IComparable<T>
{
    public T Value { get; set; }
    public Node<T>? Parent { get; set; }
    public Node<T>? Left { get; set; }
    public Node<T>? Right { get; set; }
    public Color Color { get; set; }

    /// <summary> create node for red-black tree </summary>
    /// <param name="value">value for node</param>
    /// <param name="parent">parent node, null for root</param>
    /// <param name="color">[optional]color of node, default Color.Red</param>
    public Node(T value, Node<T>? parent, Color color = Color.Red)
    {
        Value = value;
        Parent = parent;
        Color = color;
        Left = null;
        Right = null;
    }
}