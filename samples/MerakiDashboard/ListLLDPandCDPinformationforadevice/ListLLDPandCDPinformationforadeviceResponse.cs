using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<ListLLDPAndCDPInformationForADeviceResponse>(myJsonResponse);
namespace MerakiDashboard
{
    public class ListLLDPAndCDPInformationForADeviceResponse
    {
        public string SourceMac { get; set; }
        public Ports2 Ports { get; set; }
    }

    public class Cdp2
    {
        public string DeviceId { get; set; }
        public string PortId { get; set; }
        public string Address { get; set; }
        public string SourcePort { get; set; }
    }

    public class Lldp2
    {
        public string SystemName { get; set; }
        public string PortId { get; set; }
        public string ManagementAddress { get; set; }
        public string SourcePort { get; set; }
    }

    public class Ports2
    {
        [JsonPropertyName("8")]
        public _8 _8 { get; set; }

        [JsonPropertyName("12")]
        public _12 _12 { get; set; }
    }

    public class _127
    {
        public Cdp Cdp { get; set; }
        public Lldp2 Lldp { get; set; }
    }

    public class _86
    {
        public Cdp2 Cdp { get; set; }
    }
}