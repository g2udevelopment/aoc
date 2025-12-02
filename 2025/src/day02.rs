use std::fs::read_to_string;
use itertools::Itertools;

fn parse_line(line: &str) -> Vec<(u64,u64)> {
    line.trim()
        .split(',')
        .filter_map(|s| {
            let mut nums = s.split('-').filter_map(|n| n.parse().ok());
            Some((nums.next()?,nums.next()?))
        })
        .collect()
}

fn invalid_id2(num: u64) -> bool {
    let str_num = num.to_string();
    let str_len = str_num.len();
    let chunks_equal = 
    &str_num
    .chars()
    .chunks(2)
    .into_iter()
    .map(|c| c.collect::<String>())
    .all_equal();
    false
}

fn invalid_id(num: u64) -> bool {
    let str_num = num.to_string();
    let str_len = str_num.len();
    
    if str_len % 2 != 0 {return false;}

    let (part1, part2) = str_num.split_at(str_len/2);
    part1 == part2
}

fn sum_invalid_ids((from,to): (u64,u64)) -> u64 {
    (from..=to).filter(|x| invalid_id(*x)).sum()
}

pub fn solve() -> std::io::Result<()> {
    let line = read_to_string("input-day2.txt")?;
    let parsed_line = parse_line(&line);

    let part1: u64 = parsed_line.iter().map(|item| sum_invalid_ids(*item)).sum();
    println!("Solution day2 part1: {part1}");


    Ok(())
}

#[cfg(test)]
mod tests {
    use super::*;

    #[test]
    fn parse_simple_line() {
        let result = parse_line("11-22");
        assert_eq!(result, vec![(11,22)]);
    }

    #[test]
    fn parse_multi_line() {
        let result = parse_line("11-22,1000-2000");
        assert_eq!(result, vec![(11,22),(1000,2000)]);
    }

    #[test]
    fn same_parts() {
        let result = invalid_id(11);
        assert_eq!(result, true);
    }

    #[test]
    fn same_parts_1() {
        let result = invalid_id(1010);
        assert_eq!(result, true);
    }

    #[test]
    fn not_same_parts_1() {
        let result = invalid_id(1011);
        assert_eq!(result, false);
    }

    #[test]
    fn not_same_parts_2() {
        let result = invalid_id(111);
        assert_eq!(result, false);
    }

    #[test]
    fn sum_invalid_1() {
        let result = sum_invalid_ids((11,22));
        assert_eq!(result, 33);
    }

    #[test]
    fn sum_invalid_2() {
        let result = sum_invalid_ids((38593856,38593862));
        assert_eq!(result, 38593859);
    }

     #[test]
    fn sum_invalid_3() {
        let result = sum_invalid_ids((1,19));
        assert_eq!(result, 11);
    }

    

    
}