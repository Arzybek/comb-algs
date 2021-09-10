from collections import deque

# inp = open('in.txt', 'r')
# out = open('out.txt', 'w')

n = int(input())
input_lines = [[""]]
graph = [[""]]
for i in range(n):
    string = input().split(" ")[:-1]  # 2
    input_lines.append(string)
    graph.append([])

for i in range(1, n + 1):
    for one in input_lines[i]:
        graph[i].append(one)
        if (str(i) not in graph[int(one)]):
            graph[int(one)].append(str(i))

visited = dict()
q = deque()
colors = []

for i in range(n + 1):
    visited[i] = False
    colors.append(False)

q.append(1)
visited[1] = True
colors[1] = True
broken = False
while len(q) != 0:
    node = q.popleft()
    sub = graph[node]
    color = colors[node]
    for child in sub:
        if visited[int(child)] == False:
            flag = not color
            q.append(int(child))
            visited[int(child)] = True
            colors[int(child)] = flag
        else:
            if colors[node] == colors[int(child)]:
                broken = True
result = ""

if broken:
    print("-1")
else:
    for i in colors[1:]:
        if (i):
            result += "0"
        else:
            result += "1"
    print(result)
