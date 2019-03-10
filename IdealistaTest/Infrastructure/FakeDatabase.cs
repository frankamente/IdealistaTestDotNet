using IdealistaTest.Infrastructure.Entities;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using WebGrease.Css.Extensions;

namespace IdealistaTest.Infrastructure
{
    public class FakeDatabase
    {
        private static volatile FakeDatabase instance;

        private IEnumerable<Ad> infrastructureAds;
        private IEnumerable<Picture> infrastructurePictures;

        private static IList<Domain.Entities.Ad> domainAds;

        private static readonly object InstanceLocker = new object();
        protected FakeDatabase()
        {
            infrastructureAds = new List<Ad>();
            infrastructurePictures = new List<Picture>();
            domainAds = new List<Domain.Entities.Ad>();
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

        protected void RestartInstance()
        {
            instance = new FakeDatabase();
        }

        public void InitializeDatabase(string adJsonFilename, string pictureJsonFilename)
        {
            if (adJsonFilename == null)
            {
                throw new FileLoadException("Error loading Ads Json");
            }
            if (pictureJsonFilename == null)
            {
                throw new FileLoadException("Error loading Picture Json");
            }
            InitializeInfrastructureEntities(adJsonFilename, pictureJsonFilename);
            InitializeDomainAds();
        }

        private void InitializeDomainAds()
        {
            infrastructureAds.ForEach(infrastructureAd =>
            {
                domainAds.Add(new Domain.Entities.Ad(infrastructureAd, infrastructurePictures));
            });
        }

        private void InitializeInfrastructureEntities(string adJsonFilename, string pictureJsonFilename)
        {
            infrastructureAds = JsonConvert.DeserializeObject<HashSet<Ad>>(File.ReadAllText(adJsonFilename, Encoding.Default));
            infrastructurePictures = JsonConvert.DeserializeObject<HashSet<Picture>>(File.ReadAllText(pictureJsonFilename, Encoding.Default));
        }

        public IEnumerable<Domain.Entities.Ad> GetOrderedAds()
        {
            return domainAds.OrderByDescending(x=>x.Mark);
        }
    }
}