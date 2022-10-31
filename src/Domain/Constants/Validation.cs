namespace Domain.Constants;

public static class Validation
{
    public static string Name => "^[\\w'\\-,.][^0-9_!¡?÷?¿/\\\\+=@#$%ˆ&*(){}|~<>;:[\\]]{2,}$";
    public static string Email => "^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$";
    public static string PhoneNumber =>
        "^[\\+]?[(]?[0-9]{3}[)]?[-\\s\\.]?[0-9]{3}[-\\s\\.]?[0-9]{4,6}$";
    public static string Departement => "^[a-zA-Z]+[ a-zA-Z]*"; // TODO: Fixed options?
    public static string Education => "^.{0}$|^[a-zA-Z]+[ a-zA-Z]*"; // TODO: Fixed options?
    public static string Password =>
        "^(?=.*[A-Za-z])(?=.*\\d)(?=.*[@$!%*#?&])[A-Za-z\\d@$!%*#?&]{8,}$"; // Minimum eight characters, at least one letter, one number and one special character.
    public static string Fqdn =>
        "^(?!:\\/\\/)(?=.{1,255}$)((.{1,63}\\.){1,127}(?![0-9]*$)[a-z0-9-]+\\.?)$";
}
