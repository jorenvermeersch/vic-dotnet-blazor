using Domain.Domain;

namespace Domain.Interfaces;

public interface ICustomer
{
    #region Fields
    public ContactPerson ContactPerson { get; set; }
    public ContactPerson BackupContactPerson { get; set; }
    #endregion
}
