namespace Tests;

public class AccountTests
{
    [Fact]
    public void Account_creation_is_correct()
    {
        Account account = new("Jane", "Doe", Domain.Constants.Role.Admin, "password123", "DIT", "Toegepaste Informatica");
        account.Firstname.ShouldBe("Jane");
        account.Lastname.ShouldBe("Doe");
        account.Role.ToString().ShouldBe("Admin");
        account.PasswordHash.ShouldBe("ef92b778bafe771e89245b89ecbc08a44a4e166c06659911881f383d4473e94f");
        account.Department.ShouldBe("DIT");
        account.Education.ShouldBe("Toegepaste Informatica");
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData("abc142")]
    [InlineData("1435")] 
    [InlineData("abn/djik")]
    [InlineData("abn.djik")]
    public void Account_firstAndLastName_with_characters_besides_alphabets_is_invalid(string name)
    {
        Should.Throw<ArgumentException>(() => new Account(name, name, Domain.Constants.Role.Admin, "password123", "DIT", "Toegepaste Informatica"));
    }

    [Theory]
    [InlineData("")]
    [InlineData("       ")]
    [InlineData("12345")]
    public void Account_password_length_smaller_than_6_is_invalid(string password)
    {
        Should.Throw<ArgumentException>(() => new Account("Jane", "Doe", Domain.Constants.Role.Admin, password, "DIT", "Toegepaste Informatica"));
    }

    [Theory]
    [InlineData(" ")]
    [InlineData("   ")]
    [InlineData(".")]
    [InlineData("k66")]
    [InlineData("")]
    public void Account_departement_with_characters_besides_alphabets_is_invalid(string departement)
    {
        Should.Throw<ArgumentException>(() => new Account("Jane", "Doe", Domain.Constants.Role.Admin, "password 123", departement, "Toegepaste Informatica"));
    }

    [Fact]
    public void Account_education_with_empty_string_is_valid()
    {
        Account account = new("Jane", "Doe", Domain.Constants.Role.Admin, "password123", "DIT", "");
        account.ShouldNotBeNull();
        account.Education.ShouldBe("");
        account.Department.ShouldBe("DIT");
    }
}
