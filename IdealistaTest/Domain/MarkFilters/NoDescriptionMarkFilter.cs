using IdealistaTest.Domain.Entities;

namespace IdealistaTest.Domain.MarkFilters
{
    public class NoDescriptionMarkFilter : IMarkFilter
    {
        public void CalculateMark(Ad ad)
        {
            if (string.IsNullOrEmpty(ad.Description))
            {
                return;
            }

            ad.Mark += 5;
        }
    }
}