using System.Diagnostics;

namespace TaskTask;

class Program
{
    static void Main(string[] args)
    {
        Stopwatch sw = Stopwatch.StartNew();
        
        List<string> urls = new List<string>()
        {
            "https://www.microsoft.com/en-us/",
            "https://www.meta.com",
            "https://www.freelancer.com"
        };
        
        RetrieveHTMLData(urls);
        Console.WriteLine($"Completed in {sw.ElapsedMilliseconds} ms.");
        
        Console.WriteLine("------------------------------------------------------------------");
        
        sw.Restart();
        
        RetrieveHTMLDataAsync(urls).Wait();
        Console.WriteLine($"Completed in {sw.ElapsedMilliseconds} ms.");

    }

    public static void RetrieveHTMLData(List<string> urls)
    {
        HttpClient client = new HttpClient();

        foreach (var item in urls)
        {
            client.GetStringAsync(item).Result.ToString();
        }
    }

    public static async Task RetrieveHTMLDataAsync(List<string> urls)
    {
        HttpClient client = new HttpClient();

        List<Task<string>> tasks = new List<Task<string>>();
        
        foreach (var item in urls)
        {
            tasks.Add(client.GetStringAsync(item));
        }
        
        await Task.WhenAll(tasks);
    }
}