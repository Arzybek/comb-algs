inp = open('in.txt', 'r')
out = open('out.txt', 'w')

n = int(inp.readline())
m = int(inp.readline())

lab = [[]]
for i in range(m + 1):
    lab[0].append("1")
for i in range(1, n + 1):
    str_arr = inp.readline().split(" ")
    lab.append([])
    lab[i].append("1")
    lab[i].extend(str_arr)

inst = inp.readline().split(" ")
dest = inp.readline().split(" ")
inp.close()
inst = (int(inst[0]), int(inst[1]))
dest = (int(dest[0]), int(dest[1]))

visited = dict()
stack = list()
stack.append((inst, 0))
history = dict()
flag = False

while len(stack) != 0:
    point, last = stack.pop()
    if point[0] < 1 or point[0] >= n or point[1] < 1 or point[1] >= m:
        continue
    if lab[point[0]][point[1]] == "1" or point in visited:
        continue
    visited[point] = True
    history[point] = last
    if point == dest:
        flag = True
        break
    for y in [0, 1, -1]:  # [-1, 1, 0]:
        for x in [1, 0, -1]:  # [-1, 0, 1]:
            if y != 0 and x != 0:
                continue
            stack.append(((point[0] + y, point[1] + x), point))

if flag:
    result = list()
    result.append(dest)
    last = dest
    while True:
        if (last == inst):
            break
        result.append(history[last])
        last = history[last]
    result.reverse()
    out.write("Y\n")
    for tup in result:
        out.write("{} {}".format(tup[0], tup[1]))
        out.write("\n")
else:
    out.write("N\n")

out.close()
