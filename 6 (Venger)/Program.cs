using System;
using System.IO;

namespace KA_Venger
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            StreamReader file = new StreamReader("in.txt");
            var firstLine = file.ReadLine().Split(' ');
            var n = int.Parse(firstLine[0]);
            var m = int.Parse(firstLine[1]);
            int[,] P = new int[n + 1, m + 1];
            for (var i = 1; i < n + 1; i++)
            {
                var line = file.ReadLine().Split(' ');
                for (var k = 1; k < m + 1; k++)
                {
                    P[i, k] = int.Parse(line[k - 1]);
                }
            }

            int[,] Q = new int[n + 1, m + 1];
            for (var i = 1; i < n + 1; i++)
            {
                var line = file.ReadLine().Split(' ');
                for (var k = 1; k < m + 1; k++)
                {
                    Q[i, k] = int.Parse(line[k - 1]);
                }
            }

            int[] S = new int[n + 1];
            int[] V = new int[n + 1];

            var newLine = file.ReadLine().Split(' ');
            for (var k = 1; k < n + 1; k++)
            {
                S[k] = int.Parse(newLine[k - 1]);
            }

            newLine = file.ReadLine().Split(' ');
            for (var k = 1; k < n + 1; k++)
            {
                V[k] = int.Parse(newLine[k - 1]);
            }

            int[,] h = new int[n + 1, m + 1];
            for (var i = 1; i < n + 1; i++)
            {
                for (var k = 1; k < m + 1; k++)
                {
                    h[i, k] = S[i] - P[i, k] - Q[i, k];
                }
            }

            int[,] H = new int[n + 1, m + 1];
            for (var i = 1; i < n + 1; i++)
            {
                for (var k = 1; k < m + 1; k++)
                {
                    H[i, k] = h[i, k] * V[i];
                }
            }

            for (var i = 1; i < n + 1; i++)
            {
                for (var k = 1; k < m + 1; k++)
                {
                    H[i, k] = h[i, k] * V[i];
                }
            }

            var max = int.MinValue;
            for (var i = 1; i < n + 1; i++)
            {
                for (var k = 1; k < m + 1; k++)
                {
                    if (H[i, k] > max)
                        max = H[i, k];
                }
            }
            
            for (var i = 1; i < n + 1; i++)
            {
                for (var k = 1; k < m + 1; k++)
                {
                    H[i, k] = H[i, k] * (-1) + max;
                }
            }

            int[] u = new int[n + 1];
            int[] v = new int[m + 1];
            int[] p = new int[m + 1];
            int[] way = new int[m+1];

            for (int i = 1; i <= n; ++i)
            {
                p[0] = i;
                int j0 = 0;
                int[] minV = new int[m + 1];
                Fill<int>(minV, int.MaxValue);
                bool[] used = new bool[m + 1];
                do
                {
                    used[j0] = true;
                    int i0 = p[j0];
                    int delta = int.MaxValue;
                    int j1 = 0;
                    for (int j = 1; j <= m; ++j)
                    {
                        if (!used[j])
                        {
                            int cur = H[i0, j] - u[i0] - v[j];
                            if (cur < minV[j])
                            {
                                minV[j] = cur;
                                way[j] = j0;
                            }

                            if (minV[j] < delta)
                            {
                                delta = minV[j];
                                j1 = j;
                            }
                        }
                    }

                    for (int j = 0; j <= m; ++j)
                    {
                        if (used[j])
                        {
                            u[p[j]] += delta;
                            v[j] -= delta;
                        }
                        else minV[j] -= delta;
                    }

                    j0 = j1;
                } while (p[j0] != 0);

                do
                {

                    int j1 = way[j0];
                    p[j0] = p[j1];
                    j0 = j1;
                } while (j0 != 0);
            }

            int[] ans = new int[n + 1];
            for (int j = 1; j <= m; j++)
            {
                ans[p[j]] = j;
            }

            var res = new int[n + 1, m + 1];
            for (var i = 1; i < n + 1; i++)
            {
                for (var k = 1; k < m + 1; k++)
                {
                    if (ans[i] == k)
                    {
                        res[i, k] = V[i];
                    }
                }
            }
            
            StreamWriter output = new StreamWriter("out.txt");
            for (var i = 1; i < n+1; i++)
            {
                for (int j = 1; j < m+1; j++)
                {
                    output.Write("{0} ", res[i, j]);
                }

                output.Write(Environment.NewLine);
            }
            
            output.Close();
            file.Close();
        }
        
        public static void Fill<T>(T[] px, T value)
        {
            for (var k = 0; k < px.Length; k++)
            {
                px[k] = value;
            }
        }
    }
}