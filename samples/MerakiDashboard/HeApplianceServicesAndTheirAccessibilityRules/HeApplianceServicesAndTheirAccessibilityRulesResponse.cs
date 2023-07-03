using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<List<HeApplianceServicesAndTheirAccessibilityRulesResponse>>(myJsonResponse);
namespace MerakiDashboard
{
    public class HeApplianceServicesAndTheirAccessibilityRulesResponse
    {
        public string Service { get; set; }
        public string Access { get; set; }
        public List<string> AllowedIps { get; set; }
    }
}