using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<List<TencyovertimeforanetworkdeviceornetworkclientResponse>>(myJsonResponse);
namespace MerakiDashboard
{
    public class TencyovertimeforanetworkdeviceornetworkclientResponse
    {
        public DateTime StartTs { get; set; }
        public DateTime EndTs { get; set; }
        public int AvgLatencyMs { get; set; }
    }
}