using Domain.Args;
using Domain.Domain;

namespace Domain.Controllers;

public class AccountController
{
    #region Fields
    private VIC _vic = VIC.Instance;
    #endregion

    #region Constructors
    public AccountController() { }
    #endregion

    #region Methods
    public ISet<Account> GetAll()
    {
        return _vic.AccountRepository.Entities;
    }

    public Account GetById(long id)
    {
        throw new NotImplementedException();
        // TODO: Implement method.
    }

    public void Add(AccountArgs args)
    {
        _vic.CreateAccount(args);
    }

    public void UpdateById(long id, AccountArgs args)
    {
        throw new NotImplementedException();
        // TODO: Implement method.
    }
    #endregion
}
