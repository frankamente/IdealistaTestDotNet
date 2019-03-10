using IdealistaTest.Infrastructure;
using IdealistaTest.Services;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace IdealistaTest.Controllers
{
    [RoutePrefix("api/idealista")]
    public class IdealistaController : ApiController
    {
        [Route("markCalculation")]
        [HttpGet]
        public IEnumerable<Domain.Entities.Ad> MarkCalculation()
        {
            new IdealistaService().MarkCalculation();
            return FakeDatabase.Instance().GetOrderedAds();
        }

        [Route("qualityManager")]
        [HttpGet]
        public IEnumerable<Domain.Entities.Ad> QualityManager()
        {
            return FakeDatabase.Instance().GetOrderedAds();
        }

        [Route("applicationUser")]
        [HttpGet]
        public IEnumerable<Domain.Entities.Ad> ApplicationUser()
        {
            return FakeDatabase.Instance().GetOrderedAds().Where(x => !x.IsIrrelevant());
        }
    }
}
