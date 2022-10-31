using Shared.VirtualMachine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Account;

public interface IAccountService
{
    Task<IEnumerable<AccountDto.Index>> GetIndexAsync();
    Task<AccountDto.Details> GetDetailAsync(long AccountId);
}
