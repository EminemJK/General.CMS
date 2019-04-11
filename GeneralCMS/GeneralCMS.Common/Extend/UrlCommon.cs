using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace GeneralCMS.Common.Extend
{
    public static class UrlCommon
    {
        public static string CreateUrlPath(string localhost, string url)
        {
            if (url.ToLower().StartsWith("http:"))
                return url;
            return new Uri(new Uri(localhost), url).ToString();
        }

        public static string GetUrlPath(string localhost, string url)
        {
            return url.Replace(localhost, "");
        }
    }
}
