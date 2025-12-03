mod day01;
mod day02;
mod day03;

use std::env;

fn main() -> std::io::Result<()> {
    let args: Vec<String> = env::args().collect();

    let day = if args.len() > 1 {
        args[1].parse::<u32>().unwrap_or(1)
    } else {
        1
    };

    match day {
        1 => day01::solve()?,
        2 => day02::solve()?,
        3 => day03::solve()?,
        _ => println!("Day {} not implemented yet!", day),
    }

    Ok(())
}
