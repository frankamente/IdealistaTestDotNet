using IdealistaTest.Infrastructure;
using IdealistaTest.Services;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace IdealistaTest.Controllers
{
    public class IdealistaController : ApiController
    {
        [HttpGet]
        public IEnumerable<Domain.Entities.Ad> MarkCalculation()
        {
            new IdealistaService().MarkCalculation();
            return FakeDatabase.Instance().GetOrderedAds();
        }

        public IEnumerable<Domain.Entities.Ad> QualityManager()
        {
            return FakeDatabase.Instance().GetOrderedAds();
        }

        public IEnumerable<Domain.Entities.Ad> ApplicationUser()
        {
            return FakeDatabase.Instance().GetOrderedAds().Where(x => !x.IsIrrelevant());
        }
    }
}
