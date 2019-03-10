using FluentAssertions;
using IdealistaTest.Domain.Entities;
using IdealistaTest.Domain.MarkFilters;
using IdealistaTest.DomainTests.Infrastructure;
using IdealistaTest.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace IdealistaTest.DomainTests.Domain.MarkFilters
{
    [TestClass]
    public class MarkFilterTests
    {
        private FakeDatabase fakeDatabaseInstance;
        private List<string> KeyWordsList = new List<string> { "Luminoso", "Nuevo", "Céntrico", "Reformado", "Ático" };
        [TestInitialize]
        public void TestInitialize()
        {
            fakeDatabaseInstance = FakeDatabase.Instance();
            new FakeDatabaseExtend().RestartFakeDatabaseInstance();
            fakeDatabaseInstance.InitializeDatabase(GetAdJsonFullPath(), GetPictureJsonFullPath());
        }

        [TestMethod]
        public void WhenAdHaveNoPicturesThenNoPicturesMarkFilterShouldDecreaseMarkInTen()
        {
            var firstAd = fakeDatabaseInstance.GetOrderedAds().First();
            var initialMark = firstAd.Mark;

            firstAd.Pictures.Clear();
            new NoPicturesMarkFilter().CalculateMark(firstAd);

            firstAd.Mark.Should().Be(initialMark - 10);
        }

        [TestMethod]
        public void WhenAdHaveAnyPictureThenNoPicturesMarkFilterShouldKeepMarkAsBefore()
        {
            var firstAd = fakeDatabaseInstance.GetOrderedAds().First();
            var initialMark = firstAd.Mark;

            firstAd.Pictures.Add(new Picture());
            new NoPicturesMarkFilter().CalculateMark(firstAd);

            firstAd.Mark.Should().Be(initialMark);
        }

        [TestMethod]
        public void WhenAdHaveNoPicturesThenPictureQualityMarkFilterShouldKeepMarkAsBefore()
        {
            var firstAd = fakeDatabaseInstance.GetOrderedAds().First();
            var initialMark = firstAd.Mark;

            firstAd.Pictures.Clear();
            new PictureQualityMarkFilter().CalculateMark(firstAd);

            firstAd.Mark.Should().Be(initialMark);
        }

        [TestMethod]
        public void WhenAdHaveOneHdPictureThenPictureQualityMarkFilterShouldIncreaseMarkInTwenty()
        {
            var firstAd = fakeDatabaseInstance.GetOrderedAds().First();
            var initialMark = firstAd.Mark;

            firstAd.Pictures.Clear();

            firstAd.Pictures.Add(GetHdPicture());
            new PictureQualityMarkFilter().CalculateMark(firstAd);

            firstAd.Mark.Should().Be(initialMark + 20);
        }

        [TestMethod]
        public void WhenAdHave2HdPictureThenPictureQualityMarkFilterShouldIncreaseMarkInForty()
        {
            var firstAd = fakeDatabaseInstance.GetOrderedAds().First();
            var initialMark = firstAd.Mark;

            firstAd.Pictures.Clear();

            firstAd.Pictures.Add(GetHdPicture());
            firstAd.Pictures.Add(GetHdPicture());
            new PictureQualityMarkFilter().CalculateMark(firstAd);

            firstAd.Mark.Should().Be(initialMark + 40);
        }

        [TestMethod]
        public void WhenAdHave1SdPictureThenPictureQualityMarkFilterShouldIncreaseMarkInTen()
        {
            var firstAd = fakeDatabaseInstance.GetOrderedAds().First();
            var initialMark = firstAd.Mark;

            firstAd.Pictures.Clear();

            firstAd.Pictures.Add(GetSdPicture());
            new PictureQualityMarkFilter().CalculateMark(firstAd);

            firstAd.Mark.Should().Be(initialMark + 10);
        }

        [TestMethod]
        public void WhenAdHave2SdPictureThenPictureQualityMarkFilterShouldIncreaseMarkInTwenty()
        {
            var firstAd = fakeDatabaseInstance.GetOrderedAds().First();
            var initialMark = firstAd.Mark;

            firstAd.Pictures.Clear();

            firstAd.Pictures.Add(GetSdPicture());
            firstAd.Pictures.Add(GetSdPicture());
            new PictureQualityMarkFilter().CalculateMark(firstAd);

            firstAd.Mark.Should().Be(initialMark + 20);
        }

        [TestMethod]
        public void WhenAdHave1HdPictureAnd1SdPictureThenPictureQualityMarkFilterShouldIncreaseMarkInThirty()
        {
            var firstAd = fakeDatabaseInstance.GetOrderedAds().First();
            var initialMark = firstAd.Mark;

            firstAd.Pictures.Clear();

            firstAd.Pictures.Add(GetSdPicture());
            firstAd.Pictures.Add(GetHdPicture());
            new PictureQualityMarkFilter().CalculateMark(firstAd);

            firstAd.Mark.Should().Be(initialMark + 30);
        }

        [TestMethod]
        public void WhenAdHaveNotDescriptionThenNoDescriptionMarkFilterShouldKeepMarkAsBefore()
        {
            var firstAd = fakeDatabaseInstance.GetOrderedAds().First();
            var initialMark = firstAd.Mark;

            firstAd.Description = "";
            new NoDescriptionMarkFilter().CalculateMark(firstAd);

            firstAd.Mark.Should().Be(initialMark);
        }

        [TestMethod]
        public void WhenAdHaveNotEmptyDescriptionThenNoDescriptionMarkFilterShouldIncreaseMarkInFive()
        {
            var firstAd = fakeDatabaseInstance.GetOrderedAds().First();
            var initialMark = firstAd.Mark;

            firstAd.Description = "Sample description";
            new NoDescriptionMarkFilter().CalculateMark(firstAd);

            firstAd.Mark.Should().Be(initialMark + 5);
        }

        [TestMethod]
        public void WhenAdIsChaletAndDescriptionHasLessThan50WordsThenDescriptionSizeMarkFilterShouldKeepMarkAsBefore()
        {
            var firstAd = fakeDatabaseInstance.GetOrderedAds().First();
            var initialMark = firstAd.Mark;

            firstAd.Typology = Typology.CHALET;
            firstAd.Description = "Sample description";
            new DescriptionSizeMarkFilter().CalculateMark(firstAd);

            firstAd.Mark.Should().Be(initialMark);
        }

        [TestMethod]
        public void WhenAdIsChaletAndDescriptionHasMoreThan50WordsThenDescriptionSizeMarkFilterShouldIncreaseMarkInTwenty()
        {
            var firstAd = fakeDatabaseInstance.GetOrderedAds().First();
            var initialMark = firstAd.Mark;

            firstAd.Typology = Typology.CHALET;
            firstAd.Description = GetMoreThan50WordsDescription();
            new DescriptionSizeMarkFilter().CalculateMark(firstAd);

            firstAd.Mark.Should().Be(initialMark + 20);
        }

        [TestMethod]
        public void WhenAdIsFlatAndDescriptionHasLessThan20WordsThenDescriptionSizeMarkFilterShouldKeepMarkAsBefore()
        {
            var firstAd = fakeDatabaseInstance.GetOrderedAds().First();
            var initialMark = firstAd.Mark;

            firstAd.Typology = Typology.FLAT;
            firstAd.Description = "Sample description";
            new DescriptionSizeMarkFilter().CalculateMark(firstAd);

            firstAd.Mark.Should().Be(initialMark);
        }

        [TestMethod]
        public void WhenAdIsFlatAndDescriptionHas20WordsThenDescriptionSizeMarkFilterShouldIncreaseMarkInTen()
        {
            var firstAd = fakeDatabaseInstance.GetOrderedAds().First();
            var initialMark = firstAd.Mark;

            firstAd.Typology = Typology.FLAT;
            firstAd.Description = Get20WordsDescription();
            new DescriptionSizeMarkFilter().CalculateMark(firstAd);

            firstAd.Mark.Should().Be(initialMark + 10);
        }

        [TestMethod]
        public void WhenAdIsFlatAndDescriptionHasMoreThan20AndLessThan50WordsThenDescriptionSizeMarkFilterShouldIncreaseMarkInTen()
        {
            var firstAd = fakeDatabaseInstance.GetOrderedAds().First();
            var initialMark = firstAd.Mark;

            firstAd.Typology = Typology.FLAT;
            firstAd.Description = Get49WordsDescription();
            new DescriptionSizeMarkFilter().CalculateMark(firstAd);

            firstAd.Mark.Should().Be(initialMark + 10);
        }

        [TestMethod]
        public void WhenAdIsFlatAndDescriptionHas50WordsThenDescriptionSizeMarkFilterShouldIncreaseMarkInTen()
        {
            var firstAd = fakeDatabaseInstance.GetOrderedAds().First();
            var initialMark = firstAd.Mark;

            firstAd.Typology = Typology.FLAT;
            firstAd.Description = Get50WordsDescription();
            new DescriptionSizeMarkFilter().CalculateMark(firstAd);

            firstAd.Mark.Should().Be(initialMark + 30);
        }

        [TestMethod]
        public void WhenAdDescriptionHas1KeyWordThenDescriptionKeyWordsMarkFilterShouldIncreaseMarkInFive()
        {
            var firstAd = fakeDatabaseInstance.GetOrderedAds().First();
            var initialMark = firstAd.Mark;

            firstAd.Description = KeyWordsList.First();
            new DescriptionKeyWordsMarkFilter().CalculateMark(firstAd);

            firstAd.Mark.Should().Be(initialMark + 5);
        }

        [TestMethod]
        public void WhenAdDescriptionHas2KeyWordThenDescriptionKeyWordsMarkFilterShouldIncreaseMarkIn10()
        {
            var firstAd = fakeDatabaseInstance.GetOrderedAds().First();
            var initialMark = firstAd.Mark;

            firstAd.Description = $"{KeyWordsList.First()} {KeyWordsList.Last()}";
            new DescriptionKeyWordsMarkFilter().CalculateMark(firstAd);

            firstAd.Mark.Should().Be(initialMark + 10);
        }

        [TestMethod]
        public void WhenAdDescriptionHasAllKeyWordThenDescriptionKeyWordsMarkFilterShouldIncreaseMarkInFivePerKeyWord()
        {
            var firstAd = fakeDatabaseInstance.GetOrderedAds().First();
            var initialMark = firstAd.Mark;

            firstAd.Description = GetAllKeyWordDescription();
            new DescriptionKeyWordsMarkFilter().CalculateMark(firstAd);

            firstAd.Mark.Should().Be(initialMark + KeyWordsList.Count * 5);
        }

        [TestMethod]
        public void WhenChaletAdIsCompleteThenIsCompleteMarkFilterShouldIncreaseMarkIn40()
        {
            var completeAd = GetChaletCompleteAd();
            var initialMark = completeAd.Mark;

            new IsCompleteMarkFilter().CalculateMark(completeAd);

            completeAd.Mark.Should().Be(initialMark + 40);
        }

        [TestMethod]
        public void WhenChaletAdIsInCompleteByDescriptionThenIsCompleteMarkFilterShouldKeepMarkAsBefore()
        {
            var completeAd = GetChaletCompleteAd();
            var initialMark = completeAd.Mark;

            completeAd.Description = "";
            new IsCompleteMarkFilter().CalculateMark(completeAd);

            completeAd.Mark.Should().Be(initialMark);
        }

        [TestMethod]
        public void WhenChaletAdIsInCompleteByHouseSizeThenIsCompleteMarkFilterShouldKeepMarkAsBefore()
        {
            var completeAd = GetChaletCompleteAd();
            var initialMark = completeAd.Mark;

            completeAd.HouseSize = 0;
            new IsCompleteMarkFilter().CalculateMark(completeAd);

            completeAd.Mark.Should().Be(initialMark);
        }

        [TestMethod]
        public void WhenChaletAdIsInCompleteByGardenSizeThenIsCompleteMarkFilterShouldKeepMarkAsBefore()
        {
            var completeAd = GetChaletCompleteAd();
            var initialMark = completeAd.Mark;

            completeAd.GardenSize = 0;
            new IsCompleteMarkFilter().CalculateMark(completeAd);

            completeAd.Mark.Should().Be(initialMark);
        }

        [TestMethod]
        public void WhenChaletAdIsInCompleteByPictureSizeThenIsCompleteMarkFilterShouldKeepMarkAsBefore()
        {
            var completeAd = GetChaletCompleteAd();
            var initialMark = completeAd.Mark;

            completeAd.Pictures.Clear();
            new IsCompleteMarkFilter().CalculateMark(completeAd);

            completeAd.Mark.Should().Be(initialMark);
        }

        [TestMethod]
        public void WhenFlatAdIsCompleteThenIsCompleteMarkFilterShouldIncreaseMarkIn40()
        {
            var completeAd = GetFlatCompleteAd();
            var initialMark = completeAd.Mark;

            new IsCompleteMarkFilter().CalculateMark(completeAd);

            completeAd.Mark.Should().Be(initialMark + 40);
        }

        [TestMethod]
        public void WhenFlatAdIsInCompleteByDescriptionThenIsCompleteMarkFilterShouldKeepMarkAsBefore()
        {
            var completeAd = GetFlatCompleteAd();
            var initialMark = completeAd.Mark;

            completeAd.Description = "";
            new IsCompleteMarkFilter().CalculateMark(completeAd);

            completeAd.Mark.Should().Be(initialMark);
        }

        [TestMethod]
        public void WhenFlatAdIsInCompleteByHouseSizeThenIsCompleteMarkFilterShouldKeepMarkAsBefore()
        {
            var completeAd = GetFlatCompleteAd();
            var initialMark = completeAd.Mark;

            completeAd.HouseSize = 0;
            new IsCompleteMarkFilter().CalculateMark(completeAd);

            completeAd.Mark.Should().Be(initialMark);
        }

        [TestMethod]
        public void WhenFlatAdIsInCompleteByPictureSizeThenIsCompleteMarkFilterShouldKeepMarkAsBefore()
        {
            var completeAd = GetFlatCompleteAd();
            var initialMark = completeAd.Mark;

            completeAd.Pictures.Clear();
            new IsCompleteMarkFilter().CalculateMark(completeAd);

            completeAd.Mark.Should().Be(initialMark);
        }

        [TestMethod]
        public void WhenGarageAdIsCompleteThenIsCompleteMarkFilterShouldIncreaseMarkIn40()
        {
            var completeAd = GetGarageCompleteAd();
            var initialMark = completeAd.Mark;

            new IsCompleteMarkFilter().CalculateMark(completeAd);

            completeAd.Mark.Should().Be(initialMark + 40);
        }

        [TestMethod]
        public void WhenGarageAdIsInCompleteByPictureSizeThenIsCompleteMarkFilterShouldKeepMarkAsBefore()
        {
            var completeAd = GetGarageCompleteAd();
            var initialMark = completeAd.Mark;

            completeAd.Pictures.Clear();
            new IsCompleteMarkFilter().CalculateMark(completeAd);

            completeAd.Mark.Should().Be(initialMark);
        }

        private Ad GetChaletCompleteAd()
        {
            var completeAd = fakeDatabaseInstance.GetOrderedAds().First();
            completeAd.Typology = Typology.CHALET;
            completeAd.Description = Get20WordsDescription();
            completeAd.GardenSize = 30;
            completeAd.HouseSize = 30;
            completeAd.Pictures.Add(new Picture());
            return completeAd;
        }

        private Ad GetFlatCompleteAd()
        {
            var completeAd = fakeDatabaseInstance.GetOrderedAds().First();
            completeAd.Typology = Typology.FLAT;
            completeAd.Description = Get20WordsDescription();
            completeAd.GardenSize = 30;
            completeAd.HouseSize = 30;
            completeAd.Pictures.Add(new Picture());
            return completeAd;
        }

        private Ad GetGarageCompleteAd()
        {
            var completeAd = fakeDatabaseInstance.GetOrderedAds().First();
            completeAd.Typology = Typology.GARAGE;
            completeAd.Pictures.Add(new Picture());
            return completeAd;
        }

        private string GetAllKeyWordDescription()
        {
            var allKeyWordDescription = "";
            KeyWordsList.ForEach(keyWord => { allKeyWordDescription += $"{keyWord} "; });
            return allKeyWordDescription;
        }

        private static Picture GetHdPicture()
        {
            return new Picture { Quality = PictureQuality.HD };
        }

        private static Picture GetSdPicture()
        {
            return new Picture { Quality = PictureQuality.SD };
        }

        private static string Get20WordsDescription()
        {
            return "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy";
        }

        private static string Get49WordsDescription()
        {
            return "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy Lorem Ipsum has been the industry's standard dummy hola";
        }

        private static string Get50WordsDescription()
        {
            return "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy Lorem Ipsum has been the industry's standard dummy hola adios";
        }

        private static string GetMoreThan50WordsDescription()
        {
            return "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.";
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