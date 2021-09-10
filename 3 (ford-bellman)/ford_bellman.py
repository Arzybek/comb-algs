import sys

inp = open("in.txt", "r")
out = open("out.txt", 'w')

n = int(inp.readline())
last = []
costs = {}
last.append([])
for i in range(1, n + 1):
    str_t = inp.readline()
    l = str_t.split(" ")
    tmpL = l[:-1]
    if len(tmpL) == 0:
        last.append(tmpL)
    else:
        listNodes = []
        for k in range(0, len(tmpL) - 1, 2):
            v = int(tmpL[k])
            c = int(tmpL[k + 1])
            listNodes.append(v)
            costs[(v, i)] = c
        last.append(listNodes)

inst = int(inp.readline())
dest = int(inp.readline())
inp.close()

inf = -sys.maxsize-1
D = []
pred = []

for i in range(0, n + 1):
    D.append(inf)
    pred.append(inf)

for i in range(1, n + 1):
    if (inst in last[i]):
        D[i] = costs[(inst, i)]
    pred[i] = inst

for iter in range(2, n - 1, 1):
    for v in range(1, n + 1, 1):
        if v == inst:
            pass
        for edge in last[v]:
            if edge == inst:
                pass
            if D[edge] * costs[(edge, v)] > D[v]:
                D[v] = D[edge] * costs[(edge, v)]
                pred[v] = edge

stack = []
if (D[dest] != inf):
    out.write("Y\n")
    stack.append(dest)
    u = pred[dest]
    while (u != inst):
        stack.append(u)
        u = pred[u]
    stack.append(inst)
    stack.reverse()
    res = " ".join(str(x) for x in stack)
    out.write(res)
    out.write("\n")
    out.write(str(D[dest]))
else:
    out.write("N\n")

out.close()


