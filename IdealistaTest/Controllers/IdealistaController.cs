using IdealistaTest.Services;
using System.Collections.Generic;
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
            var idealistaService = new IdealistaService();
            idealistaService.MarkCalculation();
            return idealistaService.GetQualityManagerAds();
        }

        [Route("qualityManager")]
        [HttpGet]
        public IEnumerable<Domain.Entities.Ad> QualityManager()
        {
            return new IdealistaService().GetQualityManagerAds();
        }

        [Route("applicationUser")]
        [HttpGet]
        public IEnumerable<Domain.Entities.Ad> ApplicationUser()
        {
            return new IdealistaService().GetUserAds();
        }
    }
}
