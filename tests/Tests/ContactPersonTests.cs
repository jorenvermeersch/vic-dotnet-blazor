using Domain.Core;

namespace Tests;
public class ContactPersonTests
{

    [Fact]
    public void ContactPerson_creation_is_correct()
    {
        ContactPerson contact = new ("Jane", "Doe", "jane.doe@hotmail.com", "+3247259836");
        contact.ShouldNotBeNull();
        contact.Firstname.ShouldBe("Jane");
        contact.Lastname.ShouldBe("Doe");
        contact.Email.ShouldBe("jane.doe@hotmail.com");
        contact.PhoneNumber.ShouldBe("+3247259836");
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData("abc")]
    [InlineData("abc.ijk.com")]
    [InlineData("abc@cde")]
    [InlineData("abc/uin@cde.com")]
    public void ContactPerson_email_with_incorrect_structure_is_invalid(string email)
    {
        Should.Throw<ArgumentException>(() => new ContactPerson("Jane", "Doe", email, "+3247259836"));
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData("/1234567892")]
    [InlineData("AAA14725868")]
    [InlineData("+324725983O")] //o ipv 0
    [InlineData("125-8675252365896")]
    public void ContactPerson_phoneNumber_with_incorrect_structure_is_invalid(string phoneNumber)
    {
        Should.Throw<ArgumentException>(() => new ContactPerson("Jane", "Doe", "jane.doe@hotmail.com", phoneNumber));
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData("abc142")]
    [InlineData("1435")] 
    [InlineData("abn/djik")]
    [InlineData("abn.djik")]
    public void ContactPerson_firstAndLastName_with_characters_besides_alphabets_is_invalid(string name)
    {
        Should.Throw<ArgumentException>(() => new ContactPerson(name, name, "jane.doe@hotmail.com", "+3247259836"));
    }

    [Theory]
    [InlineData("jane.doe@student.hogent.be")]
    [InlineData("janedoe@hogent.be")]
    [InlineData("jane.doe@student.hogent.com")]
    public void ContactPerson_creation_with_valid_email_is_correct(string email)
    {
        ContactPerson contact = new("Jane", "Doe", email, "+3247259836");
        contact.ShouldNotBeNull();
        contact.Email.ShouldBe(email);
    }
}
