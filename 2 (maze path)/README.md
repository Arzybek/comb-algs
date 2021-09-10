* In a given maze, it searches for a path between two given nodes.
* Using depth-first search
* Source data file (in.txt): 
* Labyrinth.
      N is the number of lines in the maze.
      M is the number of columns in the maze.
      Further, the maze itself is located line by line. Then the coordinates of the source and target are located in the X Y format, where X is the row number, Y is the column number. Maze encoding: 1 - ban; 0 - free.
* Result file (out.txt):
* Route in the labyrinth.
      If there is no path in the file, "N" is written, if there is a path "Y" and then the entire path. The route must begin with source coordinates and end with target coordinates. Each step is written on a new line in the X Y format, where X is the row number, Y is the column number.