using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests;

public class ContactPersonTest
{

    [Fact]
    public void NewContactPerson_CreatedCorrectly()
    {
        ContactPerson contact = new ("Jane", "Doe", "jane.doe@hotmail.com", "+3247259836");
        Assert.NotNull(contact);
        Assert.Equal("Jane", contact.Firstname);
        Assert.Equal("Doe", contact.Lastname);
        Assert.Equal("jane.doe@hotmail.com", contact.Email);
        Assert.Equal("+3247259836", contact.PhoneNumber);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData("abc")]
    [InlineData("abc.ijk.com")]
    [InlineData("abc@cde")]
    [InlineData("abc/uin@cde.com")]
    public void NewContactPerson_InvalidEmail_ThrowsException(string email)
    {
        Assert.Throws<ArgumentException>(()=>new ContactPerson("Jane", "Doe", email, "+3247259836"));
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData("/1234567892")]
    [InlineData("AAA14725868")]
    [InlineData("+324725983O")] //o ipv 0
    [InlineData("125-8675252365896")]
    public void NewContactPerson_InvalidPhoneNumber_ThrowsException(string phoneNumber)
    {
        Assert.Throws<ArgumentException>(() => new ContactPerson("Jane", "Doe", "jane.doe@hotmail.com", phoneNumber));
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData("abc142")]
    [InlineData("1435")] //o ipv 0
    [InlineData("abn/djik")]
    [InlineData("abn.djik")]
    public void NewContactPerson_InvalidFirstAndLastName_ThrowsException(string name)
    {
        Assert.Throws<ArgumentException>(() => new ContactPerson(name, name, "jane.doe@hotmail.com", "+3247259836"));
    }

    [Theory]
    [InlineData("jane.doe@student.hogent.be")]
    [InlineData("janedoe@hogent.be")]
    [InlineData("jane.doe@student.hogent.com")]
    public void NewContactPerson_ValidEmail_CreatesContactPerson(string email)
    {
        ContactPerson contact = new("Jane", "Doe", email, "+3247259836");
        Assert.NotNull(contact);
        Assert.Equal(email, contact.Email);
    }
}
