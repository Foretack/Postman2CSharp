using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<List<ListTheVPPAccountsInTheOrganizationResponse>>(myJsonResponse);
namespace MerakiDashboard
{
    public class ListTheVPPAccountsInTheOrganizationResponse
    {
        public string Id { get; set; }
        public string VppServiceToken { get; set; }
    }
}