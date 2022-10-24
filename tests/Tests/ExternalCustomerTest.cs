using Domain;

namespace Tests;

public class ExternalCustomerTest
{
    [Fact]
    public void NewExternalCustomer_CreatedCorrectly()
    {
        ExternalCustomer customer = new("Name", "VOKA", new ContactPerson("jane", "doe", "jane.doe@hotmail.com", "+3245867952"), new ContactPerson("Alice", "Smith", "alice.smith@hotmail.com", "+3285768245"));
        Assert.NotNull(customer);
        Assert.Equal("Name", customer.Name);
        Assert.Equal("VOKA", customer.Type);
        Assert.Equal("jane.doe@hotmail.com", customer.ContactPerson.Email);
        Assert.Equal("alice.smith@hotmail.com", customer.BackupContactPerson.Email);
    }

    [Theory]
    [InlineData(" ")]
    [InlineData("   ")]
    [InlineData(".")]
    [InlineData("/")]
    [InlineData("")]
    public void NewExternalCustomer_InvalidName_ThrowsException(string name)
    {
        Assert.Throws<ArgumentException>(() => new ExternalCustomer(name, "VOKA", new ContactPerson("jane", "doe", "jane.doe@hotmail.com", "+3245867952")));
    }

    [Theory]
    [InlineData(" ")]
    [InlineData("   ")]
    [InlineData(".")]
    [InlineData("/")]
    [InlineData("")]
    public void NewExternalCustomer_InvalidType_ThrowsException(string type)
    {
        Assert.Throws<ArgumentException>(() => new ExternalCustomer("Name", type, new ContactPerson("jane", "doe", "jane.doe@hotmail.com", "+3245867952")));
    }
}
