  

using System;

public class YandexAppMetricaStringAttribute
{
    private const string AttributeName = "customString";

    private readonly string Key;

    public YandexAppMetricaStringAttribute (string key)
    {
        Key = key;
    }

    public YandexAppMetricaUserProfileUpdate WithValue (string value)
    {
        return new YandexAppMetricaUserProfileUpdate (AttributeName, "withValue", Key, value);
    }

    public YandexAppMetricaUserProfileUpdate WithValueIfUndefined (string value)
    {
        return new YandexAppMetricaUserProfileUpdate (AttributeName, "withValueIfUndefined", Key, value);
    }

    public YandexAppMetricaUserProfileUpdate WithValueReset ()
    {
        return new YandexAppMetricaUserProfileUpdate (AttributeName, "withValueReset", Key);
    }
}
