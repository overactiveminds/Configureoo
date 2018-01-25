using System;
using Microsoft.Extensions.Configuration;
using Configureoo.JsonConfigurationProvider;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            SetDemoKeyDontEverDoThis();

            IConfigurationRoot configuration = new ConfigurationBuilder()
                .AddConfigureooJsonFile("appsettings.json")
                .Build();

            string configValue = configuration["someSensitiveKey"];

            Console.WriteLine(configValue);
            Console.ReadLine();
        }

        // Never do this, this is for demo purposes only!!!!
        // You should set your keys as environment variables on deploy.
        static void SetDemoKeyDontEverDoThis()
        {
            Environment.SetEnvironmentVariable("CONFIGUREOO_default", "g79YEKpDKla5zUvX"); 
        }
    }
}
