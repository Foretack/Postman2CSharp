﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;
using Postman2CSharp.Core.Infrastructure;
using Postman2CSharp.Core.Models.PostmanCollection.Http.Request;
using Postman2CSharp.Core.Models.PostmanCollection.Http.Response;
using Xamasoft.JsonClassGenerator.Models;

namespace Postman2CSharp.Core.Utilities;

public enum CsharpPropertyType
{
    Public,
    Local,
    Private
}
public static class Utils
{
    public static string NormalizeToCsharpPropertyName(string? input, CsharpPropertyType propertyType = CsharpPropertyType.Public)
    {
        if (input == null)
        {
            return string.Empty;
        }
        var words = input.Split('_', ' ', '-')
            .Select(x => Regex.Replace(x, @"\W|_", ""))
            .Where(word => !string.IsNullOrEmpty(word))
            .Select(word => char.ToUpperInvariant(word[0]) + word[1..]);

        var result = string.Join(string.Empty, words);

        if (propertyType != CsharpPropertyType.Public && !string.IsNullOrEmpty(result))
        {
            result = char.ToLowerInvariant(result[0]) + result[1..];
            if (propertyType == CsharpPropertyType.Private)
            {
                result = "_" + result;
            }
        }
        if (result.Length > 45)
        {
            result = result[^45..];
        }
        return result;
    }

    public static string HttpClientCall(string httpMethod, DataType requestDataType, DataType responseDataType, JsonLibrary jsonLibrary)
    {
        string function;
        var suffix = jsonLibrary == JsonLibrary.SystemTextJson ? "Json" : "NewtonsoftJson";
        if (requestDataType == DataType.Json)
        {
            if (responseDataType == DataType.Json)
            {
                function = httpMethod.ToUpper() switch
                {
                    "GET" => $"Get{suffix}Async",
                    "PUT" => $"Put{suffix}Async",
                    "POST" => $"Post{suffix}Async",
                    "PATCH" => $"Patch{suffix}Async",
                    "DELETE" => $"Delete{suffix}Async",
                    _ => throw new NotImplementedException(),
                };
            }
            else
            {
                function = httpMethod.ToUpper() switch
                {
                    "GET" => $"GetAs{suffix}Async",
                    "PUT" => $"PutAs{suffix}Async",
                    "POST" => $"PostAs{suffix}Async",
                    "PATCH" => $"PatchAs{suffix}Async",
                    "DELETE" => $"DeleteAs{suffix}Async",
                    _ => throw new NotImplementedException(),
                };
            }
        }
        else
        {
            if (responseDataType == DataType.Json)
            {
                function = httpMethod.ToUpper() switch
                {
                    "GET" => $"GetFrom{suffix}Async",
                    "PUT" => $"PutFrom{suffix}Async",
                    "POST" => $"PostFrom{suffix}Async",
                    "PATCH" => $"PatchFrom{suffix}Async",
                    "DELETE" => $"DeleteFrom{suffix}Async",
                    _ => throw new NotImplementedException(),
                };
            }
            else
            {
                function = httpMethod.ToUpper() switch
                {
                    "GET" => "GetAsync",
                    "PUT" => "PutAsync",
                    "POST" => "PostAsync",
                    "PATCH" => "PatchAsync",
                    "DELETE" => "DeleteAsync",
                    _ => throw new NotImplementedException(),
                };
            }
        }

        return function;
    }

    public static string ExtractRelativePath(string? baseUrl, string? fullUrl, bool safe = true)
    {
        if (baseUrl is null || fullUrl is null)
        {
            return string.Empty;
        }
        baseUrl = baseUrl.ReplaceBrackets();
        fullUrl = fullUrl.ReplaceBrackets();
        if (baseUrl.StartsWith("http://"))
        {
            baseUrl = "https://" + baseUrl[7..];
        }
        if (fullUrl.StartsWith("http://"))
        {
            fullUrl = "https://" + fullUrl[7..];
        }
        var fullUriBuilder = new UriBuilder(fullUrl) { Scheme = Uri.UriSchemeHttps };
        var baseUriBuilder = new UriBuilder(baseUrl) { Scheme = Uri.UriSchemeHttps };

        var fullUri = fullUriBuilder.Uri;
        var baseUri = baseUriBuilder.Uri;
        if (!fullUri.AbsoluteUri.StartsWith(baseUri.AbsoluteUri))
        {
            if (safe)
            {
                throw new ArgumentException("The full URI must start with the base URI.");
            }
            else return string.Empty;
        }


        var relativePath = fullUri.AbsolutePath[baseUri.AbsolutePath.Length..].Trim('/');
        relativePath = relativePath.AddBackBrackets();
        return relativePath;
    }

