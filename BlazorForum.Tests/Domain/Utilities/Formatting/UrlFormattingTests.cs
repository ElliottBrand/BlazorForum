using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BlazorForum.Tests.Domain.Utilities.Formatting
{
    public class UrlFormattingTests
    {
        [Fact]
        void PrepareUrlTextValidStringReturnsFormattedTest()
        {
            string urlText = "This is some title that needs formatted";

            string result = BlazorForum.Domain.Utilities.Formatting.UrlFormatting.PrepareUrlText(urlText);

            Assert.IsType<string>(result);
            Assert.Equal("this-is-some-title-that-needs-formatted", result);
        }

        [Fact]
        void PrepareUrlTextSpecialCharStringReturnsFormattedTest()
        {
            string urlText = "This is some title that needs formatted!$%&*+?~`^#=:;<>/.,";

            string result = BlazorForum.Domain.Utilities.Formatting.UrlFormatting.PrepareUrlText(urlText);

            Assert.IsType<string>(result);
            Assert.Equal("this-is-some-title-that-needs-formatted", result);
        }

        [Fact]
        void PrepareUrlTextEmptyStringReturnsTest()
        {
            string urlText = "";

            string result = BlazorForum.Domain.Utilities.Formatting.UrlFormatting.PrepareUrlText(urlText);

            Assert.IsType<string>(result);
            Assert.Equal("", result);
        }

        [Fact]
        void PrepareUrlTextNullStringReturnsTest()
        {
            string urlText = null;

            string result = BlazorForum.Domain.Utilities.Formatting.UrlFormatting.PrepareUrlText(urlText);

            Assert.IsType<string>(result);
            Assert.Equal("", result);
        }
    }
}
