namespace Shared.Accounts;

public interface IAccountService
{
    Task<AccountResponse.GetIndex> GetIndexAsync(AccountRequest.GetIndex request);
    Task<AccountResponse.GetDetail> GetDetailAsync(AccountRequest.GetDetail request);
    Task<AccountResponse.Create> CreateAsync(AccountRequest.Create request);
    Task<AccountResponse.Edit> EditAsync(AccountRequest.Edit request);
    Task DeleteAsync(AccountRequest.Delete request);
}
