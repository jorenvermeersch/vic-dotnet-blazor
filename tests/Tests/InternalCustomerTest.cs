using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests;

public class InternalCustomerTest
{
    [Fact]
    public void NewInternalCustomer_CreatedCorrectly()
    {
        InternalCustomer customer = new("Toegepaste Informatica", "DIT", new ContactPerson("jane", "doe", "jane.doe@hotmail.com", "+3245867952"), new ContactPerson("Alice", "Smith", "alice.smith@hotmail.com", "+3285768245"));
        Assert.NotNull(customer);
        Assert.Equal("Toegepaste Informatica", customer.Education);
        Assert.Equal("DIT", customer.Departement);
        Assert.Equal("jane.doe@hotmail.com", customer.ContactPerson.Email);
        Assert.Equal("alice.smith@hotmail.com", customer.BackupContactPerson.Email);
    }

    [Fact]
    public void NewInternalCustomer_EducationAndBackupContactEmpty_CreatedCorrectly()
    {
        InternalCustomer customer = new("", "DIT", new ContactPerson("jane", "doe", "jane.doe@hotmail.com", "+3245867952"));
        Assert.NotNull(customer);
        Assert.Equal("", customer.Education);
        Assert.Equal("DIT", customer.Departement);
        Assert.Equal("jane.doe@hotmail.com", customer.ContactPerson.Email);
        Assert.Null(customer.BackupContactPerson);
    }


    [Theory]
    [InlineData(" ")]
    [InlineData("   ")]
    [InlineData(".")]
    [InlineData("/")]
    [InlineData("")]
    public void NewInternalCustomer_InvalidDepartement_ThrowsException(string departement)
    {
        Assert.Throws<ArgumentException>(() => new InternalCustomer("Toegepaste Informatica", departement, new ContactPerson("jane", "doe", "jane.doe@hotmail.com", "+3245867952"), null));
    }
}
