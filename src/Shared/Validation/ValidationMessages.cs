namespace Shared.Validation;

public static class ValidationMessages
{
    public static string NotEmpty(string field)
    {
        return $"{field} is verplicht.";
    }

    public static string MinimumLength(int length)
    {
        return $"Minimum lengte van {length}";
    }

    public static string GreatherThan(int number)
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







    public static string InvalidName(string field)
    {
        return $"{field} mag geen cijfers of speciale tekens bevatten.";
    }

    public static string UnknownRole => "Onbekende rol.";
    public static string InvalidEmailAddress => "Dit is geen geldig e-mailadres.";
    public static string InvalidPassword =>
        "Wachtwoord moet minstens 8 karakters bevatten, met minstens 1 speciale teken, 1 cijfer en 1 hoofdletter.";
}
