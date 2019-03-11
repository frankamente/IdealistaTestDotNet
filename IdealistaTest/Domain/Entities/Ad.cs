using IdealistaTest.Domain.MarkFilters;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace IdealistaTest.Domain.Entities
{
    public class Ad
    {
        private const int IRRELEVANT_AD_MARK = 40;

        public int Id { get; }
        public string Description { get; set; }
        public Typology Typology { get; set; }
        public int HouseSize { get; set; }
        public int GardenSize { get; set; }
        public IList<Picture> Pictures { get; }
        public int Mark { get; set; }
        [JsonConverter(typeof(DateFormatConverter), "dd/MM/yyyy:HH:mm")]
        public DateTime IrrelevantDate { get; private set; }

        public Ad(Infrastructure.Entities.Ad infrastructureAd, IEnumerable<Infrastructure.Entities.Picture> infrastructurePictures)
        {
            Id = infrastructureAd.Id;
            Description = infrastructureAd.Description;
            GardenSize = infrastructureAd.GardenSize;
            HouseSize = infrastructureAd.HouseSize;
            if (Enum.TryParse(infrastructureAd.Typology, out Typology typology))
            {
                Typology = typology;
            }
            Pictures = GetPicturesByInfrastructurePictures(GetInfrastructurePictures(infrastructureAd, infrastructurePictures));
        }

        public bool ShouldSerializeIrrelevantDate()
        {
            return IsIrrelevant() && IsIrrelevantDateUpdated();
        }

        private IList<Picture> GetPicturesByInfrastructurePictures(IEnumerable<Infrastructure.Entities.Picture> getInfrastructurePictures)
        {
            return getInfrastructurePictures.Select(infrastructurePicture =>
            {
                var picture = new Picture
                {
                    Id = infrastructurePicture.Id,
                    Url = infrastructurePicture.Url
                };
                if (Enum.TryParse(infrastructurePicture.Quality, out PictureQuality pictureQuality))
                {
                    picture.Quality = pictureQuality;
                }
                return picture;
            }).ToList();
        }

        private static IEnumerable<Infrastructure.Entities.Picture> GetInfrastructurePictures(Infrastructure.Entities.Ad infrastructureAd, IEnumerable<Infrastructure.Entities.Picture> infrastructurePictures)
        {
            return infrastructurePictures.Where(x => infrastructureAd.Pictures.Contains(x.Id));
        }

        public bool IsIrrelevant()
        {
            return Mark < IRRELEVANT_AD_MARK;
        }

        public bool IsIrrelevantDateUpdated()
        {
            return IrrelevantDate != DateTime.MinValue;
        }

        public override bool Equals(object obj)
        {

            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            return ((Ad)obj).Id == Id;
        }

        public override int GetHashCode()
        {
            return Id;
        }

        public void CalculateMark()
        {
            IList<IMarkFilter> markFilters = new List<IMarkFilter>
            {
                new NoPicturesMarkFilter(),
                new NoDescriptionMarkFilter(),
                new DescriptionSizeMarkFilter(),
                new DescriptionKeyWordsMarkFilter(),
                new IsCompleteMarkFilter(),
                new PictureQualityMarkFilter()
            };

            markFilters.ForEach(x => x.CalculateMark(this));
            if (IsIrrelevant())
            {
                IrrelevantDate = DateTime.Now;
            }
        }
    }
}