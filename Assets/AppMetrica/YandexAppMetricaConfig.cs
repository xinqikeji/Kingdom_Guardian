 

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public struct YandexAppMetricaConfig
{
    [System.Serializable]
    public struct Coordinates
    {
        public double Latitude { get; set; }

        public double Longitude { get; set; }
    }

    public string ApiKey { get; private set; }

    public string AppVersion { get; set; }

    public Coordinates? Location { get; set; }

    public int? SessionTimeout { get; set; }

    public bool? CrashReporting { get; set; }

    public bool? LocationTracking { get; set; }

    public bool? Logs { get; set; }

    public bool? InstalledAppCollecting { get; set; }

    public bool? HandleFirstActivationAsUpdate { get; set; }

    public YandexAppMetricaPreloadInfo? PreloadInfo { get; set; }

    public bool? StatisticsSending { get; set; }
    
    /// <summary>
    ///  Only iOS
    /// </summary>
    public bool? AppForKids { get; set; }

    public YandexAppMetricaConfig (string apiKey)
    {
        ApiKey = apiKey;
        AppVersion = null;
        Location = null;
        SessionTimeout = null;
        CrashReporting = null;
        LocationTracking = null;
        Logs = null;
        InstalledAppCollecting = null;
        HandleFirstActivationAsUpdate = null;
        PreloadInfo = null;
        StatisticsSending = null;
        AppForKids = null;
    }
}

[System.Serializable]
public struct YandexAppMetricaPreloadInfo
{
    public string TrackingId { get; private set; }

    public Dictionary<string, string> AdditionalInfo { get; private set; }

    public YandexAppMetricaPreloadInfo (string trackingId)
    {
        TrackingId = trackingId;
        AdditionalInfo = new Dictionary<string, string> ();
    }
}
