namespace Tests;

public class Customer_Should
{
    private Customer? _customer;
    private ContactPerson? _backupContactPerson;

    #region Valid fields
    private string _validName = "HoGent";
    private string _validType = "Organisatie";
    private ContactPerson _contactPerson = new("Joren", "Vermeersch", "joren.vermeersch@student.hogent.be", "404-968-5025");
    #endregion

    [Fact]
    public void Customer_with_contact_and_backup_contact_with_the_same_email_is_invalid()
    {
        _backupContactPerson = new("Other", "Person", _contactPerson.Email);
        Should.Throw<ArgumentException>(() => _customer = new ExternalCustomer(_validName, _validType, _contactPerson, _backupContactPerson));
    }

    [Fact]
    public void Customer_with_contact_and_backup_contact_with_the_same_phone_number_is_invalid()
    {
        _backupContactPerson = new("Other", "Person", "kerem.yilmaz@student.hogent.be", _contactPerson.PhoneNumber);
        Should.Throw<ArgumentException>(() => _customer = new ExternalCustomer(_validName, _validType, _contactPerson, _backupContactPerson));
    }

    [Fact]
    public void Changing_contact_person_email_or_phonenumber_to_backup_contact_is_invalid()
    {
        _backupContactPerson = new("Other", "Person", _contactPerson.Email);
        _customer = new ExternalCustomer(_validName, _validType, _contactPerson);

        Should.Throw<ArgumentException>(() => _customer.BackupContactPerson = _backupContactPerson);
    }
    [Fact]
    public void Changing_backup_contact_email_or_phonenumber_to_main_contact_person_is_invalid()
    {
        _backupContactPerson = new("Other", "Person", "kerem.yilmaz@student.hogent.be", "404-968-5021");
        _customer = new ExternalCustomer(_validName, _validType, _contactPerson, _backupContactPerson);

        Should.Throw<ArgumentException>(() => _customer.ContactPerson = new ContactPerson("Same", "Email", _backupContactPerson.Email));
    }


}
