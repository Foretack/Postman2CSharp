using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<UpdateTheCellularFirewallRulesOfAnMXNetworkResponse>(myJsonResponse);
namespace MerakiDashboard
{
    public class UpdateTheCellularFirewallRulesOfAnMXNetworkResponse
    {
        public List<Rules> Rules { get; set; }
    }
}