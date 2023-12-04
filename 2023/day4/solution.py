import re
from collections import Counter

sol1 = 0
sol2 = Counter()

def process_cards(card):
    return int(card.strip())

def get_game_points(line):
    parts = line.split("|")
    cards1 = set(map(process_cards, re.findall(r'\d+', parts[0])[1::]))
    cards2 = set(map(process_cards, re.findall(r'\d+', parts[1])))
    count_matches = len(cards1.intersection(cards2))
    return count_matches
     
def get_num_copies(line, idx):
    winners = get_game_points(line)
    sol2[idx] += 1 # Add 1 original
    for n in range(1,winners+1):
        sol2[idx+n] += sol2[idx] # Add a new ticket to the winning tickets

    
    pass
with open("test.txt","r") as file:
    lines = file.readlines()
    for idx,line in enumerate(lines):
        sol1 += int(2**(get_game_points(line)-1))
        get_num_copies(line,idx)

print(sol1)
print(sol2.total())
