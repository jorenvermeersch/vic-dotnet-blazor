namespace Tests;

public class CredentialTests
{
    [Fact]
    public void Credential_creation_is_correct()
    {
        Credential credential = new("Jane.Doe", "janedoespasswoord123");
        credential.Username.ShouldBe("Jane.Doe");
        credential.Password.ShouldBe("janedoespasswoord123");
    }

    [Theory]
    [InlineData("")]
    [InlineData("       ")]
    [InlineData("12345")]
    public void Credential_password_length_smaller_than_6_is_invalid(string password)
    {
        Should.Throw<ArgumentException>(() => new Credential("JaneDoe", password));
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("   ")]
    public void Credential_empty_username_is_invalid(string username)
    {
        Should.Throw<ArgumentException>(() => new Credential(username, "1425375fgk"));
    }
}
