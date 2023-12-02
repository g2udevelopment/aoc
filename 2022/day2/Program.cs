var input = new Input("input.txt");
var parser = new RPSParser();
var lines = input.toTuple();
var solution1 = lines
.Select(line => (parser.parseSymbol(line.Item1), parser.parseSymbol(line.Item2)))
.Select(token => parser.GameScore(token))
.Sum();

System.Console.WriteLine($"Solution1: {solution1}");

var solution2 = lines
.Select(line => (parser.parseSymbol(line.Item1), parser.parseResult(line.Item2)))
.Select(token => (token.Item1, parser.chooseToGetResult(token.Item1, token.Item2)))
.Select(token => parser.GameScore(token))
.Sum();

System.Console.WriteLine($"Solution2: {solution2}");
