using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using IdealistaTest.Services;

namespace IdealistaTest.Controllers
{
    public class IdealistaController : ApiController
    {
        [HttpGet]
        public IEnumerable<string> MarkCalculation()
        {
            new IdealistaService();
            return new[] { "value1", "value2" };
        }
    }
}
