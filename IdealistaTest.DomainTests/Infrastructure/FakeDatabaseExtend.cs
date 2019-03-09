using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdealistaTest.Infrastructure;

namespace IdealistaTest.DomainTests.Infrastructure
{
    public class FakeDatabaseExtend : FakeDatabase
    {
        public void RestartFakeDatabaseInstance()
        {
            RestartInstance();
        }
    }
}
