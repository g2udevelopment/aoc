import re
from functools import reduce
from operator import mul

games = []
MAX_CUBES = [{'color': 'red', 'max': 12},{'color': 'green', 'max': 13},{'color': 'blue', 'max': 14}]
games_count = []

def find_by_color(color, data):
    for elem in data:
        if elem['color'] == color:
            return elem['max']

def solution1(games):
    sum = 0;
    for game in games:
        impossible = False
        game_id = int(game['id'])
        for dice in game['plays']:
            if find_by_color(dice['color'],MAX_CUBES) < dice['number']:
                impossible = True
        if not impossible:
            sum += game_id
    return sum

def solution2(games):
    sum = 0;
    for game in games:
        min_cubes = {'red':0,'blue':0,'green':0}
        for dice in game['plays']:
            if dice['number'] > min_cubes[dice['color']]:
                min_cubes[dice['color']] = dice['number']
        sum += reduce(mul, min_cubes.values()) 
    return sum

def parse_play(play:str):
    dice = []
    matches = re.findall(r'(\d+)\s+(\w+)', play)
    for match in matches:
        dice.append({'number':int(match[0]), 'color': match[1]})
    return dice


def parse_game(line:str):
    game_match = re.search(r'Game\s+(\d+)', line)
    if game_match:
        game_id = game_match.group(1)
        dice_play = parse_play(line)
        return {'id':game_id, 'plays':dice_play}

            

with open("input.txt","r") as file:
    lines = file.readlines()
    for line in lines:
        game = parse_game(line)
        games.append(game)

    sol1 = solution1(games)
    sol2 = solution2(games)
    print("day2 part1 solution: " +str(sol1))
    print("day2 part2 solution: " +str(sol2))
