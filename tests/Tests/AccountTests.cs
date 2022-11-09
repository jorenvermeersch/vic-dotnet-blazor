namespace Tests;

public class AccountTests
{
    #region Correct values
    private string _validFirstname = "Jane";
    private string _validLastname = "Doe";
    private string _validEmail = "jane.doe@hotmail.com";
    private Role _validRole = Role.Admin;
    private string _validPassword = "3Wv2J9^u9nd7!";
    private string _validDepartment = "DIT";
    private string _validEducation = "Toegepaste Informatica";
    #endregion

    private Account? _account;

    [Fact]
    public void Account_is_created()
    {
        _account = new(_validFirstname, _validLastname, _validEmail, _validRole, _validPassword, _validDepartment, _validEducation);
        _account.ShouldNotBeNull();
        _account.IsActive.ShouldBeTrue();
    }

    #region Firstname
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("\t")]
    public void Account_with_first_name_consisting_of_spaces_is_invalid(string firstname)
    {
        Should.Throw<ArgumentException>(() => new Account(firstname, _validLastname, _validEmail, _validRole, _validPassword, _validDepartment, _validEducation));
    }

    [Fact]
    public void Account_with_first_name_containing_numbers_is_invalid()
    {
        Should.Throw<ArgumentException>(() => new Account("Jane1", _validLastname, _validEmail, _validRole, _validPassword, _validDepartment, _validEducation));
    }

    [Theory]
    [InlineData("Jane!")]
    [InlineData("J@ne")]
    public void Account_with_first_name_containing_special_characters_is_invalid(string firstname)
    {
        Should.Throw<ArgumentException>(() => new Account(firstname, _validLastname, _validEmail, _validRole, _validPassword, _validDepartment, _validEducation));
    }
    #endregion

    #region Lastname
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("\t")]
    public void Account_with_last_name_consisting_of_spaces_is_invalid(string lastname)
    {
        Should.Throw<ArgumentException>(() => new Account(_validFirstname, lastname, _validEmail, _validRole, _validPassword, _validDepartment, _validEducation));
    }

    [Fact]
    public void Account_with_last_name_containing_numbers_is_invalid()
    {
        Should.Throw<ArgumentException>(() => new Account("Doe1", _validLastname, _validEmail, _validRole, _validPassword, _validDepartment, _validEducation));
    }

    [Theory]
    [InlineData("Doe!")]
    [InlineData("D@e")]
    public void Account_with__name_containing_special_characters_is_invalid(string lastname)
    {
        Should.Throw<ArgumentException>(() => new Account(_validFirstname, lastname, _validEmail, _validRole, _validPassword, _validDepartment, _validEducation));
    }
    #endregion

    #region E-mail
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("\t")]
    public void Account_with_no_email_is_invalid(string email)
    {
        Should.Throw<ArgumentException>(() => new Account(_validFirstname, _validLastname, email, _validRole, _validPassword, _validDepartment, _validEducation));
    }

    [Theory]
    [InlineData("jane.doe")]
    [InlineData("@hotmail.com")]
    [InlineData("@")]
    [InlineData("jane.doe @hotmail. com")]
    public void Account_with_incorrect_email_is_invalid(string email)
    {
        Should.Throw<ArgumentException>(() => new Account(_validFirstname, _validLastname, email, _validRole, _validPassword, _validDepartment, _validEducation));
    }
    #endregion

    #region Role
    [Theory]
    [InlineData((Role)(-1))]
    [InlineData((Role)(int.MaxValue))]
    public void Account_with_non_existent_role_is_invalid(Role role)
    {
        Should.Throw<ArgumentException>(() => new Account(_validFirstname, _validLastname, _validEmail, role, _validPassword, _validDepartment, _validEducation));
    }
    #endregion

    #region Password
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("\t")]
    public void Account_with_no_password_is_invalid(string password)
    {
        Should.Throw<ArgumentException>(() => new Account(_validFirstname, _validLastname, _validEmail, _validRole, password, _validDepartment, _validEducation));
    }

    [Fact]
    public void Account_with_short_password_is_invalid()
    {
        Should.Throw<ArgumentException>(() => new Account(_validFirstname, _validLastname, _validEmail, _validRole, "3Wv!", _validDepartment, _validEducation));
    }

    [Fact]
    public void Account_with_password_without_numbers_is_invalid()
    {
        Should.Throw<ArgumentException>(() => new Account(_validFirstname, _validLastname, _validEmail, _validRole, "Abcdefgh!", _validDepartment, _validEducation));
    }

    [Fact]
    public void Account_with_password_without_letters_is_invalid()
    {
        Should.Throw<ArgumentException>(() => new Account(_validFirstname, _validLastname, _validEmail, _validRole, "12345678!", _validDepartment, _validEducation));
    }

    [Fact]
    public void Account_with_password_without_capital_letters_is_invalid()
    {
        Should.Throw<ArgumentException>(() => new Account(_validFirstname, _validLastname, _validEmail, _validRole, "1a345678!", _validDepartment, _validEducation));
    }

    [Fact]
    public void Account_with_password_without_special_character_is_invalid()
    {
        Should.Throw<ArgumentException>(() => new Account(_validFirstname, _validLastname, _validEmail, _validRole, "1aG45678", _validDepartment, _validEducation));
    }
    #endregion

    #region Department
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("\t")]
    public void Account_without_department_is_invalid(string department)
    {
        Should.Throw<ArgumentException>(() => new Account(_validFirstname, _validLastname, _validEmail, _validRole, _validPassword, department, _validEducation));
    }
    #endregion

    #region Education
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("\t")]
    public void Account_without_education_is_invalid(string education)
    {
        Should.Throw<ArgumentException>(() => new Account(_validFirstname, _validLastname, _validEmail, _validRole, _validPassword, _validDepartment, education));
    }
    #endregion
}
