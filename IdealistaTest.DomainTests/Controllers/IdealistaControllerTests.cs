using FluentAssertions;
using IdealistaTest.DomainTests.Infrastructure;
using IdealistaTest.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using IdealistaTest.Services;

namespace IdealistaTest.Controllers.Tests
{
    [TestClass()]
    public class IdealistaControllerTests
    {
        private FakeDatabase fakeDatabaseInstance;

        [TestInitialize]
        public void TestInitialize()
        {
            fakeDatabaseInstance = FakeDatabase.Instance();
            new FakeDatabaseExtend().RestartFakeDatabaseInstance();
            fakeDatabaseInstance.InitializeDatabase(GetAdJsonFullPath(), GetPictureJsonFullPath());
            new IdealistaService().MarkCalculation();
        }

        [TestMethod()]
        public void WhenMarkCalculationThenShouldReturn8Ads()
        {
            new IdealistaController().MarkCalculation().Should().HaveCount(8);
        }

        [TestMethod()]
        public void WhenQualityManagerThenShouldReturnAllAds()
        {
            var allAdsCount = FakeDatabase.Instance().GetOrderedAds().Count();
            new IdealistaController().QualityManager().Should().HaveCount(allAdsCount);
            new IdealistaController().QualityManager().Should().HaveCount(allAdsCount);
        }

        [TestMethod()]
        public void WhenApplicationUserThenShouldReturnOnlyRelevantAds()
        {
            var relevantAds = FakeDatabase.Instance().GetOrderedAds().Count(x => x.Mark > 40);
            new IdealistaController().ApplicationUser().Should().HaveCount(relevantAds);
        }

        private string GetJsonFullPath(string filename)
        {
            return $"{GetBaseDirectory()}/Infrastructure/jsonPopulateFiles/{filename}.json";
        }

        private string GetAdJsonFullPath()
        {
            return GetJsonFullPath("ads");
        }

        private string GetPictureJsonFullPath()
        {
            return GetJsonFullPath("pictures");
        }

        private string GetBaseDirectory()
        {
            return @"C:/Users/frankamente/src/IdealistaTest/IdealistaTest.DomainTests";
        }
    }
}