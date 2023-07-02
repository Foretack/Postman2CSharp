using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

// Root myDeserializedClass = JsonConvert.DeserializeObject<AutocompleteResponse>(myJsonResponse);
namespace Postman2CSharp
{
    public class AutocompleteResponse
    {
        public List<Predictions2> Predictions { get; set; }
        public string Status { get; set; }
    }

    public class Predictions2
    {
        public string Description { get; set; }
        public List<MatchedSubstrings> MatchedSubstrings { get; set; }
        public string PlaceId { get; set; }
        public string Reference { get; set; }
        public StructuredFormatting2 StructuredFormatting { get; set; }
        public List<Terms> Terms { get; set; }
        public List<string> Types { get; set; }
    }

    public class StructuredFormatting2
    {
        public string MainText { get; set; }
        public List<MainTextMatchedSubstrings> MainTextMatchedSubstrings { get; set; }
        public string SecondaryText { get; set; }
    }
}