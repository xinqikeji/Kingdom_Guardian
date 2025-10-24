  

using System;

public class YandexAppMetricaNameAttribute
{
    private const string AttributeName = "name";

    public YandexAppMetricaUserProfileUpdate WithValue (string value)
    {
        return new YandexAppMetricaUserProfileUpdate (AttributeName, "withValue", null, value);
    }

    public YandexAppMetricaUserProfileUpdate WithValueReset ()
    {
        return new YandexAppMetricaUserProfileUpdate (AttributeName, "withValueReset", null);
    }
}
