var line = File.ReadLines("input.txt").Single();

System.Console.WriteLine($"Solution1: {FindMarker(line,4)}");
System.Console.WriteLine($"Solution1: {FindMarker(line,14)}");


static int FindMarker(string line, int length)
{
    for (int i = 0; i < line.Length - length-1; i++)
    {
        if (line[i..(i+length)].Distinct().Count() == length) // Marker found
        { 
            return i + length;
        }
    }
    throw new ArgumentException("Marker not found");
}
