using Nest;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {



            var settings = new ConnectionSettings(new Uri("http://localhost:9200"));
            //.DefaultMappingFor<propertyNode>(p => p.IndexName("smartapartmentdata"));

            var client = new ElasticClient(settings);

            var xxx = client.Search<object>(s => s.Index("smartapartmentdata").Size(600).Query(q => q.QueryString(qs => qs
                 .Query("stones into rocks.")
                 .Analyzer("snowball")
               )));





            client.Indices.Delete("properties");
            client.Indices.Delete("propertymanagement");


            var createPropertyIndexResponse = client.Indices.Create("properties",
                x => x
                .Map<Property>(p => p.AutoMap())
                .Aliases(p => p.Alias("smartapartmentdata"))
                .Settings(p => p.Analysis(a => a.Analyzers(z => z.Snowball("snowball", sa => sa.Language(SnowballLanguage.English)))))
                );
            var createPropertyManagementIndexResponse = client.Indices.Create("propertymanagement",
                x => x
                .Map<PropertyManagement>(p => p.AutoMap())
                .Aliases(p => p.Alias("smartapartmentdata"))
                .Settings(p => p.Analysis(a => a.Analyzers(z => z.Snowball("snowball", sa => sa.Language(SnowballLanguage.English)))))
                );
            //client.Map<Property>(p => p.Index("my-index").AutoMap());
            //client.Map<PropertyManagement>(p => p.Index("my-index").IncludeTypeName(true).AutoMap());




            var propertiesJsonString = File.ReadAllText("properties.json");
            var propertiesJsonObject = JsonConvert.DeserializeObject<List<PropertyRoot>>(propertiesJsonString).Select(x => x.property);

            var propertyManagementJsonString = File.ReadAllText("mgmt.json");
            var propertyManagementJsonObject = JsonConvert.DeserializeObject<List<PropertyManagementRoot>>(propertyManagementJsonString).Select(x => x.mgmt);

            var response = client.IndexMany<Property>(propertiesJsonObject, "properties");
            response = client.IndexMany<PropertyManagement>(propertyManagementJsonObject, "propertymanagement");

            

            Console.WriteLine("Hello World!");
            Console.ReadLine();
        }
    }
    public class PropertyRoot
    {
        public Property property { get; set; }
    }
    public class Property
    {
        public int propertyID { get; set; }
        [Text(Analyzer = "snowball", SearchAnalyzer = "snowball")]
        public string name { get; set; }
        [Text(Analyzer = "snowball", SearchAnalyzer = "snowball")]
        public string formerName { get; set; }
        [Text(Analyzer = "snowball", SearchAnalyzer = "snowball")]
        public string streetAddress { get; set; }
        [Text(Analyzer = "snowball", SearchAnalyzer = "snowball")]
        public string city { get; set; }
        [Text(Analyzer = "snowball", SearchAnalyzer = "snowball")]
        public string market { get; set; }
        [Text(Analyzer = "snowball", SearchAnalyzer = "snowball")]
        public string state { get; set; }
        public float lat { get; set; }
        public float lng { get; set; }
    }
    public class PropertyManagementRoot
    {
        public PropertyManagement mgmt { get; set; }
    }
    public class PropertyManagement
    {
        public int mgmtID { get; set; }
        [Text(Analyzer = "snowball", SearchAnalyzer = "snowball")]
        public string name { get; set; }
        [Text(Analyzer = "snowball", SearchAnalyzer = "snowball")]
        public string market { get; set; }
        [Text(Analyzer = "snowball", SearchAnalyzer = "snowball")]
        public string state { get; set; }

    }
}



//{​
//    "mgmt": {​
//      "mgmtID": 27918, //int​
//      "name": "Essex Property Trust AKA Essex Apartment Homes", //string​
//      "market": "San Francisco", //string​
//      "state": "CA" //string​
//    }​
//  }

//{​
//    "property": {​
//      "propertyID": 70034, //int​
//      "name": "Sage at 1825 Place", //string​
//      "formerName": "1825 Place 2", //string​
//      "streetAddress": "15835 Foothill Farms Loop", //string​
//      "city": "Pflugerville", //string​
//      "market": "Austin", //string​
//      "state": "TX", //string​
//      "lat": 3.044956000000000e+001, //float​
//      "lng": -9.765073000000000e+001 //float​
//    }​
//  }