using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Account;

public static class AccountResponse
{
    public class GetIndex
    {
        public List<AccountDto.Index> Accounts { get; set; } = new();
        public int TotalAmount { get; set; }
    }

    public class GetDetail
    {
        
    }

    public class Delete
    {
    }

    public class Create
    {
       
    }

    public class Edit
    {
        
    }
}
