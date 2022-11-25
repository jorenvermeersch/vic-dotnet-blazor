namespace Shared.Account;

public interface IAccountService
{
    Task<AccountResult.Index> GetIndexAsync(AccountRequest.Index request);
    Task<AccountDto.Details> GetDetailAsync(long accountId);
    Task<long> CreateAsync(AccountDto.Mutate model);
    Task EditAsync(long accountId, AccountDto.Mutate model);
    Task DeleteAsync(long accountId);
}
