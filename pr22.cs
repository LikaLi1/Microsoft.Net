using System;
using System.Collections.Generic;

namespace Example
{
    class Edge
    {
        public int To { get; set; }
        public int Weight { get; set; }

        public Edge(int to, int weight)
        {
            To = to;
            Weight = weight;
        }
    }

    class Graph
    {
        private Dictionary<int, List<Edge>> adjList;

        public Graph()
        {
            adjList = new Dictionary<int, List<Edge>>();
        }

        public void AddEdge(int from, int to, int weight)
        {
            if (!adjList.ContainsKey(from))
                adjList[from] = new List<Edge>();
            adjList[from].Add(new Edge(to, weight));
        }

        public void PrintGraph()
        {
            foreach (var pair in adjList)
            {
                Console.Write($"Вершина {pair.Key}: ");
                foreach (var edge in pair.Value)
                    Console.Write($"->{edge.To} (вес {edge.Weight}) ");
                Console.WriteLine();
            }
        }

        public void Dijkstra(int start)
        {
            var distances = new Dictionary<int, int>();
            var visited = new HashSet<int>();

            foreach (var node in adjList.Keys)
                distances[node] = int.MaxValue;

            distances[start] = 0;

            var pq = new SortedSet<Tuple<int, int>>(Comparer<Tuple<int, int>>.Create(
                (a, b) => a.Item1 == b.Item1 ? a.Item2.CompareTo(b.Item2) : a.Item1.CompareTo(b.Item1)
            ));

            pq.Add(Tuple.Create(0, start));

            while (pq.Count > 0)
            {
                var current = pq.Min;
                pq.Remove(current);

                int currentDist = current.Item1;
                int currentNode = current.Item2;

                if (visited.Contains(currentNode))
                    continue;
                visited.Add(currentNode);

                if (!adjList.ContainsKey(currentNode))
                    continue;

                foreach (var edge in adjList[currentNode])
                {
                    int neighbor = edge.To;
                    int weight = edge.Weight;

                    int newDist = distances[currentNode] + weight;
                    if (newDist < distances[neighbor])
                    {
                        distances[neighbor] = newDist;
                        pq.Add(Tuple.Create(newDist, neighbor));
                    }
                }
            }

            Console.WriteLine("Кратчайшие расстояния от вершины " + start + ":");
            foreach (var kvp in distances)
                Console.WriteLine($"До {kvp.Key} = {kvp.Value}");
        }

        public void BellmanFord(int start)
        {
            var distances = new Dictionary<int, int>();
            foreach (var node in adjList.Keys)
                distances[node] = int.MaxValue;
            distances[start] = 0;

            int V = adjList.Count;

            for (int i = 0; i < V - 1; i++)
            {
                foreach (var u in adjList.Keys)
                {
                    foreach (var edge in adjList[u])
                    {
                        int v = edge.To;
                        int weight = edge.Weight;

                        if (distances[u] != int.MaxValue && distances[u] + weight < distances[v])
                        {
                            distances[v] = distances[u] + weight;
                        }
                    }
                }
            }

            foreach (var u in adjList.Keys)
            {
                foreach (var edge in adjList[u])
                {
                    int v = edge.To;
                    int weight = edge.Weight;
                    if (distances[u] != int.MaxValue && distances[u] + weight < distances[v])
                    {
                        Console.WriteLine("Обнаружен отрицательный цикл!");
                        return;
                    }
                }
            }

            Console.WriteLine("Результаты Беллмана-Форда от вершины " + start + ":");
            foreach (var kvp in distances)
                Console.WriteLine($"До {kvp.Key} = {kvp.Value}");
        }

        public void UnweightedShortestPaths(int start)
        {
            var distances = new Dictionary<int, int>();
            var visited = new HashSet<int>();
            var queue = new Queue<int>();

            foreach (var node in adjList.Keys)
                distances[node] = int.MaxValue;
            distances[start] = 0;
            visited.Add(start);
            queue.Enqueue(start);

            while (queue.Count > 0)
            {
                int current = queue.Dequeue();

                if (!adjList.ContainsKey(current))
                    continue;

                foreach (var edge in adjList[current])
                {
                    int neighbor = edge.To;
                    if (!visited.Contains(neighbor))
                    {
                        visited.Add(neighbor);
                        distances[neighbor] = distances[current] + 1;
                        queue.Enqueue(neighbor);
                    }
                }
            }

            Console.WriteLine("Результаты BFS (невзвешенный граф) от вершины " + start + ":");
            foreach (var kvp in distances)
                Console.WriteLine($"До {kvp.Key} = {kvp.Value}");
        }

        public void DFS(int start)
        {
            var visited = new HashSet<int>();
            Console.WriteLine($"Обход DFS от вершины {start}:");
            DFSUtil(start, visited);
        }

        private void DFSUtil(int node, HashSet<int> visited)
        {
            visited.Add(node);
            Console.WriteLine($"Посетили вершину {node}");

            if (adjList.ContainsKey(node))
            {
                foreach (var edge in adjList[node])
                {
                    if (!visited.Contains(edge.To))
                    {
                        DFSUtil(edge.To, visited);
                    }
                }
            }
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Graph g = new Graph();

            var rand = new Random();
            int nodeCount = 1000;
            int edgeCount = 2000;

            for (int i = 0; i < edgeCount; i++)
            {
                int from = rand.Next(0, nodeCount);
                int to = rand.Next(0, nodeCount);
                int weight = rand.Next(1, 20);
                g.AddEdge(from, to, weight);
            }
            g.DFS(0);

            Console.WriteLine("Обход в глубину завершен.");
            Console.ReadLine();
        }
    }
}
