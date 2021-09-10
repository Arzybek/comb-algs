* Find the largest matching in a bipartite graph
* reduction to the maximum flow problem and the use of the Ford-Fulkerson algorithm.
* File input (in.txt):
* Bipartite graph G = (X, Y, E), k = | X |, l = | Y |, given by the X-array of adjacencies.
  X-array of adjacencies: just like an array of adjacencies, it only lists those adjacent to vertices x from X. For an isolated vertex, the index in the array is 0.
  In the first line of the file, the numbers k l. In the second size of the array. Next is the adjacency array (no more than 10 numbers in one line).
  The last element of the array is 32767.
* Output file (out.txt):
* An array X PAIR of length k (X PAIR [xi] = yj if {xi, yj} is included in the matching, otherwise X PAIR [xi] = 0).