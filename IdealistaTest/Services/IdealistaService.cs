using System;
using IdealistaTest.Domain.Entities;
using IdealistaTest.Infrastructure;
using Microsoft.Ajax.Utilities;
using System.Collections.Generic;
using System.Linq;

namespace IdealistaTest.Services
{
    public class IdealistaService
    {
        public void MarkCalculation()
        {
            GetOrderedAds().ForEach(x => x.CalculateMark());
        }

        public IEnumerable<Ad> GetQualityManagerAds()
        {
            return GetOrderedAds();
        }

        public IEnumerable<Ad> GetUserAds()
        {
            return GetOrderedAds().Where(x => !x.IsIrrelevant());
        }

        private IEnumerable<Ad> GetOrderedAds()
        {
            return FakeDatabase.Instance().GetOrderedAds();
        }
    }
}