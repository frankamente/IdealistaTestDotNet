using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IdealistaTest.Infrastructure
{
    public class FakeDatabase
    {
        private static volatile FakeDatabase instance;

        private static readonly object InstanceLocker = new object();
        private FakeDatabase()
        {

        }

        public static FakeDatabase Instance()
        {
            if (instance != null) return instance;
            lock (InstanceLocker)
            {
                if (instance == null)
                {
                    instance = new FakeDatabase();
                }
            }
            return instance;
        }

        public  void InitializeDatabase()
        {
            
        }
    }
}