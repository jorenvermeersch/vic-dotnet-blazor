using Domain;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests;

public class InternalCustomerTests
{
    [Fact]
    public void InternalCustomer_creation_is_correct()
    {
        InternalCustomer customer = new("Toegepaste Informatica", "DIT", new ContactPerson("jane", "doe", "jane.doe@hotmail.com", "+3245867952"), new ContactPerson("Alice", "Smith", "alice.smith@hotmail.com", "+3285768245"));
        customer.ShouldNotBeNull();
        customer.Education.ShouldBe("Toegepaste Informatica");
        customer.Departement.ShouldBe("DIT");
        customer.ContactPerson.Email.ShouldBe("jane.doe@hotmail.com");
        customer?.BackupContactPerson?.Email.ShouldBe("alice.smith@hotmail.com");
    }

    [Fact]
    public void InternalCustomer_creation_without_education_and_backupContactPerson_is_correct()
    {
        InternalCustomer customer = new("", "DIT", new ContactPerson("jane", "doe", "jane.doe@hotmail.com", "+3245867952"));
        customer.ShouldNotBeNull();
        customer.Education.ShouldBe("");
        customer.Departement.ShouldBe("DIT");
        customer.ContactPerson.Email.ShouldBe("jane.doe@hotmail.com");
        customer?.BackupContactPerson?.Email.ShouldBe("");
    }


    [Theory]
    [InlineData(" ")]
    [InlineData("   ")]
    [InlineData(".")]
    [InlineData("/")]
    [InlineData("")]
    public void InternalCustomer_departement_without_letters_is_invalid(string departement)
    {
        Should.Throw<ArgumentException>(() => new InternalCustomer("Toegepaste Informatica", departement, new ContactPerson("jane", "doe", "jane.doe@hotmail.com", "+3245867952"), null));
    }
}
