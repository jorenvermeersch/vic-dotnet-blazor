using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Account;

public static class AccountRequest
{
    public class GetIndex
    {
        public string SearchTerm { get; set; }
        public bool OnlyActiveProducts { get; set; }
        public decimal? MinimumPrice { get; set; }
        public decimal? MaximumPrice { get; set; }
        public int Page { get; set; }
        public int Amount { get; set; } = 25;
        public int Offset { get; set; } = 0;
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
