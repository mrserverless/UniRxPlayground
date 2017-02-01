using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Amazon
{
    public class AWSSDKUtils
    {
        internal static Dictionary<int, string> RFCEncodingSchemes = new Dictionary<int, string>()
        {
            {
                3986,
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-_.~"
            },
            {
                1738,
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-_."
            }
        };
        private static string ValidPathCharacters = DetermineValidPathCharacters();
        public const string ISO8601BasicDateTimeFormat = "yyyyMMddTHHmmssZ";
        public const string ISO8601BasicDateFormat = "yyyyMMdd";

        public static DateTime CorrectedUtcNow
        {
            get
            {
                DateTime utcNow = DateTime.UtcNow;
                if (AWSConfigs.CorrectForClockSkew)
                    utcNow += AWSConfigs.ClockOffset;
                return utcNow;
            }
        }

        internal static string GetParametersAsString(IDictionary<string, string> parameters)
        {
            string[] array = new string[parameters.Keys.Count];
            parameters.Keys.CopyTo
                (array, 0);
            Array.Sort
                (array);

            StringBuilder stringBuilder = new StringBuilder(512);
            foreach
            (string index in
                array)
            {
                string parameter = parameters[index];
                if (parameter != null)
                {
                    stringBuilder.Append(index);
                    stringBuilder.Append('=');
                    stringBuilder.Append(UrlEncode(parameter, false));
                    stringBuilder.Append('&');
                }
            }

            string str = stringBuilder.ToString();
            if
                (str.Length == 0)
                return
                    string.Empty;
            return
                str.Remove
                    (str.Length - 1);
        }

        public static string UrlEncode(string data, bool path)
        {
            return UrlEncode(3986, data, path);
        }

        public static string UrlEncode(int rfcNumber, string data, bool path)
        {
            StringBuilder stringBuilder = new StringBuilder(data.Length * 2);
            string str1;
            if (!RFCEncodingSchemes.TryGetValue(rfcNumber, out str1))
                str1 = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-_.~";
            string str2 = str1 + (path ? ValidPathCharacters : "");
            foreach (char ch in Encoding.UTF8.GetBytes(data))
            {
                if (str2.IndexOf(ch) != -1)
                    stringBuilder.Append(ch);
                else
                    stringBuilder.Append("%")
                        .Append(string.Format(CultureInfo.InvariantCulture, "{0:X2}", (int) ch));
            }
            return stringBuilder.ToString();
        }

        public static string CanonicalizeResourcePath(Uri endpoint, string resourcePath)
        {
            if (endpoint != null)
            {
                string a = endpoint.AbsolutePath;
                if (string.IsNullOrEmpty(a) || string.Equals(a, "/", StringComparison.Ordinal))
                    a = string.Empty;
                if (!string.IsNullOrEmpty(resourcePath) && resourcePath.StartsWith("/", StringComparison.Ordinal))
                    resourcePath = resourcePath.Substring(1);
                if (!string.IsNullOrEmpty(resourcePath))
                    a = a + "/" + resourcePath;
                resourcePath = a;
            }
            if (string.IsNullOrEmpty(resourcePath))
                return "/";
            return string.Join("/", resourcePath.Split(new char[1]
            {
                '/'
            }, StringSplitOptions.None).Select(segment => UrlEncode(segment, false)).ToArray());
        }

        private static string DetermineValidPathCharacters()
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (char ch in "/:'()!*[]$")
            {
                string str = Uri.EscapeUriString(ch.ToString());
                if (str.Length == 1 && str[0] == ch)
                    stringBuilder.Append(ch);
            }
            return stringBuilder.ToString();

        }

        public static string ToHex(byte[] data, bool lowercase)
        {
            StringBuilder stringBuilder = new StringBuilder();
            for (int index = 0; index < data.Length; ++index)
                stringBuilder.Append(data[index].ToString(lowercase ? "x2" : "X2", (IFormatProvider) CultureInfo.InvariantCulture));
            return stringBuilder.ToString();
        }
    }


}