    public static string AddBackBrackets(this string str)
    {
        str = str.Replace("leftcurly", "{"); // better way to do this?
        str = str.Replace("rightcurly", "}");
        str = str.Replace("%7B", "{"); // Add brackets back in if they got replaced in the process
        str = str.Replace("%7D", "}");

        // Replace any character with '-' in front of it with its uppercase version
        str = Regex.Replace(str, @"(\{[^}]*?)-([a-z])([^}]*?\})", m =>
        {
            var start = m.Groups[1].Value;
            var letter = m.Groups[2].Value.ToUpperInvariant();
            var end = m.Groups[3].Value;
            return start + letter + end;
        });
        return str;
    }

    // The regex is to ensure variables in curly brackets don't become lower cased.
    public static string ReplaceBrackets(this string str)
    {
        // Find uppercase letters between brackets and add '-' before them
        str = Regex.Replace(str, @"(\{[^}]*)([A-Z])([^}]*\})", m =>
        {
            var start = m.Groups[1].Value;
            var letter = m.Groups[2].Value;
            var end = m.Groups[3].Value;
            return start + "-" + letter.ToLowerInvariant() + end;
        });

        str = str.Replace("{", "leftcurly");
        str = str.Replace("}", "rightcurly");

        return str;
    }


    /// <summary>
    /// Gets the largest common base from a list of strings
    /// </summary>
    /// <param name="strings"></param>
    /// <returns></returns>
    public static string? GetCommonBase(List<string> strings)
    {
        if (!strings.Any() || strings.Any(string.IsNullOrWhiteSpace))
            return null;

        var commonBase = strings[0];

        for (var i = 1; i < strings.Count; i++)
        {
            commonBase = string.Concat(commonBase.TakeWhile((c, index) => index < strings[i].Length && c == strings[i][index]));
        }

        return commonBase.Length > 4 ? commonBase : null;
    }

    public static string? GetLongestSubstring(List<string> strings)
    {
        if (!strings.Any() || strings.Any(string.IsNullOrWhiteSpace))
            return null;

        string? longestCommonSubstring = null;
        var minLengthStr = strings.OrderBy(s => s.Length).First();

        for (var len = minLengthStr.Length; len > 0; len--)
        {
            for (var strIndex = 0; strIndex <= minLengthStr.Length - len; strIndex++)
            {
                var currentSubstring = minLengthStr.Substring(strIndex, len);

                if (char.IsUpper(currentSubstring[0]) && strings.All(str => str.Contains(currentSubstring)))
                {
                    if (longestCommonSubstring == null || currentSubstring.Length > longestCommonSubstring.Length)
                        longestCommonSubstring = currentSubstring;
                }
            }
        }

        return longestCommonSubstring?.Length > 4 ? longestCommonSubstring : null;
    }

    public enum NamingPolicy
    {
        Unknown,
        CamelCase,
        PascalCase,
        SnakeCase
    }

    public static NamingPolicy DetectNamingPolicy(string jsonString)
    {
        var json = JObject.Parse(jsonString);

        foreach (var property in json.Properties())
        {
            string name = property.Name;

            if (name.All(c => char.IsLower(c) || c == '_')) // snake_case check
            {
                if (name.Contains('_'))
                {
                    return NamingPolicy.SnakeCase;
                }
                else
                {
                    return NamingPolicy.Unknown;
                }
            }

            if (char.IsUpper(name[0])) // PascalCase check
            {
                return NamingPolicy.PascalCase;
            }
            else if (char.IsLower(name[0])) // camelCase check
            {
                return NamingPolicy.CamelCase;
            }
        }

        return NamingPolicy.Unknown;
    }

    public static string PadLeft(string sourceCode, int indentCount = 1)
    {
        using StringReader reader = new(sourceCode);
        var paddedParagraph = string.Empty;
        // Read each line, add padding, and join them back
        while (reader.ReadLine() is { } line)
        {
            var paddedLine = line.PadLeft(line.Length + 4 * indentCount);
            paddedParagraph += paddedLine + Environment.NewLine;
        }
        return paddedParagraph;
    }

