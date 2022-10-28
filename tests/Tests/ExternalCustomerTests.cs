namespace Tests;

public class ExternalCustomerTests
{

    public void test()
    {
        ContactPerson p = new("jane", "doe", "jane.doe@hotmail.com", "+3245867952");
        Customer customer = new(p, p);
    }

    [Fact]
    public void ExternalCustomer_creation_is_correct()
    {
        ExternalCustomer customer = new("Name", "VOKA", new ContactPerson("jane", "doe", "jane.doe@hotmail.com", "+3245867952"), new ContactPerson("Alice", "Smith", "alice.smith@hotmail.com", "+3285768245"));
        customer.ShouldNotBeNull();
        customer.Name.ShouldBe("Name");
        customer.Type.ShouldBe("VOKA");
        customer.ContactPerson.Email.ShouldBe("jane.doe@hotmail.com");
        customer?.BackupContactPerson?.Email.ShouldBe("alice.smith@hotmail.com");
    }

    [Theory]
    [InlineData(" ")]
    [InlineData("   ")]
    [InlineData(".")]
    [InlineData("/")]
    [InlineData("")]
    [InlineData("io255")]
    [InlineData("255")]
    public void ExternalCustomer_name_without_letters_is_invalid(string name)
    {
        Should.Throw<ArgumentException>(() => new ExternalCustomer(name, "VOKA", new ContactPerson("jane", "doe", "jane.doe@hotmail.com", "+3245867952")));
    }

    [Theory]
    [InlineData(" ")]
    [InlineData("   ")]
    [InlineData(".")]
    [InlineData("/")]
    [InlineData("")]
    [InlineData("io255")]
    [InlineData("255")]
    public void ExternalCustomer_type_without_letters_is_invalid(string type)
    {
        Should.Throw<ArgumentException>(() => new ExternalCustomer("Name", type, new ContactPerson("jane", "doe", "jane.doe@hotmail.com", "+3245867952")));
    }
}
