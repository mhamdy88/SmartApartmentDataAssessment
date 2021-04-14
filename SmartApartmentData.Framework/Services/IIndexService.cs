using System;
using System.Collections.Generic;
using System.Text;

namespace SmartApartmentData.Framework.Services
{
    public interface IIndexService
    {
        void CleanUpIndex(string[] indicesName);
        void CreateIndex<T>(string name, string alias) where T : class;
        void IndexDocumnets<T>(List<T> documents, string index) where T : class;
    }
}
