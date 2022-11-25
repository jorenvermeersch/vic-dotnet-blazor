﻿namespace Shared.Account;

public static class AccountRequest
{
    public class Index
    {
        public string? SearchTerm { get; set; }
        public List<string>? Roles { get; set; }
        public int Page { get; set; }
        public int Amount { get; set; } = 25;
        public int Offset { get; set; } = 0;
    }
}
