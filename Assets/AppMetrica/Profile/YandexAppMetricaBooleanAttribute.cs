  

using System;

public class YandexAppMetricaBooleanAttribute
{
    private const string AttributeName = "customBoolean";

    private readonly string Key;

    public YandexAppMetricaBooleanAttribute (string key) {
        Key = key;
    }

    public YandexAppMetricaUserProfileUpdate WithValue (bool value)
    {
        return new YandexAppMetricaUserProfileUpdate (AttributeName, "withValue", Key, value);
    }

    public YandexAppMetricaUserProfileUpdate WithValueIfUndefined (bool value)
    {
        return new YandexAppMetricaUserProfileUpdate (AttributeName, "withValueIfUndefined", Key, value);
    }

    public YandexAppMetricaUserProfileUpdate WithValueReset ()
    {
        return new YandexAppMetricaUserProfileUpdate (AttributeName, "withValueReset", Key);
    }
}
