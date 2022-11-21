using Shared.VirtualMachine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Account;

public interface IAccountService
{
    Task<AccountResponse.GetIndex> GetIndexAsync(AccountRequest.GetIndex request);
    Task<AccountResponse.GetDetail> GetDetailAsync(/*long AccountId*/ AccountRequest.GetDetail request);
    Task<AccountDto.Details> Add(AccountDto.Create newAccount);
}
