using IdealistaTest.Infrastructure;
using Microsoft.Ajax.Utilities;

namespace IdealistaTest.Services
{
    public class IdealistaService
    {
        public void MarkCalculation()
        {
            FakeDatabase.Instance().GetOrderedAds().ForEach(x => x.CalculateMark());
        }
    }
}