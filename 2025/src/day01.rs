use std::fs::read_to_string;

fn parse_line(line: &str) -> Option<(&str,i32)> {
    let (dir, num) = line.split_at(1);
    let number: i32 = num.parse().ok()?;
    match dir {
        "L" => Some((dir,-number)),
        "R" => Some((dir,number)),
        _ => None,
    }
}

pub fn solve() -> std::io::Result<()> {
    let mut current = 50;
    let mut end_zero = 0;
    let mut cross_zero = 0;

    for line in read_to_string("input-day1.txt")?.lines() {
        let (dir,rotation) = parse_line(line).ok_or_else(|| {
            std::io::Error::new(std::io::ErrorKind::InvalidData, "Parse error")
        })?;

        cross_zero += (rotation / 100).abs();
        let new = (current + rotation).rem_euclid(100);

        if new != 0 && current != 0 {
            if (dir == "L" && new > current) || (dir == "R" && new < current) {
                cross_zero += 1;
            }
        }

        end_zero += (new == 0) as i32;
        current = new;
    }

    println!("Day 1 Part 1: {end_zero}");
    println!("Day 1 Part 2: {}", end_zero + cross_zero);
    Ok(())
}
