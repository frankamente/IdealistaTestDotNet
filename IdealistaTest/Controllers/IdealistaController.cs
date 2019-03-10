using IdealistaTest.Services;
using System.Collections.Generic;
using System.Web.Http;
using IdealistaTest.Infrastructure;

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
    }
}
