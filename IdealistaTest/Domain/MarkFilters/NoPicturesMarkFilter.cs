using IdealistaTest.Domain.Entities;
using System.Linq;

namespace IdealistaTest.Domain.MarkFilters
{
    public class NoPicturesMarkFilter : IMarkFilter
    {
        public void CalculateMark(Ad ad)
        {
            if (ad.Pictures.Any())
            {
                return;
            }

            ad.Mark -= 10;
        }
    }
}