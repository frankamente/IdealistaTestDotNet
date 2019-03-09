using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Ajax.Utilities;

namespace IdealistaTest.Domain.Entities
{
    public class Ad
    {
        public Ad(Infrastructure.Entities.Ad infrastructureAd, IEnumerable<Infrastructure.Entities.Picture> infrastructurePictures)
        {
            Id = infrastructureAd.Id;
            Description = infrastructureAd.Description;
            GardenSize = infrastructureAd.GardenSize;
            HouseSize = infrastructureAd.HouseSize;
            Pictures = GetPicturesByInfrastructurePictures(GetInfrastructurePictures(infrastructureAd, infrastructurePictures));
        }

        private IList<Picture> GetPicturesByInfrastructurePictures(IEnumerable<Infrastructure.Entities.Picture> getInfrastructurePictures)
        {
            return getInfrastructurePictures.Select(infrastructurePicture =>
            {
                return new Picture
                {
                    Id = infrastructurePicture.Id
                };
            }).ToList();
        }

        private static IEnumerable<Infrastructure.Entities.Picture> GetInfrastructurePictures(Infrastructure.Entities.Ad infrastructureAd, IEnumerable<Infrastructure.Entities.Picture> infrastructurePictures)
        {
            return infrastructurePictures.Where(x => infrastructureAd.Id == x.Id);
            //return infrastructurePictures.Where(adPictureId => GetInfrastructurePictureId(infrastructurePictures).Contains(adPictureId));
        }

        public int Id { get; set; }
        public string Description { get; set; }
        public int HouseSize { get; set; }
        public int GardenSize { get; set; }
        public IList<Picture> Pictures { get; set; }
        public DateTime IrrelevantDate { get; set; }

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
    }
}