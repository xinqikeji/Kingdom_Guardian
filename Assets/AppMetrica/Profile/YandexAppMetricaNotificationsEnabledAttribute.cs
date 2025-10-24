  

using System;

public class YandexAppMetricaNotificationsEnabledAttribute
{
    private const string AttributeName = "notificationsEnabled";

    public YandexAppMetricaUserProfileUpdate WithValue (bool value)
    {
        return new YandexAppMetricaUserProfileUpdate (AttributeName, "withValue", null, value);
    }

    public YandexAppMetricaUserProfileUpdate WithValueReset ()
    {
        return new YandexAppMetricaUserProfileUpdate (AttributeName, "withValueReset", null);
    }
}
