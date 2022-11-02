namespace Tests;

public class AccountTests
{
    [Fact]
    public void Account_creation_is_correct()
    {
        Account account = new("Jane", "Doe","jane.doe@hotmail.com", Domain.Constants.Role.Admin, "Password123@", "DIT", "Toegepaste Informatica");
        account.Firstname.ShouldBe("Jane");
        account.Lastname.ShouldBe("Doe");
        account.Email.ShouldBe("jane.doe@hotmail.com");
        account.Role.ToString().ShouldBe("Admin");
        account.PasswordHash.ShouldBe("d85fb61a933e0b8a45f88c89888502573a3d318657a576ef5529bf948b98882c");
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
        Should.Throw<ArgumentException>(() => new Account(name, name, "jane.doe@hotmail.com", Domain.Constants.Role.Admin, "password123", "DIT", "Toegepaste Informatica"));
    }

    [Theory]
    [InlineData("")]
    [InlineData("       ")]
    [InlineData("12345")]
    public void Account_password_length_smaller_than_6_is_invalid(string password)
    {
        Should.Throw<ArgumentException>(() => new Account("Jane", "Doe", "jane.doe@hotmail.com", Domain.Constants.Role.Admin, password, "DIT", "Toegepaste Informatica"));
    }

    [Theory]
    [InlineData(" ")]
    [InlineData("   ")]
    [InlineData(".")]
    [InlineData("k66")]
    [InlineData("")]
    public void Account_departement_with_characters_besides_alphabets_is_invalid(string departement)
    {
        Should.Throw<ArgumentException>(() => new Account("Jane", "Doe", "jane.doe@hotmail.com", Domain.Constants.Role.Admin, "password 123", departement, "Toegepaste Informatica"));
    }

    [Fact]
    public void Account_education_with_empty_string_is_valid()
    {
        Account account = new("Jane", "Doe", "jane.doe@hotmail.com", Domain.Constants.Role.Admin, "Password123@", "DIT", "");
        account.ShouldNotBeNull();
        account.Education.ShouldBe("");
        account.Department.ShouldBe("DIT");
    }
}