    public static string HtmlToPlainText(this string html)
    {
        const string tagWhiteSpace = @"(>|$)(\W|\n|\r)+<";//matches one or more (white space or line breaks) between '>' and '<'
        const string stripFormatting = @"<[^>]*(>|$)";//match any character between '<' and '>', even when end tag is missing
        const string lineBreak = @"<(br|BR)\s{0,1}\/{0,1}>";//matches: <br>,<br/>,<br />,<BR>,<BR/>,<BR />
        var lineBreakRegex = new Regex(lineBreak, RegexOptions.Multiline);
        var stripFormattingRegex = new Regex(stripFormatting, RegexOptions.Multiline);
        var tagWhiteSpaceRegex = new Regex(tagWhiteSpace, RegexOptions.Multiline);

        var text = html;
        //Decode html specific characters
        text = System.Net.WebUtility.HtmlDecode(text);
        //Remove tag whitespace/line breaks
        text = tagWhiteSpaceRegex.Replace(text, "><");
        //Replace <br /> with line breaks
        text = lineBreakRegex.Replace(text, Environment.NewLine);
        //Strip formatting
        text = stripFormattingRegex.Replace(text, string.Empty);

        return text;
    }

    public static string FixXmlCommentsAfterCodeAnalysis(this string sourceCode, int indent)
    {
        return ReplaceExceptFirst(sourceCode, "/// <summary>", Environment.NewLine + Consts.Indent(indent) + "/// <summary>");
    }

    private static string ReplaceExceptFirst(string sourceCode, string oldValue, string newValue)
    {
        var pos = sourceCode.IndexOf(oldValue, StringComparison.Ordinal);
        if (pos == -1)
        {
            return sourceCode;
        }

        var nextPos = sourceCode.IndexOf(oldValue, pos + oldValue.Length, StringComparison.Ordinal);

        if (nextPos == -1)
        {
            return sourceCode;
        }

        return sourceCode[..nextPos] + sourceCode[nextPos..].Replace(oldValue, newValue);
    }

    public static string NormalizeWhitespace(string sourceCode)
    {
        sourceCode = sourceCode.TrimEnd();

        sourceCode = sourceCode.Replace("\r\n", "\n").Replace("\r", "\n").Replace("\n", "\r\n");
        return sourceCode;
    }

    public static DataType GetRequestDataType(Request request)
    {
        if (request.Body is { Mode: "raw", Options.Raw.Language: "json" })
        {
            return DataType.Json;
        }
        else if (request.Body is { Mode: "raw", Options: null })
        {
            return DataType.Json;
        }
        if (request.Body is { Mode: "raw", Options.Raw.Language: "xml" })
        {
            return DataType.Xml;
        }
        if (request.Body is { Mode: "raw", Options.Raw.Language: "html" })
        {
            return DataType.Html;
        }
        if (request.Body is { Mode: "raw", Options.Raw.Language: "text" })
        {
            return DataType.Text;
        }
        if (request.Body is { Mode: "urlencoded", Urlencoded.Count: > 0 })
        {
            return DataType.SimpleFormData;
        }
        if (request.Body is { Mode: "formdata", Formdata.Count: > 0 })
        {
            return request.Body.Formdata.Any(x => x.FormDataType == FormDataType.File || x.Src != null) ? DataType.ComplexFormData : DataType.SimpleFormData;
        }
        else if (request.Body is { Mode: "formdata", Formdata.Count: 0 })
        {
            return DataType.QueryOnly;
        }
        if (request.Body == null)
        {
            return DataType.QueryOnly;
        }
        if (request.Body.Mode == "file")
        {
            return DataType.Binary;
        }

        if (request.Body.Mode == "graphql")
        {
            return DataType.GraphQl;
        }
        else
        {
            if (request.Body.Mode == null)
            {
                throw new ArgumentException(nameof(request.Body.Mode), $"Request mode not supported for request {request.Url.Raw}: null");
            }
            else
            {
                throw new ArgumentException(nameof(request.Body.Mode), $"Request mode not supported for request {request.Url.Raw}: {request.Body.Mode}");
            }
        }
    }

    private static readonly List<string> PossibleJsonContentTypes = new() { "application/json", "text" };
    public static DataType GetResponseDataType(Response? response)
    {
        if (response?.Header?.Any(x => x.Key.ToLower() == "content-type" && PossibleJsonContentTypes.Any(type => x.Value.ToLower().Contains(type))) ?? false)
        {
            return DataType.Json;
        }

        return DataType.Null;
    }
}