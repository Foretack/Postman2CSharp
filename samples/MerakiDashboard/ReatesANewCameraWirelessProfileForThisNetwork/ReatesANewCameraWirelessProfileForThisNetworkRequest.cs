using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<ReatesANewCameraWirelessProfileForThisNetworkRequest>(myJsonResponse);
namespace MerakiDashboard
{
    public class ReatesANewCameraWirelessProfileForThisNetworkRequest
    {
        public string Name { get; set; }
        public Ssid Ssid { get; set; }
        public Identity Identity { get; set; }
    }

    public class Ssid
    {
        public string Name { get; set; }
        public string AuthMode { get; set; }
        public string EncryptionMode { get; set; }
        public string Psk { get; set; }
    }

    public class Identity
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}