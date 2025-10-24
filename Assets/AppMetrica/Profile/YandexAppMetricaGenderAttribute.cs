  

using System;

public class YandexAppMetricaGenderAttribute
{
    private const string AttributeName = "gender";

    public enum Gender
    {
        MALE,
        FEMALE,
        OTHER
    }

    public YandexAppMetricaUserProfileUpdate WithValue (Gender value)
    {
        return new YandexAppMetricaUserProfileUpdate (AttributeName, "withValue", null, value.ToString ());
    }

    public YandexAppMetricaUserProfileUpdate WithValueReset ()
    {
        return new YandexAppMetricaUserProfileUpdate (AttributeName, "withValueReset", null);
    }
}
