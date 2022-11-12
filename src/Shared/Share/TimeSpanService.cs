using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Shared;

public class TimeSpanService
{
	public readonly List<TimeSpanDto> timespan = new();
	public TimeSpanService()
	{
		var timespanFake = new Faker<TimeSpanDto>()
            .UseSeed(1337)
            .RuleFor(x => x.StartDate, f => f.Date.Between(DateTime.Now, new DateTime(2023, 1, 30)))
			.RuleFor(x => x.EndDate, (f, u) => f.Date.Between(u.StartDate, new DateTime(2023, 5, 30)));

		timespan = timespanFake.Generate(30);
    }
}
