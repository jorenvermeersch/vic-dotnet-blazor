using Microsoft.IdentityModel.Tokens;

namespace Shared.Validation;

public static class ValidationMessages
{
    public static string NotEmpty(string field)
    {
        return $"{field} is verplicht.";
    }

    public static string MinimumLength(string field, int length)
    {
        return $"{field} heeft een minimum lengte van {length}.";
    }

    public static string GreaterThanOrEqual(string field, int number, string unit = "")
    {
        unit = !unit.IsNullOrEmpty() ? $" {unit}" : "";
        return $"{field} moet groter of gelijk zijn aan {number}{unit}.";
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
    public static string UnknownCustomerType => "Onbekende soort.";
    public static string UnknownInstitution => "Onbekend instituut.";
    public static string InvalidEmailAddress => "E-mailadres is niet geldig.";
    public static string InvalidPhoneNumber => "Telefoonnummer is niet geldig.";
    public static string InvalidPassword =>
        "Wachtwoord moet minstens acht karakters bevatten, met minstens één speciale teken, één cijfer en één hoofdletter.";
}
