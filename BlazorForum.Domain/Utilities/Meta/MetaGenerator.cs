using System;
using System.Text.RegularExpressions;

namespace BlazorForum.Domain.Utilities.Meta
{
    public class MetaGenerator
    {
        /// <summary>
        /// This method builds and returns a meta tag. Set "isName" to true if the meta tag uses the "name" attribute, 
        /// set it to false if the "property" attribute is needed.
        /// </summary>
        /// <param name="propertyValue"></param>
        /// <param name="contentValue"></param>
        /// <param name="isName"></param>
        /// <returns></returns>
        public static string BuildMetaTag(string propertyValue, string contentValue, bool isName)
        {
            // Add spaces after closing h tags and p tags to make sure words don't butt up next to eachother.
            contentValue = Regex.Replace(System.Net.WebUtility.HtmlDecode(contentValue), "</p>", "</p> ");
            contentValue = Regex.Replace(contentValue, "</h1>", "</h1> ");
            contentValue = Regex.Replace(contentValue, "</h2>", "</h2> ");
            contentValue = Regex.Replace(contentValue, "</h3>", "</h3> ");
            contentValue = Regex.Replace(contentValue, "</h4>", "</h4> ");
            contentValue = Regex.Replace(contentValue, "<.*?>", String.Empty);
            if (contentValue.Length > 150)
                contentValue = contentValue.Substring(0, 147).Trim() + "...";

            return "<meta name=\"" + propertyValue + "\" content=\"" + contentValue + "\">";
        }

        public static string BuildImageMetaTag(string imageFileName, string fullImageFolderPath, string fullWebFolderUrl, bool isTwitter)
        {
            if (System.IO.File.Exists(fullImageFolderPath + imageFileName))
            {
                string propValue = "og:image";

                if (isTwitter == true)
                    propValue = "twitter:image:src";

                return BuildMetaTag(propValue, fullWebFolderUrl + imageFileName, false);
            }
            else
                return null;
        }
    }
}
