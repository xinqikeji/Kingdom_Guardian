  

using System;

public class YandexAppMetricaCounterAttribute
{
    private const string AttributeName = "customCounter";

    private readonly string Key;

    public YandexAppMetricaCounterAttribute (string key)
    {
        Key = key;
    }

    public YandexAppMetricaUserProfileUpdate WithDelta (double value)
    {
        return new YandexAppMetricaUserProfileUpdate (AttributeName, "withDelta", Key, value);
    }
}
