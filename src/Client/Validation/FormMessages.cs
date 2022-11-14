using System.Numerics;

namespace Client.Validation;

public static class FormMessages
{
    public static string NOTEMPTY(string field) => string.Format("Veld \"{0}\" is verplicht", field ?? "Unknown");
    public static string MINLENGTH(int length) => string.Format("De minimum lengte voor dit veld is \"{0}\"", length);
    public static string GREATERTHAN(int number) => string.Format("De waarde voor dit veld moet groter zijn dan {0}", number);
    public static string SMALLERTHANENDDATE() => string.Format("De startdatum moet voor de einddatum vallen");
    public static string GREATERTHANDATE() => string.Format("De einddatum moet na de startdatum vallen");

}
