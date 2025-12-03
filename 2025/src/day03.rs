use itertools::Itertools;
use std::fs::read_to_string;

fn max_batteries(batteries: &str, len: usize) -> u64 {
    let combinations: u64 = batteries
        .chars()
        .combinations(len)
        .map(|x| x.iter().collect::<String>().parse::<u64>().unwrap())
        .max()
        .unwrap();
    combinations
}

fn largest_subsequence(s: &str, k: usize) -> String {
    let n = s.len();
    let to_remove = n - k;
    let mut stack: Vec<char> = Vec::new();
    let mut removed = 0;
    
    for ch in s.chars() {
        // Remove smaller digits if we can afford to
        while !stack.is_empty() 
            && removed < to_remove 
            && stack.last().unwrap() < &ch 
        {
            stack.pop();
            removed += 1;
        }
        stack.push(ch);
    }
    
    // Keep only k elements
    stack.into_iter().take(k).collect()
}

pub fn solve() -> std::io::Result<()> {
    let text = read_to_string("input-day3.txt")?;
    let lines = text.lines();
    let lines2 = lines.clone();
    let part1: u64 = lines.map(|l| max_batteries(l, 2)).sum();
    println!("Day3 part1: {part1}");

    let part2: u64 = lines2.map(|l| largest_subsequence(l, 12).parse::<u64>().unwrap()).sum();
    println!("Day3 part2: {part2}");

    Ok(())
}

#[cfg(test)]
mod tests {
    use super::*;

    #[test]
    fn test() {
        let result = max_batteries("9118117", 2);
        assert_eq!(result, 98);
    }

    #[test]
    fn test_2() {
        let result = largest_subsequence("818181911112111", 12);
        assert_eq!(result, "888911112111");
    }
}
