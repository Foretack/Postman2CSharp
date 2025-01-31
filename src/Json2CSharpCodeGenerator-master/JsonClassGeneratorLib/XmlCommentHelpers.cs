﻿using System;
using System.Text;

namespace Postman2CSharp.Core;

public static class XmlCommentHelpers
{
    public static string ToXmlComment(string str, string indent)
    {
        if (string.IsNullOrWhiteSpace(str)) return "";
        var sb = new StringBuilder();
        var lines = str.Split(new[] {"\r\n", "\r", "\n"}, StringSplitOptions.None);
        sb.AppendLine(indent + "/// <summary>");
        foreach (var line in lines)
        {
            sb.AppendLine(indent + "/// " + line);
        }

        sb.AppendLine(indent + "/// </summary>");
        return sb.ToString();
    }
}