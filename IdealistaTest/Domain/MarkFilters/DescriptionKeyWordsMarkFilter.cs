using IdealistaTest.Domain.Entities;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IdealistaTest.Domain.MarkFilters
{
    public class DescriptionKeyWordsMarkFilter : IMarkFilter
    {
        private IList<string> KeyWordsList = new List<string> { "Luminoso", "Nuevo", "Céntrico", "Reformado", "Ático" };
        public void CalculateMark(Ad ad)
        {
            GetWordsByString(ad.Description).ForEach(x =>
            {
                if (!KeyWordsList.Select(y => y.ToUpper()).Contains(x.ToUpper()))
                {
                    return;
                }
                ad.Mark += 5;
            });
        }

        private IEnumerable<string> GetWordsByString(string text)
        {
            return text.Split(new[] { '.', '?', '!', ' ', ';', ':', ',' }, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}