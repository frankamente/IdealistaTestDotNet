using IdealistaTest.Domain.Entities;
using Microsoft.Ajax.Utilities;
using System.Linq;

namespace IdealistaTest.Domain.MarkFilters
{
    public class PictureQualityMarkFilter : IMarkFilter
    {
        public void CalculateMark(Ad ad)
        {
            if (!ad.Pictures.Any())
            {
                return;
            }
            ad.Pictures.ForEach(picture =>
            {
                switch (picture.Quality)
                {
                    case PictureQuality.HD:
                        ad.Mark += 20;
                        return;
                    case PictureQuality.SD:
                        ad.Mark += 10;
                        return;
                }
            });
        }
    }
}