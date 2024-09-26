// See https://aka.ms/new-console-template for more information

using System.Text.RegularExpressions;


Console.WriteLine("Exercise2_1_1\n");

var lines = ReadFile("data.csv");

Console.WriteLine("Longest title: {0}", LongestTitle(lines));

foreach (var (id, count) in ExtractWordCounts(lines))
{
    Console.WriteLine($"Id: {id}, Count: {count}");
}

var words = ExtractWords(lines);
var outputFile = "words.csv";
WriteWordsToFile(outputFile, words);
Console.WriteLine("Words written to file {0}", outputFile);

static string LongestTitle(List<(int, string?)> lines)
{
    string longestTitle = "";
    foreach (var (_, line) in lines)
    {
        if (longestTitle.Length < line.Length)
            longestTitle = line;
    }
    return longestTitle;
}

static List<(int, string?)> ReadFile(string filename)
{
    Regex rgx = new Regex("([0-9]+),\"([^\"]+)\"");
    var result = new List<(int, string?)>();
    try
    {
        using (var reader = File.OpenText(filename))
        {
            while (!reader.EndOfStream)
            {
                var line = Parse(reader.ReadLine(), rgx);
                result.Add(line);
            }
        }
    }
    catch (Exception e)
    {
        Console.WriteLine("Error reading file:");
        Console.WriteLine(e.Message);
    }
    return result;
}

static List<(int, List<string>)> ExtractWords(List<(int, string?)> lines)
{
    var words = new List<(int, List<string>)>();
    foreach (var (id, line) in lines)
    {
        if (!string.IsNullOrEmpty(line))
        {
            words.Add((id, SplitWords(line)));
        }
    }
    return words;
}

static List<(int, int)> ExtractWordCounts(List<(int, string?)> lines)
{
    var wordCounts = new List<(int, int)>();
    foreach (var (id, line) in lines)
    {
        var wordsInLine = SplitWords(line);
        foreach (var word in wordsInLine)
        {
            wordCounts.Add((id, wordsInLine.Count));
        }
    }
    return wordCounts;
}



static List<string> SplitWords(string line)
{
    char[] delimiterChars = { ' ', ',', '.', ':', '\t', '(', ')', '-', '?', '/', '<', '>', '!', '=' };
    var words = new List<string>();
    foreach (var word in line.Split(delimiterChars, StringSplitOptions.RemoveEmptyEntries))
    {
        words.Add(word);
    }
    return words;
}

static void WriteWordsToFile(string outputFile, List<(int, List<string>)> words)
{
    using var writer = new StreamWriter(File.OpenWrite(outputFile));
    foreach (var line in words)
    {
        foreach (var word in line.Item2)
        {
            writer.WriteLine("{0},\"{1}\"", line.Item1, word.ToLower());
        }
    }
}

static (int, string?) Parse(string line, Regex regex)
{
    var matches = regex.Matches(line);
    if (matches.Count > 0)
    {
        // TODO handle parsing errors 
        return (int.Parse(matches[0].Groups[1].Value), matches[0].Groups[2].Value);
    }
    return (-1, null);
}