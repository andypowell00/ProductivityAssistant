using ProductivityAssistant.Services;
using Microsoft.Extensions.Configuration;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            Console.WriteLine($"Running as user: {Environment.UserName}");
                
            if (args.Length == 0)
            {
                Console.WriteLine("Error: Missing argument. Use 'block' or 'unblock'.");
                return;
            }
            Console.WriteLine("1");

            string action = args[0].ToLower();
            Console.WriteLine("2");
            // Load settings from appsettings.json
            IConfiguration configuration;
            try
            {
                configuration = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .Build();
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Error: appsettings.json file not found. Ensure it is in the same directory as the executable.");
                return;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: Failed to load configuration file. Details: {ex.Message}");
                return;
            }
            Console.WriteLine("3");
            // Read configuration values with fallback and validation
            string hostsFilePath = configuration["HostsFilePath"];
            if (string.IsNullOrEmpty(hostsFilePath))
            {
                Console.WriteLine("Error: HostsFilePath is not defined in appsettings.json.");
                return;
            }
            Console.WriteLine("4");
            string redirectIP = configuration["RedirectIP"] ?? "127.0.0.1";

            var websitesToBlock = configuration.GetSection("WebsitesToBlock").Get<List<string>>() ?? new List<string>();

            if (websitesToBlock.Count == 0)
            {
                Console.WriteLine("No websites to block/unblock found in appsettings.json.");
                return;
            }
            Console.WriteLine("5");
            var websiteBlocker = new WebsiteBlockerService(hostsFilePath, redirectIP);

            // Perform action based on argument
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
        catch (UnauthorizedAccessException)
        {
            Console.WriteLine("Error: The application requires administrator privileges to modify the hosts file.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unhandled error: {ex.Message}");
        }
    }
}
