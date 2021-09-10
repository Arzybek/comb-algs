using System;
using System.Collections.Generic;
using System.Linq;

namespace KA_min_tree
{
    class Program
    {
        class Point
        {
            public int X;
            public int Y;
            public int N;
            public static int Metric(Point first, Point second)
            {
                return Math.Abs(first.X - second.X) + Math.Abs(first.Y - second.Y);
            }
            public Point(int x, int y, int N)
            {
                this.X = x;
                this.Y = y;
                this.N = N;
            }
        }

        class Node
        {
            public Point From;
            public Point To;
            public int cost;

            public Node(Point first, Point second)
            {
                From = first;
                To = second;
                cost = Point.Metric(first, second);
            }
        }

        static void Main(string[] args)
        {
            /* Алгоритм Крускала
            Данный алгоритм был описан Крускалом (Kruskal) в 1956 г.
            Алгоритм Крускала изначально помещает каждую вершину в своё дерево, а затем постепенно объединяет эти деревья, объединяя на каждой итерации 
            два некоторых дерева некоторым ребром. Перед началом выполнения алгоритма, все рёбра сортируются по весу (в порядке неубывания). 
            Затем начинается процесс объединения: перебираются все рёбра от первого до последнего (в порядке сортировки), и если у текущего ребра 
            его концы принадлежат разным поддеревьям, то эти поддеревья объединяются, а ребро добавляется к ответу. По окончании перебора всех рёбер 
            все вершины окажутся принадлежащими одному поддереву, и ответ найден.
            Простейшая реализация
            Этот код самым непосредственным образом реализует описанный выше алгоритм, и выполняется за O (M log N + N2). Сортировка рёбер 
            потребует O (M log N) операций. Принадлежность вершины тому или иному поддереву хранится просто с помощью массива tree_id - в нём для 
            каждой вершины хранится номер дерева, которому она принадлежит. Для каждого ребра мы за O (1) определяем, принадлежат ли его концы разным 
            деревьям. Наконец, объединение двух деревьев осуществляется за O (N) простым проходом по массиву tree_id. Учитывая, что всего операций 
            объединения будет N-1, мы и получаем асимптотику O (M log N + N2). */

            int N = int.Parse(Console.ReadLine());
            Point[] points = new Point[N];
            List<Node> nodes = new List<Node>(N);
            for (var i = 0; i < N; i++)
            {
                string[] input = Console.ReadLine().Split(' ');
                Point point = new Point(int.Parse(input[0]), int.Parse(input[1]), i + 1);
                points[i] = point;
            }

            for (int i = 0; i < N; i++)
            {
                for (var j = i + 1; j < N; j++)
                {
                    Node node = new Node(points[i], points[j]);
                    nodes.Add(node);
                }
            }
            nodes.Sort((x, y) => x.cost.CompareTo(y.cost));

            int cost = 0;
            int[] treeId = new int[N];
            for (int i = 0; i < N; i++)
            {
                treeId[i] = i;
            }
            List<Node> result = new List<Node>();
            for (int i = 0; i < nodes.Count; ++i)
            {
                int from = nodes[i].From.N - 1;
                int to = nodes[i].To.N - 1;
                if (treeId[from] != treeId[to])
                {
                    cost += nodes[i].cost;
                    result.Add(nodes[i]);
                    int oldId = treeId[to];
                    int newId = treeId[from];
                    for (int j = 0; j < N; ++j)
                        if (treeId[j] == oldId)
                            treeId[j] = newId;
                }
            }
            //foreach (var node in result)
            //{
            //    Console.WriteLine("From: (" + node.From.X + ", " + node.From.Y + ") " + "To: (" + node.To.X + ", " + node.To.Y + ")");
            //}
            for (var i = 0; i < N; i++)
            {
                Point cPt = points[i];
                List<int> results = new List<int>();
                foreach (var node in result)
                {
                    if (node.From == cPt)
                    {
                        results.Add(node.To.N);
                    }
                    else if (node.To == cPt)
                    {
                        results.Add(node.From.N);
                    }
                }
                results.Sort();
                foreach (var res in results)
                {
                    Console.Write(res + " ");
                }
                Console.Write("0\n");
            }
            Console.WriteLine(cost);
            Console.ReadKey();
        }
    }
}
