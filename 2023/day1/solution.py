# Solution to day 1, first task
def get_line_sum(line):
    digits = [c for c in line if c.isdigit()]
    return int(digits[0]+digits[len(digits)-1])

word_dict = {"one":"1", "two":"2","three":"3", "four":"4", "five":"5", "six":"6", "seven":"7", "eight":"8", "nine":"9"}
word_dict_two = {"one":"one1one", "two":"two2two","three":"three3three", "four":"four4four", "five":"five5five", "six":"six6six", "seven":"seven7seven", "eight":"eight8eight", "nine":"nine9nine"}

def replace_word_with_number(line:str):
    for k,v in word_dict_two.items():
        line = line.replace(k,v)
    return line 

sol1 = 0
sol2 = 0
with open("input.txt", "r") as file:
    lines = file.readlines()
    for line in lines:
        sol1 += get_line_sum(line)
        line = replace_word_with_number(line)
        sol2 += get_line_sum(line)

print("day1 part1 solution: " + str(sol1))
print("day1 part2 solution: " + str(sol2))
