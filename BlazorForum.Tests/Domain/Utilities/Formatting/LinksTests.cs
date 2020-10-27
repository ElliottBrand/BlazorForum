using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using BlazorForum.Domain.Utilities.Formatting;

namespace BlazorForum.Tests.Domain.Utilities.Formatting
{
    public class LinksTests
    {
        [Fact]
        void GetNetworkingLinkForGitHubHandleReturnsHyperlinkTest()
        {
            string network = "GitHub";
            string userHandleOrLink = "dotnet";

            var link = Links.GetNetworkingLink(network, userHandleOrLink);

            Assert.Equal("<a href=\"https://github.com/dotnet\" target=\"_blank\">dotnet</a>", link);
        }

        [Fact]
        void GetNetworkingHttpsLinkForGitHubLinkReturnsHyperlinkTest()
        {
            string network = "GitHub";
            string userHandleOrLink = "https://github.com/dotnet";

            var link = Links.GetNetworkingLink(network, userHandleOrLink);

            Assert.Equal("<a href=\"https://github.com/dotnet\" target=\"_blank\">https://github.com/dotnet</a>", link);
        }

        [Fact]
        void GetNetworkingHttpLinkForGitHubLinkReturnsHyperlinkTest()
        {
            string network = "GitHub";
            string userHandleOrLink = "http://github.com/dotnet";

            var link = Links.GetNetworkingLink(network, userHandleOrLink);

            Assert.Equal("<a href=\"http://github.com/dotnet\" target=\"_blank\">http://github.com/dotnet</a>", link);
        }

        [Fact]
        void GetNetworkingLinkForTwitterHandleReturnsHyperlinkTest()
        {
            string network = "Twitter";
            string userHandleOrLink = "dotnet";

            var link = Links.GetNetworkingLink(network, userHandleOrLink);

            Assert.Equal("<a href=\"https://twitter.com/dotnet\" target=\"_blank\">dotnet</a>", link);
        }

        [Fact]
        void GetNetworkingLinkForLinkedInHandleReturnsHyperlinkTest()
        {
            string network = "LinkedIn";
            string userHandleOrLink = "steve-r-elliott";

            var link = Links.GetNetworkingLink(network, userHandleOrLink);

            Assert.Equal("<a href=\"https://linkedin.com/in/steve-r-elliott\" target=\"_blank\">steve-r-elliott</a>", link);
        }

        [Fact]
        void LinkifyURLStringDomainOnlyAndLinkTextReturnsHyperlinkTest()
        {
            string url = "blazorforum.net";
            string linkText = "blazorforum.net";

            var link = Links.LinkifyURLString(url, linkText);

            Assert.Equal("<a href=\"https://blazorforum.net\" target=\"_blank\">blazorforum.net</a>", link);
        }

        [Fact]
        void LinkifyURLStringDomainOnlyReturnsHyperlinkTest()
        {
            string url = "blazorforum.net";

            var link = Links.LinkifyURLString(url);

            Assert.Equal("<a href=\"https://blazorforum.net\" target=\"_blank\">blazorforum.net</a>", link);
        }

        [Fact]
        void LinkifyURLStringHttpReturnsHttpHyperlinkTest()
        {
            string url = "http://blazorforum.net";
            string linkText = "blazorforum.net";

            var link = Links.LinkifyURLString(url, linkText);

            Assert.Equal("<a href=\"http://blazorforum.net\" target=\"_blank\">blazorforum.net</a>", link);
        }
    }
}
