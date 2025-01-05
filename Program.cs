using ProductivityAssistant.Services;
using Microsoft.Extensions.Configuration;

class Program
{
    static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("Error: Missing argument. Use 'block' or 'unblock'.");
            return;
        }

        string action = args[0].ToLower();

        // Load settings from appsettings.json
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        string hostsFilePath = configuration["HostsFilePath"] ?? throw new ArgumentNullException("HostsFilePath is not defined in settings.json.");
        string redirectIP = configuration["RedirectIP"] ?? "127.0.0.1"; // Default fallback

        var websitesToBlock = configuration.GetSection("WebsitesToBlock").Get<List<string>>();

        var websiteBlocker = new WebsiteBlockerService(hostsFilePath, redirectIP);

        if (websitesToBlock?.Count > 0)
        {
            switch (action)
            {
                case "block":
                    Console.WriteLine("Blocking websites...");
                    websiteBlocker.BlockWebsites(websitesToBlock);
                    break;

                case "unblock":
                    Console.WriteLine("Unblocking websites...");
                    websiteBlocker.UnblockWebsites(websitesToBlock);
                    break;

                default:
                    Console.WriteLine("Invalid argument. Use 'block' or 'unblock'.");
                    break;
            }
        }
        else { Console.WriteLine("No websites to block/unblock found."); }
    }
}
