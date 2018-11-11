using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using PasswordChecking;
using System.IO;

namespace UnitTesting.Tests
{
    public class DemoTest2
    {
        [Fact]
        public void ValidStringShouldWork()
        {
            string actual = DemoProgram.Validate("Tsundere Vong");

            Assert.Contains("Tsundere", actual);
        }

    }
}
