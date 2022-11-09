namespace Tests;

public class CredentialTests
{

    #region Correct values
    private string _validUsername = "jane.doe";
    private string _validPassword = "3Wv2J9^u9nd7!";
    private string _validRole = "admin";
    #endregion

    private Credentials? _credentials;

    [Fact]
    public void Credentials_are_created()
    {
        _credentials = new(_validUsername, _validPassword, _validRole);
        _credentials.Username.ShouldBe(_validUsername);
        _credentials.PasswordHash.ShouldNotBeEmpty();
        _credentials.Role.ShouldBe(_validRole);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("\t")]
    public void Show_error_when_username_is_invalid(string username)
    {
        Should.Throw<ArgumentException>(() => new Credentials(username, _validPassword, _validRole));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("\t")]
    [InlineData("3Wv!")] // To short.
    [InlineData("12345678!")] // No letter. 
    [InlineData("Abcdefgh!")] // No number. 
    [InlineData("3Wv2J9u9nd7")] // No special character. 
    public void Show_error_when_password_is_invalid(string password)
    {
        Should.Throw<ArgumentException>(() => new Credentials(_validUsername, password, _validRole));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("\t")]
    public void Show_error_when_role_is_invalid(string role)
    {
        Should.Throw<ArgumentException>(() => new Credentials(_validUsername, _validPassword, role));
    }
}
