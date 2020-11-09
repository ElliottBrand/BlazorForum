using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BlazorForum.Tests.Domain.Utilities.Formatting
{
    public class DateTimeFormattingTests
    {
        [Fact]
        void RelativeDateAgoReturnsCorrectSecondsAgoTest()
        {
            DateTime secondsAgo = DateTime.Now.AddSeconds(-5).ToUniversalTime();

            string result = BlazorForum.Domain.Utilities.Formatting.DateTimeFormatting.RelativeDateAgo(secondsAgo);

            Assert.IsType<string>(result);
            Assert.Equal("5 seconds ago", result);
        }

        [Fact]
        void RelativeDateAgoReturnsCorrectMinutesAgoTest()
        {
            DateTime minutesAgo = DateTime.Now.AddMinutes(-5).ToUniversalTime();

            string result = BlazorForum.Domain.Utilities.Formatting.DateTimeFormatting.RelativeDateAgo(minutesAgo);

            Assert.IsType<string>(result);
            Assert.Equal("5 minutes ago", result);
        }

        [Fact]
        void RelativeDateAgoReturnsCorrectHourAgoTest()
        {
            DateTime hourAgo = DateTime.Now.AddHours(-1).ToUniversalTime();

            string result = BlazorForum.Domain.Utilities.Formatting.DateTimeFormatting.RelativeDateAgo(hourAgo);

            Assert.IsType<string>(result);
            Assert.Equal("an hour ago", result);
        }

        [Fact]
        void RelativeDateAgoReturnsCorrectHoursAgoTest()
        {
            DateTime hoursAgo = DateTime.Now.AddHours(-5).ToUniversalTime();

            string result = BlazorForum.Domain.Utilities.Formatting.DateTimeFormatting.RelativeDateAgo(hoursAgo);

            Assert.IsType<string>(result);
            Assert.Equal("5 hours ago", result);
        }

        [Fact]
        void RelativeDateAgoReturnsCorrectDaysAgoTest()
        {
            DateTime daysAgo = DateTime.Now.AddDays(-55).ToUniversalTime();

            string result = BlazorForum.Domain.Utilities.Formatting.DateTimeFormatting.RelativeDateAgo(daysAgo);

            Assert.IsType<string>(result);
            Assert.Equal("55 days ago", result);
        }

        [Fact]
        void RelativeDateAgoReturnsCorrectDaysYearAgoTest()
        {
            DateTime daysAgo = DateTime.Now.AddDays(-365).ToUniversalTime();

            string result = BlazorForum.Domain.Utilities.Formatting.DateTimeFormatting.RelativeDateAgo(daysAgo);

            Assert.IsType<string>(result);
            Assert.Equal("1 year ago", result);
        }

        [Fact]
        void RelativeDateAgoReturnsCorrectYearAgoTest()
        {
            DateTime yearsAgo = DateTime.Now.AddYears(-5).ToUniversalTime();

            string result = BlazorForum.Domain.Utilities.Formatting.DateTimeFormatting.RelativeDateAgo(yearsAgo);

            Assert.IsType<string>(result);
            Assert.Equal("5 years ago", result);
        }
    }
}
