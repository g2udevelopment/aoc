import re
from collections import defaultdict

def get_adjacent_chars(grid, row, col):
    height = len(grid)
    width = len(grid[0].strip())
    chars = []
    dir = [(1,0),(-1,0),(1,1),(0,-1),(-1,-1),(0,1),(-1,1),(1,-1)]
    for dr,dc in dir:
        r = row + dr
        c = col + dc
        if 0 <= r < height and 0 <= c < width:
            chars.append((grid[r][c], r, c))
    return chars

def is_adjacent_sym(adj_chars):
    for adj_char,_,_ in adj_chars:
        if not adj_char.isdigit() and not adj_char == '.':
            return True

def get_adjacent_gears(adj_chars):
    stars = []
    for adj_char,r,c in adj_chars:
        if adj_char == "*":
            stars.append((r,c))
    return stars

def solution1(grid,line, row):
    sum = 0
    matches = re.finditer(r"\d+", line)
    for match in matches:
        number = match.group(0)
        start = match.start()
        len_num = len(number)
        for idx in range(len_num):
            adj_chars = get_adjacent_chars(grid,row, start+idx)
            if is_adjacent_sym(adj_chars):
                sum += int(number)
                break

    return sum

def solution2(grid,line, row, stars_total):
    matches = re.finditer(r"\d+", line)
    for match in matches:
        number = match.group(0)
        start = match.start()
        len_num = len(number)
        num_added = False
        for idx in range(len_num):
            adj_chars = get_adjacent_chars(grid,row, start+idx)
            stars = get_adjacent_gears(adj_chars)
            for r,c in stars:
                if not num_added:
                    stars_total[(r,c)].append(int(number))
                    num_added = True
    
# Count all the stars that have more then one number
def sol2_sum(stars_total):
    sum = 0
    for s in stars_total.values():
        if len(s) > 1:
            res = 1
            for num in s:
                res *= num
            sum += res
    return sum

sol1 = 0
sol2 = 0
row = 0
stars_total = defaultdict(list) # Store the numbers that are adj on the star position
with open("input.txt","r") as file:
    grid = file.readlines()
    for line in grid:
        sol1 += solution1(grid, line.strip(), row)
        solution2(grid,line.strip(),row, stars_total)
        row+=1

print(sol1)
print(sol2_sum(stars_total))
