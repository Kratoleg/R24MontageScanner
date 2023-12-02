using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MontageScanLib.Test
{
    public class UserInterfaceLogicTests
    {

        [Theory]
        [InlineData("L123456", true)]
        [InlineData("M999999", true)]
        [InlineData("M000001", true)]
        [InlineData("A987654", true)]
        [InlineData("B654321", true)]
        [InlineData("C111111", true)]
        [InlineData("D999999", true)]
        [InlineData("L15555", false)]
        [InlineData("X12345", false)]
        [InlineData("YABCDE", false)]
        [InlineData("Z", false)]
        [InlineData("123456", false)]
        [InlineData("ABCDEF", false)]
        [InlineData("G12345", false)]
        [InlineData("HABCDE", false)]
        public void InputCheckReturnsExpectedValue(string input, bool expected)
        {
            bool actual = UserInterfaceLogic.InputCheck(input);

            Assert.Equal(expected, actual);

        }

    }
}
