using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityAssistant.Services
{
    public class WebsiteBlockerService
    {
        private readonly string _hostsFilePath;
        private readonly string _redirectIP;

        public WebsiteBlockerService(string hostsFilePath, string redirectIP = "127.0.0.1")
        {
            _hostsFilePath = hostsFilePath;
            _redirectIP = redirectIP;
        }

        public void BlockWebsites(IEnumerable<string> websites)
        {
            try
            {
                var lines = File.ReadAllLines(_hostsFilePath).ToList();

                foreach (var website in websites)
                {
                    if (!lines.Any(line => line.Contains(website)))
                    {
                        lines.Add($"{_redirectIP} {website}");
                    }
                }

                File.WriteAllLines(_hostsFilePath, lines);
                Console.WriteLine("Websites successfully blocked.");
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine("Error: Please run the application as an administrator.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        public void UnblockWebsites(IEnumerable<string> websites)
        {
            try
            {
                var lines = File.ReadAllLines(_hostsFilePath).ToList();

                lines = lines.Where(line => !websites.Any(website => line.Contains(website))).ToList();

                File.WriteAllLines(_hostsFilePath, lines);
                Console.WriteLine("Websites successfully unblocked.");
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine("Error: Please run the application as an administrator.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
