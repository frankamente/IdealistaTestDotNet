using IdealistaTest.Domain.Entities;
using System.Linq;

namespace IdealistaTest.Domain.MarkFilters
{
    public class IsCompleteMarkFilter : IMarkFilter
    {
        public void CalculateMark(Ad ad)
        {
            if (!IsComplete(ad))
            {
                return;
            }

            ad.Mark += 40;
        }

        private bool IsComplete(Ad ad)
        {
            switch (ad.Typology)
            {
                case Typology.CHALET:
                    return IsChaletComplete(ad);
                case Typology.FLAT:
                    return IsFlatComplete(ad);
                case Typology.GARAGE:
                    return IsGarageComplete(ad);
            }

            return false;
        }

        private bool IsGarageComplete(Ad ad)
        {
            return HaveAnyPicture(ad);
        }

        private bool IsChaletComplete(Ad ad)
        {
            if (!IsFlatComplete(ad)) return false;

            if (!HasGardenSize(ad))
            {
                return false;
            }

            return true;
        }

        private static bool IsFlatComplete(Ad ad)
        {
            if (!HasDescription(ad))
            {
                return false;
            }

            if (!HaveAnyPicture(ad))
            {
                return false;
            }

            if (!HasHouseSize(ad))
            {
                return false;
            }

            return true;
        }

        private static bool HasHouseSize(Ad ad)
        {
            return ad.HouseSize > 0;
        }

        private static bool HasGardenSize(Ad ad)
        {
            return ad.GardenSize > 0;
        }

        private static bool HasDescription(Ad ad)
        {
            return !string.IsNullOrEmpty(ad.Description);
        }

        private static bool HaveAnyPicture(Ad ad)
        {
            return ad.Pictures.Any();
        }
    }
}