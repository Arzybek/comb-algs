using System;
using System.Collections.Generic;
using System.IO;

namespace KA_FordFalkerson
{
    internal class Program
    {
        class Node
        {
            public int Number;
            public List<int> Nodes = new List<int>();

            public Node(int n)
            {
                Number = n;
            }
        }

        public static void Main(string[] args)
        {
            StreamReader file = new StreamReader("in.txt");
            var f = file.ReadLine().Split(' ');
            var k = int.Parse(f[0]);
            var l = int.Parse(f[1]);
            var sizeOfArray = int.Parse(file.ReadLine());
            var array = new int[sizeOfArray];
            var index = 1;
            var flag = true;
            while (flag)
            {
                var line = file.ReadLine().Split(' ');
                for (var j = 0; j < line.Length; j++)
                {
                    if (line[j] == "32767")
                    {
                        flag = false;
                        break;
                    }

                    array[index] = int.Parse(line[j]);
                    index++;
                }
            }


            var XPart = new List<Node>();
            var YPart = new List<Node>();
            XPart.Add(new Node(0));
            YPart.Add(new Node(0));
            for (var i = 1; i < k + 1; i++)
            {
                XPart.Add(new Node(i));
            }

            for (var i = 1; i < l + 1; i++)
            {
                YPart.Add(new Node(i + k));
            }

            var z = 1;
            while (z < k + 1)
            {
                if (array[z] == 0)
                {
                    z++;
                    continue;
                }

                var node = XPart[z];
                for (var i = 1; i < k + 1; i++)
                {
                    var next = array[z + i];
                    if (next != 0)
                    {
                        for (var m = array[z]; m < next; m++)
                        {
                            node.Nodes.Add(array[m] + k);
                            if (!YPart[array[m]].Nodes.Contains(z))
                                YPart[array[m]].Nodes.Add(z);
                        }

                        break;
                    }
                }

                z++;
            }

            List<(int, int)> graph = new List<(int, int)>();
            for (var i = 1; i < XPart.Count; i++)
            {
                graph.Add((0, i));
            }

            for (var i = 1; i < YPart.Count; i++)
            {
                graph.Add((i + k, k + l + 1));
            }

            foreach (var node in XPart)
            {
                foreach (var innernode in node.Nodes)
                {
                    graph.Add((node.Number, innernode));
                }
            }

            int[] px = new int[l + k + 2];
            int[] py = new int[k + l + 2];
            bool[] vis = new bool[k + l + 2];

            fill(px);
            fill(py);

            var isPath = true;
            while (isPath)
            {
                isPath = false;
                for (var i = 0; i < vis.Length; i++)
                {
                    vis[i] = false;
                }

                foreach (var node in XPart)
                {
                    if (px[node.Number] == -1)
                        if (dfs(node.Number, vis, px, py, graph))
                            isPath = true;
                }
            }

            var result = new Dictionary<int, int>();
            foreach (var nodeX in XPart)
            {
                foreach (var nodeY in YPart)
                {
                    if (px[nodeX.Number] == nodeY.Number)
                        result[nodeX.Number] = nodeY.Number;
                }
            }

            var strResult = "";
            for (index = 1; index < XPart.Count; index++)
            {
                if (result.ContainsKey(index))
                    strResult += (result[index]-k)+" ";
                else
                    strResult += "0 ";
            }
            
            StreamWriter res = new StreamWriter("out.txt");
            res.WriteLine(strResult);
            res.Close();
            file.Close();
        }

        public static void fill(int[] px)
        {
            for (var k = 0; k < px.Length; k++)
            {
                px[k] = -1;
            }
        }

        public static bool dfs(int x, bool[] vis, int[] px, int[] py, List<(int, int)> graph)
        {
            if (vis[x])
                return false;
            vis[x] = true;
            foreach (var node in graph)
            {
                if (node.Item1 != x)
                    continue;
                if(node.Item1 == 0 || node.Item2==px.Length-1)
                    continue;
                if (py[node.Item2] == -1)
                {
                    py[node.Item2] = node.Item1;
                    px[node.Item1] = node.Item2;
                    return true;
                }
                else
                {
                    if (dfs(py[node.Item2], vis, px, py, graph))
                    {
                        py[node.Item2] = node.Item1;
                        px[node.Item1] = node.Item2;
                        return true;
                    }
                }
            }

            return false;
        }
    }
}