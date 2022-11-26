using BogusStore.Fakers.Common;

namespace Fakers.TimeSpan;

public class TimeSpanFaker : EntityFaker<Domain.VirtualMachines.TimeSpan>
{
    public TimeSpanFaker()
    {
        CustomInstantiator(f => new Domain.VirtualMachines.TimeSpan(
            startDate: f.Date.Between(DateTime.Now, new DateTime(2023, 1, 30)),
            endDate: f.Date.Between(new DateTime(2023, 2, 1), new DateTime(2023, 5, 30))
        ));
    }
}
