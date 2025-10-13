// 代码生成时间: 2025-10-14 03:34:26
using System;
using System.Collections.Generic;
using System.Linq;

// 图论算法实现
// 该类包含了图的基本操作和图论算法的实现
public class GraphTheoryAlgorithm
{
    public enum GraphType
    {
        Undirected,
        Directed
    }

    public class Graph
    {
        public Dictionary<int, List<int>> AdjacencyList { get; private set; }
        public GraphType Type { get; private set; }

        public Graph(GraphType type)
        {
            AdjacencyList = new Dictionary<int, List<int>>();
            Type = type;
        }

        public void AddVertex(int vertex)
        {
            if (!AdjacencyList.ContainsKey(vertex))
            {
                AdjacencyList[vertex] = new List<int>();
            }
        }

        public void AddEdge(int from, int to)
        {
            // 无向图需要在邻接表中添加两个方向的边
            if (Type == GraphType.Undirected)
            {
                AddVertex(from);
                AddVertex(to);
                AdjacencyList[from].Add(to);
                AdjacencyList[to].Add(from);
            }
            else
            {
                // 有向图只在邻接表中添加一个方向的边
                AddVertex(from);
                AddVertex(to);
                AdjacencyList[from].Add(to);
            }
        }
    }

    public static void DepthFirstSearch(Graph graph, int startVertex)
    {
        HashSet<int> visited = new HashSet<int>();
        Stack<int> stack = new Stack<int>();
        stack.Push(startVertex);

        while (stack.Count > 0)
        {
            int vertex = stack.Pop();
            if (!visited.Contains(vertex))
            {
                Console.WriteLine("Visited vertex: " + vertex);
                visited.Add(vertex);

                foreach (var neighbor in graph.AdjacencyList[vertex])
                {
                    if (!visited.Contains(neighbor))
                    {
                        stack.Push(neighbor);
                    }
                }
            }
        }
    }

    public static void BreadthFirstSearch(Graph graph, int startVertex)
    {
        HashSet<int> visited = new HashSet<int>();
        Queue<int> queue = new Queue<int>();
        queue.Enqueue(startVertex);

        while (queue.Count > 0)
        {
            int vertex = queue.Dequeue();
            if (!visited.Contains(vertex))
            {
                Console.WriteLine("Visited vertex: " + vertex);
                visited.Add(vertex);

                foreach (var neighbor in graph.AdjacencyList[vertex])
                {
                    if (!visited.Contains(neighbor))
                    {
                        queue.Enqueue(neighbor);
                    }
                }
            }
        }
    }
}

// 使用示例
class Program
{
    static void Main(string[] args)
    {
        Graph graph = new GraphTheoryAlgorithm.Graph(GraphTheoryAlgorithm.GraphType.Undirected);
        graph.AddVertex(1);
        graph.AddVertex(2);
        graph.AddVertex(3);
        graph.AddEdge(1, 2);
        graph.AddEdge(2, 3);
        graph.AddEdge(3, 1);

        Console.WriteLine("Depth First Search: ");
        GraphTheoryAlgorithm.DepthFirstSearch(graph, 1);

        Console.WriteLine("Breadth First Search: ");
        GraphTheoryAlgorithm.BreadthFirstSearch(graph, 1);
    }
}