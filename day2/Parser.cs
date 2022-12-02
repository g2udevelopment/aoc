public enum RPSToken {
    Rock,
    Paper,
    Scissor,
}

public enum ResultToken {
    Draw,
    Win,
    Lose,
}

public class RPSParser {
    public RPSToken parseSymbol(string symbol) {
        return symbol switch
        {
            "A" or "X" => RPSToken.Rock,
            "B" or "Y" => RPSToken.Paper,
            "C" or "Z" => RPSToken.Scissor,
            _ => throw new ArgumentException()
        };
    }

    public ResultToken parseResult(string symbol) {
        return symbol switch
        {
            "X" => ResultToken.Lose,
            "Y" => ResultToken.Draw,
            "Z" => ResultToken.Win,
            _ => throw new ArgumentException()
        };
    }


    public int GameScore((RPSToken opp, RPSToken me) line) {
        return toScore(line) + toIndividualScore(line.me);
    }
    public int toScore((RPSToken opp, RPSToken me) line) {
        if (line.me == line.opp) return 3;
        return line switch {
            (RPSToken.Scissor, RPSToken.Rock) => 6,
            (RPSToken.Paper, RPSToken.Scissor) => 6,
            (RPSToken.Rock, RPSToken.Paper) => 6,
            _ => 0 
        };
    }

    public int toIndividualScore(RPSToken token) {
        return token switch {
            RPSToken.Rock => 1,
            RPSToken.Paper => 2,
            RPSToken.Scissor => 3,
            _ => throw new ArgumentException()
        };
    }

    public RPSToken chooseToGetResult(RPSToken opp, ResultToken result) {
        if (result == ResultToken.Draw) return opp;
        if (result == ResultToken.Win) {
            return opp switch {
                RPSToken.Paper => RPSToken.Scissor,
                RPSToken.Scissor => RPSToken.Rock,
                RPSToken.Rock => RPSToken.Paper,
                _ => throw new ArgumentException()
            };
        } else {
            return opp switch {
                RPSToken.Paper => RPSToken.Rock,
                RPSToken.Scissor => RPSToken.Paper,
                RPSToken.Rock => RPSToken.Scissor,
                _ => throw new ArgumentException()
            };
        }
    }
}