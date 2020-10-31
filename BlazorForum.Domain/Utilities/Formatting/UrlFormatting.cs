using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace BlazorForum.Domain.Utilities.Formatting
{
    public class UrlFormatting
    {
        private static Regex purifyUrlRegex = new Regex("[^-a-zA-Z0-9_ ]", RegexOptions.Compiled);
        private static Regex dashesRegex = new Regex("[-_ ]+", RegexOptions.Compiled);

        public static string PrepareUrlText(string urlText)
        {
            if (urlText == null)
                return string.Empty;

            urlText = purifyUrlRegex.Replace(urlText, "");
            urlText = urlText.Trim();
            urlText = dashesRegex.Replace(urlText, "-");  
            return urlText.ToLower();
        }
    }
}
