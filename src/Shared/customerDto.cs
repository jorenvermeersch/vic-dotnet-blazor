using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared;

public static class CustomerDto
{
    public class Index
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }

    public class Details : Index
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Education { get; set; }
        public string Department { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string BackupFirstname { get; set; }
        public string BackupLastname { get; set; }
        public string BackupEmail { get; set; }
        public string BackupPhoneNumber { get; set; }
    }
   
}
