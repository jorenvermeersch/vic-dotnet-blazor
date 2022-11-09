namespace Tests;

public class CredentialTests
{

    #region Correct values
    private string _correctUsername = "jane.doe";
    private string _correctPassword = "3Wv2J9^u9nd7!";
    private string _correctRole = "admin";
    #endregion

    private Credentials? _credentials;

    [Fact]
    public void Credentials_are_created()
    {
        _credentials = new(_correctUsername, _correctPassword, _correctRole);
        _credentials.Username.ShouldBe(_correctUsername);
        _credentials.PasswordHash.ShouldNotBeEmpty();
        _credentials.Role.ShouldBe(_correctRole);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("\t")]
    public void Username_is_invalid(string username)
    {
        Should.Throw<ArgumentException>(() => new Credentials(username, _correctPassword, _correctRole));
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
    public void Password_is_invalid(string password)
    {
        Should.Throw<ArgumentException>(() => new Credentials(_correctUsername, password, _correctRole));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("\t")]
    public void Role_is_invalid(string role)
    {
        Should.Throw<ArgumentException>(() => new Credentials(_correctUsername, _correctPassword, role));
    }
}
