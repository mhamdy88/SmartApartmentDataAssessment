using Nest;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartApartmentData.Framework.ElasticSearch.Documents
{
    public class PropertyManagementDocument
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
