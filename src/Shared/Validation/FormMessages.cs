using System.Numerics;

namespace Shared.Validation;

public static class FormMessages
{
    public static string NOTEMPTY(string field) => string.Format("Veld \"{0}\" is verplicht.", field ?? "Unknown.");
    public static string MINLENGTH(int length) => string.Format("De minimum lengte voor dit veld is \"{0}\"", length);
    public static string GREATERTHAN(int number) => string.Format("De waarde voor dit veld moet groter zijn dan {0}.", number);
    public static string SMALLERTHANENDDATE() => string.Format("De startdatum moet voor de einddatum vallen.");
    public static string GREATERTHANDATE() => string.Format("De einddatum moet na de startdatum vallen.");
    public static string INVALIDEMAIL() => string.Format("Dit is geen geldige email.");
    public static string INVALIDPASSWORD() => string.Format("Wachtwoord moet minstens 8 karakters bevatten, met minstens 1 speciale teken, 1 cijfer en 1 hoofdletter.");
    public static string INVALIDNAME(string field) => string.Format("\"{0}\" mag geen cijfers of speciale tekens bevatten.", field);
}
