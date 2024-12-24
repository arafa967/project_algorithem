namespace pro
{
    internal class Program
    {
        #region 1- Heap Sort
        public static void Sort(int[] array)
        {
            int n = array.Length;

            // Build the heap
            for (int i = n / 2 - 1; i >= 0; i--)
                Heapify(array, n, i);

            // Extract elements from the heap
            for (int i = n - 1; i >= 0; i--)
            {
                // Swap root with the last element
                int temp = array[0];
                array[0] = array[i];
                array[i] = temp;

                // Heapify the reduced heap
                Heapify(array, i, 0);
            }
        }

        private static void Heapify(int[] array, int n, int i)
        {
            int largest = i;
            int left = 2 * i + 1;
            int right = 2 * i + 2;

            // Check if left child is larger
            if (left < n && array[left] > array[largest])
                largest = left;

            // Check if right child is larger
            if (right < n && array[right] > array[largest])
                largest = right;
            // If root is not largest, swap and heapify
            if (largest != i)
            {
                int swap = array[i];
                array[i] = array[largest];
                array[largest] = swap;

                Heapify(array, n, largest);
            }
        }
        #endregion

        #region 2- Kruskal Algorithm
        public class Edge
        {
            public int src, dest, weight;
            public Edge(int src, int dest, int weight)
            {
                this.src = src;
                this.dest = dest;
                this.weight = weight;
            }
        }

        // Union-Find Data Structure
        public class DisjointSet
        {
            private int[] parent, rank;

            public DisjointSet(int n)
            {
                parent = new int[n];
                rank = new int[n];

                // Initialize each node as its own parent (disjoint sets)
                for (int i = 0; i < n; i++)
                {
                    parent[i] = i;
                    rank[i] = 0;
                }
            }

            // Find the representative of the set containing i
            public int Find(int i)
            {
                if (parent[i] != i)
                    parent[i] = Find(parent[i]); // Path compression
                return parent[i];
            }

            // Union of two sets
            public void Union(int x, int y)
            {
                int rootX = Find(x);
                int rootY = Find(y);

                // Union by rank
                if (rootX != rootY)
                {
                    if (rank[rootX] > rank[rootY])
                        parent[rootY] = rootX;
                    else if (rank[rootX] < rank[rootY])
                        parent[rootX] = rootY;
                    else
                    {
                        parent[rootY] = rootX;
                        rank[rootX]++;
                    }
                }
            }
        }

        // Kruskal's algorithm to find MST
        public class KruskalAlgorithm
        {
            public List<Edge> KruskalMST(List<Edge> edges, int numVertices)
            {
                // Sort edges by weight
                edges.Sort((e1, e2) => e1.weight.CompareTo(e2.weight));

                DisjointSet ds = new DisjointSet(numVertices);
                List<Edge> mst = new List<Edge>();

                foreach (Edge edge in edges)
                {
                    // Check if the nodes are in different sets (i.e., no cycle)
                    if (ds.Find(edge.src) != ds.Find(edge.dest))
                    {
                        // Add the edge to the MST
                        mst.Add(edge);
                        ds.Union(edge.src, edge.dest);
                    }
                }

                return mst;
            }
        }

        #endregion
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            #region 1- Heap Sort
            int[] array = { 12, 11, 13, 5, 6, 7 };
            Console.WriteLine("=================1. Heap Sort ======================");
            Console.WriteLine("Unsorted array: " + string.Join(", ", array));

            Sort(array);

            Console.WriteLine("Sorted array: " + string.Join(", ", array));
            #endregion

            #region 2- Kruskal Algorithm
            KruskalAlgorithm kruskal = new KruskalAlgorithm();

            // Create a list of edges for the graph
            List<Edge> edges = new List<Edge>
            {
                new Edge(0, 1, 10),
                new Edge(0, 2, 6),
                new Edge(0, 3, 5),
                new Edge(1, 3, 15),
                new Edge(2, 3, 4)
            };

            int numVertices = 4;

            // Call Kruskal's algorithm to find the MST
            List<Edge> mst = kruskal.KruskalMST(edges, numVertices);

            // Print the edges in the MST
            Console.WriteLine("=================2. Kruskal Algorithm ======================");
            Console.WriteLine("Edges in the MST:");
            foreach (var edge in mst)
            {
                Console.WriteLine($"({edge.src}, {edge.dest}) -> {edge.weight}");
            }

            #endregion

        }
    }
}
