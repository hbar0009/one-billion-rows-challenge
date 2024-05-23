var watch = new System.Diagnostics.Stopwatch();

watch.Start();

// todo: investigate ConcurrentDictionary when we get into concurrency
var stations = new Dictionary<string, Station>();

// read each line of the file and process its information
using (var file = new StreamReader("C:\\Repos\\one-billion-rows-challenge\\data\\measurements.txt"))
{
    string line;

    while ((line = file.ReadLine()) != null)
    {
        var data = line.Split(';');
        var name = data[0];
        var temperature = float.Parse(data[1]);

        if (!stations.ContainsKey(name))
        {
            var station = new Station(name, temperature);
            stations.Add(name, station);
        }
        else
        {
            stations[name].Temperatures.Add(temperature);
        }
    }
}

// calculate each min, max, and average
foreach (var station in stations.Values)
{
    station.Min = MathF.Round(station.Temperatures.Min(), 1, MidpointRounding.ToPositiveInfinity);
    station.Max = MathF.Round(station.Temperatures.Max(), 1, MidpointRounding.ToPositiveInfinity);
    station.Average = MathF.Round(station.Temperatures.Average(), 1, MidpointRounding.ToPositiveInfinity); 
}

// TODO: need to sort by station name

// print the results to stdout
Console.Write('{');
foreach (var station in stations.Values)
{
    Console.Write($"{station.Name}={station.Min}/{station.Average}/{station.Max}, ");
}
Console.Write('}');

watch.Stop();

Console.WriteLine($"Execution Time: {watch.Elapsed}");


public class Station
{
    public string Name { get; set; }
    public List<float> Temperatures { get; set; }
    public float Min {  get; set; }
    public float Max { get; set; }
    public float Average { get; set; }

    public Station(string name, float temperature)
    {
        Name = name;
        Temperatures = [temperature];
    }
}
