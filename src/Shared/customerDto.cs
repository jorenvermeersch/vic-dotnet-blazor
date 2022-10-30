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
        public string CustomerName { get; set; }
    }

    public class Details
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Education { get; set; }
        public string Department { get; set; }
        public ContactPersonDto ContactPersonDto { get; set; }
        public ContactPersonDto BackupContactPersonDto { get; set; }
    }
}
