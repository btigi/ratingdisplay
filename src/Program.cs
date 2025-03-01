using System.Text;

if (args.Length != 3)
{
    Console.WriteLine("Usage: <input> <template> <output>");
    return;
}

if (!File.Exists(args[0]))
{
    Console.WriteLine("Input file does not exist");
    return;
}

if (!File.Exists(args[1]))
{
    Console.WriteLine("Template file does not exist");
    return;
}

var lines = File.ReadAllLines(args[0]);

var showname = lines.First();
var ratingLines = lines.Skip(1).ToList();

var season = string.Empty;
var newSeason = true;
var sb = new StringBuilder();
foreach (var line in ratingLines)
{
    if (newSeason)
    {
        sb.Append("<div class=\"column\">");
        sb.Append($"<div class=\"cell header\">{line}</div>");        
        newSeason = false;
        continue;
    }

    if (line == string.Empty)
    {
        sb.Append("</div>");
        newSeason = true;
        continue;
    }

    var rating = line.Split("#").First();

    var ratingTruncated = Math.Truncate(Convert.ToDouble(rating));

    sb.Append($"<div class=\"cell rating-{ratingTruncated}\">{rating}</div>");
}

var templateText = File.ReadAllText(args[1]);
templateText = templateText.Replace("{ratings}", sb.ToString());
templateText = templateText.Replace("{showname}", showname);

File.WriteAllText(args[2], templateText);