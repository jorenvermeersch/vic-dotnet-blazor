using BogusStore.Fakers.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fakers.TimeSpansFaker;

public class TimeSpanFaker : EntityFaker<Domain.Core.TimeSpan>
{
    public TimeSpanFaker()
    {
        CustomInstantiator(f => new Domain.Core.TimeSpan(
            startDate: f.Date.Between(DateTime.Now, new DateTime(2023, 1, 30)), 
            endDate: f.Date.Between(new DateTime(2023, 2, 1), new DateTime(2023, 5, 30))
        ));
    }
}
