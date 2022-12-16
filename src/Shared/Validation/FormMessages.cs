namespace Shared.Validation;

public static class ValidationMessages
{
    public static string NotEmpty(string field)
    {
        return $"{field} is verplicht.";
    }

    public static string MIN_LENGTH(int length)
    {
        return string.Format("De minimum lengte voor dit veld is \"{0}\"", length);
    }

    public static string GREATER_THAN(int number)
    {
        return string.Format("De waarde voor dit veld moet groter zijn dan {0}.", number);
    }

    public static string SMALLER_THAN_END_DATE()
    {
        return string.Format("De startdatum moet voor de einddatum vallen.");
    }

    public static string GREATER_THAN_DATE()
    {
        return string.Format("De einddatum moet na de startdatum vallen.");
    }

    public static string INVALID_EMAIL()
    {
        return string.Format("Dit is geen geldig e-mailadres.");
    }

    public static string INVALID_PASSWORD()
    {
        return string.Format("Wachtwoord moet minstens 8 karakters bevatten, met minstens 1 speciale teken, 1 cijfer en 1 hoofdletter.");
    }

    public static string INVALID_NAME(string field)
    {
        return string.Format("\"{0}\" mag geen cijfers of speciale tekens bevatten.", field);
    }
}
