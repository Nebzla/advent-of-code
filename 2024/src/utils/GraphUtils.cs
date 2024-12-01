namespace _2024.src.utils
{
    public static class GraphUtils
    {
        public static Dictionary<T, int> Dijkstra<T>(Dictionary<T, Dictionary<T, int>> graph, T start) where T : notnull
        {
            HashSet<T> unvisitedNodes = [.. graph.Keys];
            Dictionary<T, int> shortestDistances = graph.Keys.ToDictionary(k => k, k => int.MaxValue);
            shortestDistances[start] = 0;

            T currentNode = start;
            while (unvisitedNodes.Count > 0)
            {
                int currentNodePath = shortestDistances[currentNode];

                //Checking distance to all connected nodes from current, if distance from current plus distance to their is less update
                foreach (KeyValuePair<T, int> distance in graph[currentNode])
                {
                    int path = currentNodePath + distance.Value;
                    if (path < shortestDistances[distance.Key])
                    {
                        shortestDistances[distance.Key] = path;
                    }
                }

                unvisitedNodes.Remove(currentNode);

                currentNode = shortestDistances
                .Where(kvp => unvisitedNodes.Contains(kvp.Key))
                .OrderBy(kvp => kvp.Value)
                .FirstOrDefault().Key; // Set next node to be next shorted unvisited
            }

            return shortestDistances;
        }

        public static int ShortestPath<T>(Dictionary<T, Dictionary<T, int>> graph, T start, T destination) where T : notnull
        {
            return Dijkstra(graph, start)[destination];
        }




        public static List<T> BreadthFirstSearch<T>(Dictionary<T, T[]> graph, T root) where T : notnull
        {
            List<T> visitedNodes = new(graph.Count);
            HashSet<T> visitedNodesHash = new(graph.Count); //Hash set used for extra efficiency in contains checking
            Queue<T> queue = [];
            queue.Enqueue(root);

            while (queue.Count > 0)
            {
                T visited = queue.Dequeue();
                if (!visitedNodesHash.Add(visited)) continue;
                visitedNodesHash.Add(visited);

                if (!graph.TryGetValue(visited, out var children)) continue;
                foreach (T child in children.AsSpan()) queue.Enqueue(child);
            }

            return visitedNodes;
        }

        public static List<T> DepthFirstSearch<T>(Dictionary<T, T[]> graph, T root) where T : notnull
        {
            List<T> visitedNodes = new(graph.Count);
            HashSet<T> visitedNodesHash = new(graph.Count); //Hash set used for extra efficiency in contains checking
            Stack<T> stack = [];
            stack.Push(root);

            while (stack.Count > 0)
            {
                T visited = stack.Pop();
                if (!visitedNodesHash.Add(visited)) continue;
                visitedNodes.Add(visited);

                if (!graph.TryGetValue(visited, out var children)) continue;
                foreach (T child in children.AsSpan()) stack.Push(child);
            }

            return visitedNodes;
        }
    }
}