using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Emit;

namespace KA_FordFalkerson
{
    internal class Program
    {
        public class Node
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
            var i = 1;
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

                    array[i] = int.Parse(line[j]);
                    i++;
                }
            }


            var XPart = new List<Node>();
            var YPart = new List<Node>();
            XPart.Add(new Node(0));
            YPart.Add(new Node(0));
            for (i = 1; i < k + 1; i++)
            {
                XPart.Add(new Node(i));
            }

            for (i = 1; i < l + 1; i++)
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
                var next = array[z + 1];
                for (var m = array[z]; m < next; m++)
                {
                    node.Nodes.Add(array[m] + k);
                    if (!YPart[array[m]].Nodes.Contains(z))
                        YPart[array[m]].Nodes.Add(z);
                }

                z++;
            }

            int[,] A = new int[k + l + 2, k + l + 2];
            int[,] F = new int[k + l + 2, k + l + 2];

            for (i = 1; i < XPart.Count; i++)
            {
                A[0, i] = 1;
            }

            for (i = 1; i < YPart.Count; i++)
            {
                A[i + k, k + l + 1] = 1;
            }

            foreach (var node in XPart)
            {
                foreach (var innernode in node.Nodes)
                {
                    A[node.Number, innernode] = 1;
                }
            }

            var h = new int[k + l + 2];

            do
            {
            var prev = new int[k + l + 2];
                var choice = new int[k + l + 2];
                var father = new int[k + l + 2];
                Labeling(k, l, h, prev, A, F, choice, father);
                if (h[k + l + 1] < int.MaxValue)
                {
                    var v = k + l + 1;
                    while (v != 0)
                    {
                        var w = prev[v];
                        if (choice[v] == 1)
                        {
                            F[w, v] += h[k + l + 1];
                        }
                        else
                        {
                            F[v, w] -= h[k + l + 1];
                        }

                        v = w;
                    }
                }
            } 
        while (h[k+l+1] == int.MaxValue);

            PrintArray(F);
        }
        
        public static void Labeling(int k, int l, int[] h, int[] prev, int[,] A, int[,] F, int[] choice, int[] father)
        {
            for (var i = 0; i < k + l + 2; i++)
            {
                h[i] = int.MaxValue;
            }

            var Q = new Queue<int>();
            Q.Enqueue(0);
            prev[0] = -1;
            while (h[k + l + 1] == int.MaxValue && Q.Count != 0)
            {
                var w = Q.Dequeue();
                for (var v = 0; v < k + l + 2; v++)
                {
                    if (h[v] == int.MaxValue && (A[w, v] - F[w, v]) > 0)
                    {
                        h[v] = Math.Min(h[w], A[w, v] - F[w, v]);
                        prev[v] = w;
                        Q.Enqueue(v);
                        choice[v] = 1;
                    }

                    for (var item = 1; item < k + l + 2; item++)
                    {
                        if (h[item] == int.MaxValue && F[w, item] > 0)
                        {
                            h[item] = Math.Min(h[w], F[w, item]);
                            Q.Enqueue(item);
                            father[item] = w;
                            choice[item] = -1;
                        }
                    }
                }
            }
        }
        
        public static void PrintArray(int[,] A)
        {
            int rowLength = A.GetLength(0);
            int colLength = A.GetLength(1);

            for (var i = 0; i < rowLength; i++)
            {
                for (int j = 0; j < colLength; j++)
                {
                    Console.Write(string.Format("{0} ", A[i, j]));
                }

                Console.Write(Environment.NewLine);
            }
        }
    }
}