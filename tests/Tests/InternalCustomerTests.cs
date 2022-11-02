namespace Tests;

public class InternalCustomerTests
{
    [Fact]
    public void InternalCustomer_creation_is_correct()
    {
        InternalCustomer customer = new(Institution.HoGent,"DIT","Toegepaste Informatica", new ContactPerson("jane", "doe", "jane.doe@hotmail.com", "+3245867952"), new ContactPerson("Alice", "Smith", "alice.smith@hotmail.com", "+3285768245"));
        customer.ShouldNotBeNull();
        customer.Institution.ShouldBe(Institution.HoGent);
        customer.Education.ShouldBe("Toegepaste Informatica");
        customer.Department.ShouldBe("DIT");
        customer.ContactPerson.Email.ShouldBe("jane.doe@hotmail.com");
        customer?.BackupContactPerson?.Email.ShouldBe("alice.smith@hotmail.com");
    }

    [Fact]
    public void InternalCustomer_creation_without_education_is_correct()
    {
        InternalCustomer customer = new(Institution.HoGent, "DIT","", new ContactPerson("jane", "doe", "jane.doe@hotmail.com", "+3245867952"),new ContactPerson("jane", "doe", "jane.doe@hotmail.com", "+3245867952"));
        customer.ShouldNotBeNull();
        customer.Institution.ShouldBe(Institution.HoGent);
        customer.Education.ShouldBe("");
        customer.Department.ShouldBe("DIT");
        customer.ContactPerson.Email.ShouldBe("jane.doe@hotmail.com");
        customer.BackupContactPerson.Email.ShouldBe("jane.doe@hotmail.com");
    }

    [Theory]
    [InlineData(" ")]
    [InlineData("   ")]
    [InlineData(".")]
    [InlineData("k66")]
    [InlineData("")]
    public void InternalCustomer_departement_with_characters_besides_alphabets_is_invalid(string departement)
    {
        Should.Throw<ArgumentException>(() => new InternalCustomer(Institution.HoGent, departement, "Toegepaste Informatica", new ContactPerson("jane", "doe", "jane.doe@hotmail.com", "+3245867952"), null));
    }
}
