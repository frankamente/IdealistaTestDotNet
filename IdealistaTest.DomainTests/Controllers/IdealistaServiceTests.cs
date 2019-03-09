using Microsoft.VisualStudio.TestTools.UnitTesting;
using IdealistaTest.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using IdealistaTest.Services;

namespace IdealistaTest.Controllers.Tests
{
    [TestClass]
    public class IdealistaServiceTests
    {
        [TestMethod]
        public void MarkCalculationTest()
        {
            new IdealistaService();
        }
    }
}