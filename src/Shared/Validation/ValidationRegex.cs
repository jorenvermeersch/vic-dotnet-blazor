using System.Text.RegularExpressions;

namespace Shared.Validation;


public static class ValidationRegex
{
    public static Regex Name => new("^[a-zA-Z]{2}[- a-zA-Zéèçàùëüöï]*$");
    public static Regex Email => new("^\\S+@\\S+\\.\\S+$");
    public static Regex PhoneNumber =>
        new("^.{0}$|^[\\+]?[(]?[0-9]{3}[)]?[-\\s\\.]?[0-9]{3}[-\\s\\.]?[0-9]{4,6}$");
    public static Regex Departement => new("^[a-zA-Z]+[ a-zA-Z]*");
    public static Regex Education => new("^.{0}$|^[a-zA-Z]+[ a-zA-Z]*");
    public static Regex Password =>
        new("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$"); // Minimum eight characters, at least one letter, one number and one special character.
    public static Regex Fqdn =>
        new("^(?!:\\/\\/)(?=.{1,255}$)((.{1,63}\\.){1,127}(?![0-9]*$)[a-z0-9-]+\\.?)$");
}
