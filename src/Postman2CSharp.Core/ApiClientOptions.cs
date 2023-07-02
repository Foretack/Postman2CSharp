﻿using System.Collections.Generic;
using Postman2CSharp.Core.Infrastructure;

namespace Postman2CSharp.Core
{
    public class ApiClientOptions
    {
        public bool RemoveDuplicateClasses { get; set; } = true;
        public bool RemoveDisabledHeaders { get; set; }
        public bool RemoveDisabledQueryParams { get; set; }
        public bool EnsureResponseIsSuccessStatusCode { get; set; }
        public bool MakePathCollectionVariablesFunctionParameters { get; set; }
        public bool UseCancellationTokens { get; set; }

        public List<ErrorHandlingSinks> ErrorHandlingSinks { get; set; } = new()
        {
            Infrastructure.ErrorHandlingSinks.LogException
        };

        public List<CatchExceptionTypes> CatchExceptionTypes { get; set; } = new()
        {
            Infrastructure.CatchExceptionTypes.HttpRequestException
        };

        public RootDefinition RootDefinition { get; set; } = RootDefinition.PerAuthorityPerFolder;
        public ErrorHandlingStrategy ErrorHandlingStrategy { get; set; } = ErrorHandlingStrategy.None;
        public LogLevel LogLevel { get; set; } = LogLevel.Error;
        public List<XmlCommentTypes> XmlCommentTypes { get; set; } = new()
        {
            Infrastructure.XmlCommentTypes.ApiClient,
            Infrastructure.XmlCommentTypes.QueryParameters,
            Infrastructure.XmlCommentTypes.FormData,
            Infrastructure.XmlCommentTypes.PathVariables,
            Infrastructure.XmlCommentTypes.Request,
        };

        public ApiClientOptions Clone()
        {
            return (ApiClientOptions) MemberwiseClone();
        }
    }
}
