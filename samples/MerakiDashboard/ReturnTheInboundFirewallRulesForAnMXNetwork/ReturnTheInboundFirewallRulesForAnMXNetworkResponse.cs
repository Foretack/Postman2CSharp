using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<ReturnTheInboundFirewallRulesForAnMXNetworkResponse>(myJsonResponse);
namespace MerakiDashboard
{
    public class ReturnTheInboundFirewallRulesForAnMXNetworkResponse
    {
        public List<Rules> Rules { get; set; }
        public bool SyslogDefaultRule { get; set; }
    }
}