using ConsoleApp1;
using Newtonsoft.Json;
using System.Net;

Console.BackgroundColor = ConsoleColor.Gray;
Console.ForegroundColor = ConsoleColor.Red;
do
{
    using (var client = new HttpClient(new HttpClientHandler { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate }))
    {
        Console.Write("----\nType a city: ");
        string baseUri = "https://api.openweathermap.org/data/2.5/weather?q=";
        string cityName = $"{Console.ReadLine()}&units=metric&APPID=";
        if (cityName.Contains(' ')) cityName = cityName.Replace(" ", "");
        string key = "2fd1aaaa85e1d7d52c0b96d4920346a4";
        string url = baseUri + cityName + key;
        client.BaseAddress = new Uri(url);
        HttpResponseMessage response = client.GetAsync(url).Result;
        string result = response.Content.ReadAsStringAsync().Result;
        var weather = JsonConvert.DeserializeObject<WeatherCorrd>(result);
        if (response.StatusCode == HttpStatusCode.NotFound)
        {
            Console.WriteLine("" +
                "\n  -The city not found" +
                "\n  -Error: 404 .." +
                "\n\n " +
                "");
        }
        else
        {
            var todaysWeather = $"" +
                $"\n  Temprature in {weather.Name} is: {(int)weather.Main.Temp}° now {DateTime.Now.ToShortTimeString()} " +
                $"\n  It is {weather.Weather.FirstOrDefault().Main.ToLower()}";
            Console.WriteLine(todaysWeather);
        }
    }
} while (true);
