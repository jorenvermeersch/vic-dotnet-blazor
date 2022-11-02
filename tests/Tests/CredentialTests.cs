namespace Tests;

public class CredentialTests
{
    [Fact]
    public void Credential_creation_is_correct()
    {
        Credentials credential = new("Jane.Doe", "janedoespasswoord123");
        credential.Username.ShouldBe("Jane.Doe");
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("   ")]
    public void Credential_empty_username_is_invalid(string username)
    {
        Should.Throw<ArgumentException>(() => new Credentials(username, "1425375fgk"));
    }
}
