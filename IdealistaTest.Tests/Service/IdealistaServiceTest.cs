using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;

namespace IdealistaTest.Tests.Service
{
    public class IdealistaServiceTest
    {
        [Test]
        public void xd()
        {
            string hola = "hola";
            hola.Should().Be(",");
        }
    }
}
