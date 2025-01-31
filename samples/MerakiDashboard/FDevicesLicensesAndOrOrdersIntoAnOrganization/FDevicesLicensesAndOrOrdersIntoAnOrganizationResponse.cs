using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<FDevicesLicensesAndOrOrdersIntoAnOrganizationResponse>(myJsonResponse);
namespace MerakiDashboard
{
    public class FDevicesLicensesAndOrOrdersIntoAnOrganizationResponse
    {
        public List<string> Orders { get; set; }
        public List<string> Serials { get; set; }
        public List<Licenses> Licenses { get; set; }
    }
}