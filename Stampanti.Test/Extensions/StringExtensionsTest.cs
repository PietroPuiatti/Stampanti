using Shouldly;
using Stampanti.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Stampanti.Test.Extensions
{
    public class StringExtensionsTest
    {
        public class EmptyIfNullTest
        {
            [Fact]
            public void ReturnEmptyIfNull()
            {
                string s = null;
                s.EmptyIfNull().ShouldBeEmpty();

            }

            [Theory]
            [InlineData("Poldo", "Poldo")]
            [InlineData(null, "")]
            [InlineData("pippo", "pippo")]
            public void NotNullNoChange(string s, string expected)
            {
                s.EmptyIfNull().ShouldBe(expected);
            }

        }

        public class TrimTest
        {
            [Fact]
            public void ReturnTrimmed()
            {
                string s = "Poldo   ";
                var expected = "Poldo";
                s.EmptyIfNull().Trim().ShouldBe(expected);
            }

            [Fact]
            public void ReturnLeft()
            {
                string s = "Poldo";
                var expected = "Po";
                s.EmptyIfNull().Left(2).ShouldBe(expected);
            }

            [Fact]
            public void ReturnRight()
            {
                string s = "Poldo";
                var expected = "do";
                s.EmptyIfNull().Right(2).ShouldBe(expected);
            }
            
            [Fact]
            public void ReturnLeftTrimmed()
            {
                string s = "    Poldo";
                var expected = "Po";
                s.EmptyIfNull().Trim().Left(2).ShouldBe(expected);
            }

            [Fact]
            public void ReturnRightTrimmed()
            {
                string s = "Poldo    ";
                var expected = "do";
                s.EmptyIfNull().Trim().Right(2).ShouldBe(expected);
            }
        }

        public class ValidHttpAddressTest
        {
            [Fact]
            public void IsValidHttpAddressTest()
            {
                string s = "https://xunit.net/docs/getting-started/netfx/visual-studio";
                
                s.EmptyIfNull().IsValidHttpAddress().ShouldBeTrue();
            }
        }
    }
}
