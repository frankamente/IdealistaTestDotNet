using IdealistaTest.Domain.Entities;

namespace IdealistaTest.Domain.MarkFilters
{
    public class DescriptionSizeMarkFilter : IMarkFilter
    {
        public void CalculateMark(Ad ad)
        {
            switch (ad.Typology)
            {
                case Typology.GARAGE:
                    return;
                case Typology.CHALET:
                    ChaletDescriptionMarkFilter(ad);
                    break;
                case Typology.FLAT:
                    FlatDescriptionSizeMarkFilter(ad);
                    break;
            }
        }

        private void ChaletDescriptionMarkFilter(Ad ad)
        {
            if (CountWords(ad.Description) <= 50)
            {
                return;
            }

            ad.Mark += 20;
        }

        private void FlatDescriptionSizeMarkFilter(Ad ad)
        {
            if (CountWords(ad.Description) >= 50)
            {
                ad.Mark += 30;
                return;
            }

            if (CountWords(ad.Description) < 20) return;
            ad.Mark += 10;
        }

        private int CountWords(string text)
        {
            text = text.Trim();
            int wordCount = 0, index = 0;

            while (index < text.Length)
            {
                while (index < text.Length && !char.IsWhiteSpace(text[index]))
                    index++;

                wordCount++;

                while (index < text.Length && char.IsWhiteSpace(text[index]))
                    index++;
            }

            return wordCount;
        }
    }
}