using FluentAssertions;
using IdealistaTest.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Linq;

namespace IdealistaTest.DomainTests.Infrastructure
{
    [TestClass]
    public class FakeDatabaseTest
    {
        private const int ADS_QUANTITY = 8;
        private FakeDatabase fakeDatabase;

        [TestInitialize]
        public void TestInitialize()
        {
            new FakeDatabaseExtend().RestartFakeDatabaseInstance();
            fakeDatabase = FakeDatabase.Instance();
        }

        [TestMethod]
        public void WhenNoDatabaseInitializeGetAdShouldReturnEmptyIEnumerable()
        {
            fakeDatabase.GetOrderedAds().Should().BeEmpty();
        }

        [TestMethod]
        public void WhenDatabaseInitializeGetAdsShouldReturnNotEmptyIEnumerable()
        {
            fakeDatabase.InitializeDatabase(GetAdJsonFullPath(), GetPictureJsonFullPath());
            fakeDatabase.GetOrderedAds().Should().NotBeEmpty();
        }

        [TestMethod]
        public void WhenInitializeDatabaseWithNullAdJsonFullPathShouldThrowFileLoadException()
        {
            fakeDatabase.Invoking(x => x.InitializeDatabase(null, GetPictureJsonFullPath()))
                .Should().Throw<FileLoadException>();
        }

        [TestMethod]
        public void WhenInitializeDatabaseWithNullPictureJsonFullPathShouldThrowFileLoadException()
        {
            fakeDatabase.Invoking(x => x.InitializeDatabase(GetAdJsonFullPath(), null))
                .Should().Throw<FileLoadException>();
        }

        [TestMethod]
        public void WhenDatabaseInitializeGetAdsShouldReturnAdsQuantityIEnumerable()
        {
            fakeDatabase.InitializeDatabase(GetAdJsonFullPath(), GetPictureJsonFullPath());
            fakeDatabase.GetOrderedAds().Should().HaveCount(ADS_QUANTITY);
        }

        [TestMethod]
        public void WhenDatabaseInitializeGetAdsShouldReturnAdsWithDefaultIrrelevantDateAsDateTimeDefaultValue()
        {
            fakeDatabase.InitializeDatabase(GetAdJsonFullPath(), GetPictureJsonFullPath());
            fakeDatabase.GetOrderedAds().Where(x => x.IrrelevantDate != DateTime.MinValue).Should().BeEmpty();
        }

        [TestMethod]
        public void WhenDatabaseInitializeGetAdsShouldReturnAdsWithNullMarkDate()
        {
            fakeDatabase.GetOrderedAds().Where(
                    x => x.IrrelevantDate != DateTime.MinValue)
                .Should().BeEmpty();
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
            return @"C:\\Users\\frankamente\\src\\IdealistaTest\\IdealistaTest.DomainTests";
        }
    }
}
