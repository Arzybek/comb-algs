* Assignment problem
* The industrial corporation plans to release n new types of products. Production can be organized at m enterprises owned by the corporation (n <m), each with no more than one type of product. For each i-th type of product and j-th enterprise, the following are known:
  pij - production costs per unit of output,
   qij - sales costs of a unit of production,
   si - sales value of a unit of i-product,
   vi is the volume of output of i-products.
Production volumes are limited to 1,000,000.
   It is required to distribute the production of products in the best possible way among enterprises, considering that any enterprise can cope with the release of any type of product. Propose a mathematical model of this problem as an optimization problem on a graph
* File input (in.txt):
* The first line contains two numbers n and m. Then there are n lines with m numbers in each - the matrix pij, followed by n more lines with m numbers in each - matrix qij, then two lines of n numbers each - vectors S and V.
* Output file (out.txt):
* Matrix n * m - distribution of production by enterprises (n lines by m numbers)