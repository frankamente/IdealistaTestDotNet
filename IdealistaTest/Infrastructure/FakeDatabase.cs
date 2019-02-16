using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using IdealistaTest.Infrastructure.Entities;
using Newtonsoft.Json;

namespace IdealistaTest.Infrastructure
{
    public class FakeDatabase
    {
        private static volatile FakeDatabase instance;

        private static readonly object InstanceLocker = new object();
        private FakeDatabase()
        {

        }

        public static FakeDatabase Instance()
        {
            if (instance != null) return instance;
            lock (InstanceLocker)
            {
                if (instance == null)
                {
                    instance = new FakeDatabase();
                }
            }
            return instance;
        }

        public  void InitializeDatabase()
        {
            string adJsonFilename =
                System.Web.Hosting.HostingEnvironment.MapPath(@"~/Infrastructure/jsonPopulateFiles/ads.json");
            var ads = JsonConvert.DeserializeObject<List<Ad>>(File.ReadAllText(adJsonFilename));
            string pictureJsonFilename =
                System.Web.Hosting.HostingEnvironment.MapPath(@"~/Infrastructure/jsonPopulateFiles/pictures.json");
            var pictures = JsonConvert.DeserializeObject<List<Picture>>(File.ReadAllText(pictureJsonFilename));
        }
    }
}