* Looks for a v-w path in a network with non-negative weights for the maximum path problem.
* Using Bellman–Ford algorithm
* Source data file (in.txt): 
* Network defined by PREV [] lists.
      N is the number of vertices.
      Further, the lists of the preceding ones for each vertex are sequentially located. The list contains the number of the vertex and the weight of the arc. The list ends with 0 (WARNING! Not to be confused with zero weight). The source and target are recorded at the end of the file.
* Result file (out.txt):
* If there is no path to the results file, write "N", if there is a path - "Y" and then from a new line the entire path. The path begins with the source and ends with the goal. The nodes are separated from each other by spaces, the weight of the path is calculated as the product of the weights of all arcs included in it and is written in the third line.