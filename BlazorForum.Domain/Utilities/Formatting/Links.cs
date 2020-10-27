using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BlazorForum.Domain.Utilities.Formatting
{
    public class Links
    {
        /// <summary>
        /// Converts a username/handle for a social network or website to a clickable link. Currently allows
        /// GitHub, Twitter, and LinkedIn.
        /// </summary>
        /// <param name="network"></param>
        /// <returns></returns>
        public static string GetNetworkingLink(string network, string userHandleOrLink)
        {
            if (userHandleOrLink.StartsWith("http://") || userHandleOrLink.StartsWith("https://"))
                return LinkifyURLString(userHandleOrLink, userHandleOrLink);

            switch (network)
            {
                case "GitHub":
                    return LinkifyURLString("https://github.com/" + userHandleOrLink, userHandleOrLink);
                case "Twitter":
                    return LinkifyURLString("https://twitter.com/" + userHandleOrLink, userHandleOrLink);
                case "LinkedIn":
                    return LinkifyURLString("https://linkedin.com/in/" + userHandleOrLink, userHandleOrLink);
            }
            return null;
        }

        /// <summary>
        /// Converts a url string into a hyperlink. Unless specified in a link with "http", the default protocol will be "https".
        /// </summary>
        /// <param name="url"></param>
        /// <param name="linkText"></param>
        /// <returns></returns>
        public static string LinkifyURLString(string url, string linkText = null)
        {
            if (linkText == null) linkText = url;

            if (!url.StartsWith("http://") && !url.StartsWith("https://"))
                url = "https://" + url;

            Regex r = new Regex(@"([http|https]+?://[^\s]+)");

            return r.Replace(url, "<a href=\"$1\" target=\"_blank\">" + linkText + "</a>");
        }
    }
}
