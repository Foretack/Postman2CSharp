using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<List<SSystemsManagerClientMetricsForDesktopDevicesResponse>>(myJsonResponse);
namespace MerakiDashboard
{
    public class SSystemsManagerClientMetricsForDesktopDevicesResponse
    {
        public double CpuPercentUsed { get; set; }
        public int MemFree { get; set; }
        public int MemWired { get; set; }
        public int MemActive { get; set; }
        public int MemInactive { get; set; }
        public int NetworkSent { get; set; }
        public int NetworkReceived { get; set; }
        public int SwapUsed { get; set; }
        public DiskUsage DiskUsage { get; set; }
        public DateTime Ts { get; set; }
    }

    public class C
    {
        public int Used { get; set; }
        public int Space { get; set; }
    }

    public class DiskUsage
    {
        public C C { get; set; }
    }
}