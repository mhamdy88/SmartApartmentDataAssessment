using Microsoft.Extensions.Configuration;
using Nest;
using Newtonsoft.Json;
using SmartApartmentData.Framework.ElasticSearch.Documents;
using SmartApartmentData.Framework.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SmartApartmentData.IndexClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Do you want to start uploading process? [Yes/No]");
            if (Console.ReadLine().ToLower() == "yes")
            {
                var settings = Config.GetElasticSearchConfigs();
                var connectionsetting = new ConnectionSettings(new Uri(settings.URL));
                connectionsetting.BasicAuthentication(settings.Username, settings.PWD);
                var indexService = new IndexService(new ElasticClient(connectionsetting));

                Console.Write("Cleaning up old indexes if any... ");
                indexService.CleanUpIndex(new string[] { "properties", "propertymanagement" });
                Console.WriteLine("Done.");



                Console.Write("Creating new index [properties]... ");
                indexService.CreateIndex<PropertyDocument>("properties", "smartapartmentdata");
                Console.WriteLine("Done.");

                Console.Write("Creating new index [propertymanagement]... ");
                indexService.CreateIndex<PropertyDocument>("propertymanagement", "smartapartmentdata");
                Console.WriteLine("Done.");

                Console.Write("Loading Json objects from files... ");
                var propertiesJsonObject = ReadJsonObjectsFromFile<PropertyRoot>("properties.json").Select(x => x.property).ToList();
                var propertyManagementJsonObject = ReadJsonObjectsFromFile<PropertyManagementRoot>("mgmt.json").Select(x => x.mgmt).ToList();
                Console.WriteLine("Done.");

                Console.Write("Indexing documents into ElasticSearch...");
                indexService.IndexDocumnets(propertiesJsonObject, "properties");
                indexService.IndexDocumnets(propertyManagementJsonObject, "propertymanagement");
                Console.WriteLine("All done!");
            }
            Console.WriteLine("Goodbye");
        }
        private static List<T> ReadJsonObjectsFromFile<T>(string fileName)
        {
            var jsonString = File.ReadAllText(fileName);
            return JsonConvert.DeserializeObject<List<T>>(jsonString);
        }
    }
}
