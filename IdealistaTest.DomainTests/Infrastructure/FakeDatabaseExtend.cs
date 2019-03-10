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
