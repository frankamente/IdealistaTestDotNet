using FluentAssertions;
using IdealistaTest.DomainTests.Infrastructure;
using IdealistaTest.Infrastructure;
using IdealistaTest.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace IdealistaTest.DomainTests.Services
{
    [TestClass]
    public class IdealistaServiceTests
    {
        private FakeDatabase fakeDatabaseInstance;
        [TestInitialize]
        public void TestInitialize()
        {
            fakeDatabaseInstance = FakeDatabase.Instance();
            new FakeDatabaseExtend().RestartFakeDatabaseInstance();
            fakeDatabaseInstance.InitializeDatabase(GetAdJsonFullPath(), GetPictureJsonFullPath());
        }

        [TestMethod]
        public void WhenMarkCalculationAndThereAreAdsRelevantThenShouldHaveAnyWithCurrentDateTime()
        {
            new IdealistaService().MarkCalculation();

            var domainAds = fakeDatabaseInstance.GetOrderedAds();
            var relevantAds = domainAds.Where(x => !x.IsIrrelevant());
            if (!relevantAds.Any())
            {
                fakeDatabaseInstance.GetOrderedAds().Where(x =>
                        x.IrrelevantDate.ToShortDateString() == DateTime.Now.ToShortDateString())
                    .Should().BeEmpty();
                return;
            }
            fakeDatabaseInstance.GetOrderedAds().Where(x =>
                    x.IrrelevantDate.ToShortDateString() == DateTime.Now.ToShortDateString())
                .Should().NotBeEmpty();
        }

        [TestMethod]
        public void WhenMarkCalculationAdWithId1HaveMinusFiveMark()
        {
            new IdealistaService().MarkCalculation();
            fakeDatabaseInstance.GetOrderedAds().First(x => x.Id == 1).Mark.Should().Be(-5);
        }

        [TestMethod]
        public void WhenMarkCalculationAdWithId1Have50Mark()
        {
            new IdealistaService().MarkCalculation();
            fakeDatabaseInstance.GetOrderedAds().First(x => x.Id == 2).Mark.Should().Be(50);
        }

        [TestMethod]
        public void WhenMarkCalculationAdWithId3Have20Mark()
        {
            new IdealistaService().MarkCalculation();
            fakeDatabaseInstance.GetOrderedAds().First(x => x.Id == 3).Mark.Should().Be(20);
        }

        [TestMethod]
        public void WhenMarkCalculationAdWithId4Have80Mark()
        {
            new IdealistaService().MarkCalculation();
            fakeDatabaseInstance.GetOrderedAds().First(x => x.Id == 4).Mark.Should().Be(80);
        }

        [TestMethod]
        public void WhenMarkCalculationAdWithId5Have35Mark()
        {
            new IdealistaService().MarkCalculation();
            fakeDatabaseInstance.GetOrderedAds().First(x => x.Id == 5).Mark.Should().Be(35);
        }

        [TestMethod]
        public void WhenMarkCalculationAdWithId6Have50Mark()
        {
            new IdealistaService().MarkCalculation();
            fakeDatabaseInstance.GetOrderedAds().First(x => x.Id == 6).Mark.Should().Be(50);
        }

        [TestMethod]
        public void WhenMarkCalculationAdWithId7HaveNegative5Mark()
        {
            new IdealistaService().MarkCalculation();
            fakeDatabaseInstance.GetOrderedAds().First(x => x.Id == 7).Mark.Should().Be(-5);
        }

        [TestMethod]
        public void WhenMarkCalculationAdWithId8Have65Mark()
        {
            new IdealistaService().MarkCalculation();
            fakeDatabaseInstance.GetOrderedAds().First(x => x.Id == 8).Mark.Should().Be(65);
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