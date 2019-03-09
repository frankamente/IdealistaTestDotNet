using FluentAssertions;
using IdealistaTest.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace IdealistaTest.DomainTests.Infrastructure
{
    [TestClass]
    public class FakeDatabaseTest
    {
        private const int AD_QUANTITY = 8;

        [TestInitialize]
        public void TestInitialize()
        {
            new FakeDatabaseExtend().RestartFakeDatabaseInstance();
        }

        [TestMethod]
        public void WhenNoDatabaseInitializeGetAdShouldReturnEmptyIEnumerable()
        {
            var fakeDatabase = FakeDatabase.Instance();
            fakeDatabase.GetAds().Should().BeEmpty();
        }

        [TestMethod]
        public void WhenNoDatabaseInitializeGetPictureShouldReturnEmptyIEnumerable()
        {
            var fakeDatabase = FakeDatabase.Instance();
            fakeDatabase.GetPictures().Should().BeEmpty();
        }

        [TestMethod]
        public void WhenDatabaseInitializeGetAdsShouldReturnNotEmptyIEnumerable()
        {
            var fakeDatabase = FakeDatabase.Instance();
            fakeDatabase.InitializeDatabase(GetAdJsonFullPath(), GetPictureJsonFullPath());
            fakeDatabase.GetAds().Should().NotBeEmpty();
        }

        [TestMethod]
        public void WhenInitializeDatabaseWithNullAdJsonFullPathShouldThrowFileLoadException()
        {
            var fakeDatabase = FakeDatabase.Instance();
            fakeDatabase.Invoking(x => x.InitializeDatabase(null, GetPictureJsonFullPath()))
                .Should().Throw<FileLoadException>();
        }

        [TestMethod]
        public void WhenInitializeDatabaseWithNullPictureJsonFullPathShouldThrowFileLoadException()
        {
            var fakeDatabase = FakeDatabase.Instance();
            fakeDatabase.Invoking(x => x.InitializeDatabase(GetAdJsonFullPath(), null))
                .Should().Throw<FileLoadException>();
        }

        [TestMethod]
        public void WhenDatabaseInitializeGetAdsShouldReturnAdsQuantityIEnumerable()
        {
            var fakeDatabase = FakeDatabase.Instance();
            fakeDatabase.InitializeDatabase(GetAdJsonFullPath(), GetPictureJsonFullPath());
            fakeDatabase.GetAds().Should().HaveCount(AD_QUANTITY);
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
